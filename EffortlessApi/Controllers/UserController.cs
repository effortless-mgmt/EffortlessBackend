using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EffortlessApi.Core;
using EffortlessApi.Core.Models;
using EffortlessApi.Persistence;
using EffortlessLibrary.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EffortlessApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserController(EffortlessContext context, IMapper mapper)
        {
            _unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var usersModels = await _unitOfWork.Users.GetAllAsync();
            if (usersModels == null) return NotFound();

            var userDTOs = _mapper.Map<List<UserSimpleDTO>>(usersModels);

            foreach (UserSimpleDTO u in userDTOs)
            {
                if (u.AddressId != 0)
                    u.Address = _mapper.Map<AddressSimpleDTO>(await _unitOfWork.Addresses.GetByIdAsync(u.AddressId));
            }

            return Ok(userDTOs.OrderBy(u => u.UserName));
        }

        [HttpGet("{username}", Name = "GetUser")]
        public async Task<IActionResult> GetByUsername(string userName)
        {
            var userModel = await _unitOfWork.Users.GetByUsernameAsync(userName);
            if (userModel == null) return NotFound($"User {userName} could not be found.");

            var userDTO = _mapper.Map<UserDTO>(userModel);

            return Ok(userDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(UserDTO userDTO)
        {
            if (userDTO == null) return BadRequest("UserDTO is null");

            var existingUser = await _unitOfWork.Users.GetByUsernameAsync(userDTO.UserName);
            if (existingUser != null) return Conflict($"Username \"{userDTO.UserName}\" is already taken.");

            var userModel = _mapper.Map<User>(userDTO);
            await _unitOfWork.Users.AddAsync(userModel);
            await _unitOfWork.CompleteAsync();

            userDTO = _mapper.Map<UserDTO>(userModel);

            return CreatedAtRoute("GetUser", new { userName = userDTO.UserName }, userDTO);
        }

        [HttpPut("{userName}")]
        public async Task<IActionResult> UpdateAsync(string userName, UserDTO userDTO)
        {
            var existing = await _unitOfWork.Users.GetByUsernameAsync(userName);
            if (existing == null) return NotFound($"User {userName} could not be found.");

            if (userDTO.Address != null)
            {
                var userAddressModel = _mapper.Map<Address>(userDTO.Address);
                await _unitOfWork.Addresses.AddAsync(userAddressModel);
                await _unitOfWork.CompleteAsync();

                userDTO.AddressId = userAddressModel.Id;
            }

            var addressModel = _mapper.Map<Address>(await _unitOfWork.Addresses.GetByIdAsync(userDTO.AddressId));
            var userModel = _mapper.Map<User>(userDTO);
            await _unitOfWork.Users.UpdateAsync(userName, userModel);
            await _unitOfWork.CompleteAsync();

            userDTO = _mapper.Map<UserDTO>(existing);

            return Ok(userDTO);
        }

        [HttpDelete("{userName}")]
        public async Task<IActionResult> DeleteAsync(string userName)
        {
            var user = await _unitOfWork.Users.GetByUsernameAsync(userName);
            if (user == null) return NoContent();

            _unitOfWork.Users.Remove(user);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpPost("{userName}/role/{roleId}")]
        public async Task<IActionResult> CreateUserRoleAsync(string userName, long roleId)
        {
            var userModel = await _unitOfWork.Users.GetByUsernameAsync(userName);
            if (userModel == null) return NotFound($"User {userName} does not exist.");

            var role = await _unitOfWork.Roles.GetByIdWithUsersAsync(roleId);
            if (role == null) return NotFound($"Role with id {roleId} does not exist.");

            var existingUserRole = await _unitOfWork.UserRoles.GetByIdAsync(userModel.Id, roleId);
            if (existingUserRole != null) return Ok(_mapper.Map<UserRoleDTO>(existingUserRole));

            var userRoleModel = _mapper.Map<UserRole>(new UserRoleDTO(userModel.Id, roleId));
            await _unitOfWork.UserRoles.AddAsync(userRoleModel);
            await _unitOfWork.CompleteAsync();

            var userDTO = _mapper.Map<UserDTO>(userModel);
            // var userRoleDTO = _mapper.Map<UserRoleDTO>(userRoleModel);

            return CreatedAtRoute("GetUser", new { userName = userDTO.UserName }, userDTO);
        }

        [HttpDelete("{userName}/role/{roleId}")]
        public async Task<IActionResult> DeleteUserRoleAsync(string userName, long roleId)
        {
            var userModel = await _unitOfWork.Users.GetByUsernameAsync(userName);
            if (userModel == null) return NotFound($"User {userName} does not exist.");

            var role = await _unitOfWork.Roles.GetByIdAsync(roleId);
            if (role == null) return NotFound($"Role with id {roleId} does not exist.");

            var userRole = await _unitOfWork.UserRoles.GetByIdAsync(userModel.Id, roleId);
            if (userRole == null) return NoContent();

            _unitOfWork.UserRoles.Remove(userRole);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpGet("{userName}/role")]
        public async Task<IActionResult> GetRolesAssociatedWithUser(string userName)
        {
            var userModel = await _unitOfWork.Users.GetByUsernameAsync(userName);
            if (userModel == null) return NotFound($"User {userName} does not exist.");

            var userDTO = _mapper.Map<UserDTO>(userModel);
            var userRoles = userDTO.UserRoles.Select(ur => ur.Role);
            var RoleDTOs = _mapper.Map<List<RoleDTO>>(userRoles);

            return Ok(RoleDTOs);
        }

        [HttpGet("workperiod")]
        public async Task<IActionResult> GetUserWorkPeriods()
        {
            var userWorkPeriodDTOs = _mapper.Map<List<UserWorkPeriod>>(await _unitOfWork.UserWorkPeriods.GetAllAsync());
            if (userWorkPeriodDTOs == null) return NotFound();

            return Ok(userWorkPeriodDTOs);
        }

        [HttpGet("{userName}/workperiod")]
        public async Task<IActionResult> GetUserWorkPeriods(string userName)
        {
            var userModel = await _unitOfWork.Users.GetByUsernameAsync(userName);
            if (userModel == null) return NotFound($"User {userName} does not exist.");

            var userRoleDTOs = _mapper.Map<List<UserWorkPeriod>>(await _unitOfWork.UserWorkPeriods.GetAllAsync());

            return Ok(userRoleDTOs);
        }

        [HttpGet("{userName}/appointment")]
        public async Task<IActionResult> GetUserAppointments(string userName)
        {
            var userModel = await _unitOfWork.Users.GetByUsernameAsync(userName);
            if (userModel == null) return NotFound($"User {userName} does not exist.");

            var userWorkPeriods = await _unitOfWork.UserWorkPeriods.FindAsync(u => u.UserId == userModel.Id);
            List<WorkPeriodUserAppointmentDTO> workPeriodDTOs = new List<WorkPeriodUserAppointmentDTO>();

            foreach (UserWorkPeriod uwp in userWorkPeriods)
            {
                workPeriodDTOs.Add(_mapper.Map<WorkPeriodUserAppointmentDTO>(await _unitOfWork.WorkPeriods.GetByIdAsync(uwp.WorkPeriodId)));
            }

            foreach (WorkPeriodUserAppointmentDTO wp in workPeriodDTOs)
            {
                wp.Appointments = _mapper.Map<List<AppointmentUserDTO>>(await _unitOfWork.Appointments.GetByUserIdAsync(userModel.Id));
            }

            return Ok(workPeriodDTOs);
        }

    }
}
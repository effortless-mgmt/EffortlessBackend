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
            var users = await _unitOfWork.Users.GetAllAsync();

            if (users == null) return NotFound();

            var userDTOs = _mapper.Map<List<UserDTO>>(users);

            return Ok(userDTOs.OrderBy(u => u.Id));
        }

        [HttpGet("{username}", Name = "GetUser")]
        public async Task<IActionResult> GetByUsername(string userName)
        {
            var userModel = await _unitOfWork.Users.GetByUsernameAsync(userName);

            if (userModel == null)
            {
                return NotFound($"User {userName} could not be found.");
            }
            var userDTO = _mapper.Map<UserDTO>(userModel);

            return Ok(userDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest("UserDTO is null");
            }
            
            var userModel = _mapper.Map<User>(userDTO);
            var existingUser = await _unitOfWork.Users.GetByUsernameAsync(userModel.UserName);

            await _unitOfWork.Users.AddAsync(userModel);
            await _unitOfWork.CompleteAsync();

            userDTO = _mapper.Map<UserDTO>(userModel);

            return CreatedAtRoute("GetUser", new { userName = userDTO.UserName }, userDTO);
        }

        [HttpPost("{userName}/role/{roleId}")]
        public async Task<IActionResult> PostRoleToUserAsync(string userName, long roleId)
        {
            var userModel = await _unitOfWork.Users.GetByUsernameAsync(userName);
            if (userModel == null) return NotFound($"User {userName} does not exist.");

            var role = await _unitOfWork.Roles.GetByIdWithUsersAsync(roleId);
            if (role == null) return NotFound($"Role with id {roleId} does not exist.");

            if (role.Users.Contains(userModel)) return BadRequest($"User {userModel.UserName} already has the role {role.Name}.");

            userModel.UserRoles.Add(new UserRole { Role = role, User = userModel });
            await _unitOfWork.CompleteAsync();

            var userDTO = _mapper.Map<UserDTO>(userModel);

            return CreatedAtRoute("GetUser", new { userName = userDTO.UserName }, userDTO);
        }

        [HttpDelete("{userName}/role/{roleId}")]
        public async Task<IActionResult> DeleteRoleFromUserAsync(string userName, long roleId)
        {
            var userModel = await _unitOfWork.Users.GetByUsernameAsync(userName);
            if (userModel == null) return NotFound($"User {userName} does not exist.");

            var role = await _unitOfWork.Roles.GetByIdAsync(roleId);
            if (role == null) return NotFound($"Role with id {roleId} does not exist.");

            var userDTO = _mapper.Map<UserDTO>(userModel);

            if (userModel.UserRoles.Select(ur => ur.Role).ToList().Contains(role))
            {
                var userRole = userModel.UserRoles.FirstOrDefault(ur => ur.RoleId == roleId);
                userModel.UserRoles.Remove(userRole);

                await _unitOfWork.CompleteAsync();

                userDTO = _mapper.Map<UserDTO>(userModel);


                return Ok(userDTO);
            }

            return Ok(userDTO);
        }

        [HttpGet("{userName}/role")]
        public async Task<IActionResult> GetRolesAssociatedWithUser(string userName)
        {
            var userModel = await _unitOfWork.Users.GetByUsernameAsync(userName);
            if (userModel == null) return NotFound($"User {userName} does not exist.");
            var userDTO = _mapper.Map<UserDTO>(userModel);

            var userRoles = userDTO.UserRoles.Select(ur => ur.Role);
            var RoleDTOs = _mapper.Map<List<RoleSimpleDTO>>(userRoles);
            return Ok(RoleDTOs);
        }

        [HttpPut("{userName}")]
        public async Task<IActionResult> UpdateAsync(string userName, UserDTO userDTO)
        {
            var existing = await _unitOfWork.Users.GetByUsernameAsync(userName);

            if (existing == null) return NotFound($"User {userName} could not be found.");

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

            if (user == null)
            {
                return NoContent();
            }

            _unitOfWork.Users.Remove(user);

            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
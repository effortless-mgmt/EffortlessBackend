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

            return Ok(users);
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
        public async Task<IActionResult> PostAsync(User userDTO)
        {
            var userModel = _mapper.Map<User>(userDTO);
            var existingUser = await _unitOfWork.Users.GetByUsernameAsync(userModel.UserName);

            if (existingUser != null) return Ok(existingUser);
            if (userModel == null) return BadRequest();

            await _unitOfWork.Users.AddAsync(userModel);
            await _unitOfWork.CompleteAsync();

            return CreatedAtRoute("GetUser", new { userName = userModel.UserName }, userModel);
        }

        [HttpPost("{userName}/role/{roleId}")]
        public async Task<IActionResult> PostRoleToUserAsync(string userName, long roleId)
        {
            var user = await _unitOfWork.Users.GetByUsernameAsync(userName);
            if (user == null) return NotFound($"User {userName} does not exist.");

            var role = await _unitOfWork.Roles.GetByIdWithUsersAsync(roleId);
            if (role == null) return NotFound($"Role with id {roleId} does not exist.");

            if (role.Users.Contains(user)) return BadRequest($"User {user.UserName} already has the role {role.Name}.");

            user.UserRoles.Add(new UserRole { Role = role, User = user });
            await _unitOfWork.CompleteAsync();

            return CreatedAtRoute("GetUser", new { userName = user.UserName }, user);
        }

        [HttpDelete("{userName}/role/{roleId}")]
        public async Task<IActionResult> DeleteRoleFromUserAsync(string userName, long roleId)
        {
            var user = await _unitOfWork.Users.GetByUsernameAsync(userName);
            if (user == null) return NotFound($"User {userName} does not exist.");

            var role = await _unitOfWork.Roles.GetByIdAsync(roleId);
            if (role == null) return NotFound($"Role with id {roleId} does not exist.");

            if (user.UserRoles.Select(ur => ur.Role).ToList().Contains(role))
            {
                var userRole = user.UserRoles.FirstOrDefault(ur => ur.RoleId == roleId);
                user.UserRoles.Remove(userRole);

                await _unitOfWork.CompleteAsync();

                return Ok(user);
            }

            return Ok(user);
        }

        [HttpGet("{userName}/role")]
        public async Task<IActionResult> GetRolesAssociatedWithUser(string userName)
        {
            var user = await _unitOfWork.Users.GetByUsernameAsync(userName);
            if (user == null) return NotFound($"User {userName} does not exist.");

            // TODO: Clean up output, don't output the user object for each role
            return Ok(user.UserRoles.Select(ur => ur.Role));
        }

        [HttpPut("{userName}")]
        public async Task<IActionResult> Put(string userName, User user)
        {
            var existing = await _unitOfWork.Users.GetByUsernameAsync(userName);

            if (existing == null) return NotFound($"User {user.UserName} could not be found.");

            await _unitOfWork.Users.UpdateAsync(userName, user);
            await _unitOfWork.CompleteAsync();

            return Ok(existing);
        }

        [HttpDelete("{userName}")]
        public async Task<IActionResult> Delete(string userName)
        {
            var user = await _unitOfWork.Users.GetByUsernameAsync(userName);

            if (user == null)
            {
                return NoContent();
            }

            _unitOfWork.Users.Remove(user);

            await _unitOfWork.CompleteAsync();

            return Ok(user);
        }
    }
}
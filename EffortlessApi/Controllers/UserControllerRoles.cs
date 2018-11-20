using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EffortlessApi.Core.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace EffortlessApi.Controllers
{
    public partial class UserController
    {
        [HttpGet("{userName}/role"), Authorize]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Role>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetRolesAssociatedWithUser(string userName)
        {
            var user = await _unitOfWork.Users.GetByUsernameAsync(userName);
            if (user == null) return NotFound($"User {userName} does not exist.");

            // TODO: Clean up output, don't output the user object for each role
            return Ok(user.UserRoles.Select(ur => ur.Role));
        }

        [HttpPost("{userName}/role/{roleId}"), Authorize]
        [ProducesResponseType(201, Type = typeof(User))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PostRoleToUserAsync(string userName, long roleId)
        {
            var user = await _unitOfWork.Users.GetByUsernameAsync(userName);
            if (user == null) return NotFound($"User {userName} does not exist.");
            
            var role = await _unitOfWork.Roles.GetByIdWithUsersAsync(roleId);
            if (role == null) return NotFound($"Role with id {roleId} does not exist.");

            if (role.Users.Contains(user)) return BadRequest($"User {user.UserName} already has the role {role.Name}.");

            user.UserRoles.Add(new UserRole{ Role = role, User = user });
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetByUsernameAsync), new { userName = user.UserName }, user);
        }

        [HttpDelete("{userName}/role/{roleId}"), Authorize]
        [ProducesResponseType(201, Type = typeof(User))]
        [ProducesResponseType(404)]
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
    }
}
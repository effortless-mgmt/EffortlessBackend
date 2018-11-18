using System.Linq;
using EffortlessApi.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EffortlessApi.Core.Models;
using System.Collections.Generic;
using EffortlessApi.Persistence;

namespace EffortlessApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleController(EffortlessContext context) => _unitOfWork = new UnitOfWork(context);

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _unitOfWork.Roles.GetAllAsync());
        }

        [HttpGet("{id}", Name = "GetRole")]
        public async Task<IActionResult> GetById(long id)
        {
            var role = await _unitOfWork.Roles.GetByIdAsync(id);

            if (role == null)
            {
                return NotFound($"Role with id {id} could not be found.");
            }

            return Ok(role);
        }

        [HttpGet("{roleId}/user")]
        public async Task<IActionResult> GetUsersByRoleId(long roleId)
        {
            var role = await _unitOfWork.Roles.GetByIdWithUsersAsync(roleId);

            if (role == null)
            {
                return NotFound($"Role with id {roleId} could not be found.");
            }

            return Ok(role.Users);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Role role)
        {
            var existingRole = await _unitOfWork.Roles.FindAsync(r => r.Name == role.Name);

            if (existingRole != null) return Ok(existingRole);

            await _unitOfWork.Roles.AddAsync(role);
            await _unitOfWork.CompleteAsync();

            return CreatedAtRoute("GetRole", new { id = role.Id }, role);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Role newRole)
        {
            var existingRole = await _unitOfWork.Roles.GetByIdAsync(id);

            if (existingRole == null) return NotFound($"Role with id {id} does not exist.");

            await _unitOfWork.Roles.UpdateAsync(id, newRole);
            await _unitOfWork.CompleteAsync();

            return Ok(newRole);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var role = await _unitOfWork.Roles.GetByIdAsync(id);

            if (role == null) return NoContent();

            _unitOfWork.Roles.Remove(role);
            await _unitOfWork.CompleteAsync();

            return Ok(role);
        }
    }
}

using System.Threading.Tasks;
using EffortlessApi.Core;
using EffortlessApi.Core.Models;
using EffortlessApi.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace EffortlessApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrivilegeController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        public PrivilegeController(EffortlessContext context) => _unitOfWork = new UnitOfWork(context);

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var privileges = await _unitOfWork.Privileges.GetAllAsync();

            if (privileges == null) return NotFound();

            return Ok(privileges);
        }

        [HttpGet("{privilegeId}", Name = "GetPrivilege")]
        public async Task<IActionResult> GetByIdAsync(long privilegeId)
        {
            var privilege = await _unitOfWork.Privileges.GetByIdAsync(privilegeId);

            if (privilege == null)
            {
                return NotFound($"Privilege {privilegeId} could not be found.");
            }

            return Ok(privilege);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Privilege privilege)
        {
            var existingPrivilege = await _unitOfWork.Privileges.GetByIdAsync(privilege.Id);

            if (existingPrivilege != null) return Ok(privilege);

            await _unitOfWork.Privileges.AddAsync(privilege);
            await _unitOfWork.CompleteAsync();

            return CreatedAtRoute("GetPrivilege", new { privilegeId = privilege.Id }, privilege);
        }

        [HttpPut("{privilegeId}")]
        public async Task<IActionResult> Put(long privilegeId, Privilege privilege)
        {
            var existingPrivilege = await _unitOfWork.Privileges.GetByIdAsync(privilegeId);

            if (existingPrivilege == null) return NotFound($"Privilege {privilege.Id} could not be found.");

            await _unitOfWork.Privileges.UpdateAsync(privilegeId, privilege);
            await _unitOfWork.CompleteAsync();

            return Ok(existingPrivilege);
        }

        [HttpDelete("{privilegeId}")]
        public async Task<IActionResult> Delete(long privilegeId)
        {
            var privilege = await _unitOfWork.Privileges.GetByIdAsync(privilegeId);

            if (privilege == null)
            {
                return NotFound($"Privilege {privilegeId} could not be found.");
            }

            _unitOfWork.Privileges.Remove(privilege);

            await _unitOfWork.CompleteAsync();

            return Ok(privilege);
        }
    }
}
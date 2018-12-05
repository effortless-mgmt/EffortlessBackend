using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EffortlessApi.Core;
using EffortlessApi.Core.Models;
using EffortlessApi.Persistence;
using EffortlessLibrary.DTO.Privilege;
using Microsoft.AspNetCore.Mvc;

namespace EffortlessApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrivilegeController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PrivilegeController(EffortlessContext context, IMapper mapper)
        {
            _unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var privilegeModels = await _unitOfWork.Privileges.GetAllAsync();

            if (privilegeModels == null) return NotFound();

            var privilegeDTOs = _mapper.Map<List<PrivilegeDTO>>(privilegeModels);

            return Ok(privilegeDTOs.OrderBy(p => p.Id));
        }

        [HttpGet("{privilegeId}", Name = "GetPrivilege")]
        public async Task<IActionResult> GetByIdAsync(long privilegeId)
        {
            var privilegeModel = await _unitOfWork.Privileges.GetByIdAsync(privilegeId);

            if (privilegeModel == null)
            {
                return NotFound($"Privilege {privilegeId} could not be found.");
            }

            var privilegeDTO = _mapper.Map<PrivilegeDTO>(privilegeModel);

            return Ok(privilegeDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] PrivilegeDTO privilegeDTO)
        {
            if (privilegeDTO == null) return BadRequest();

            var privilegeModel = _mapper.Map<Privilege>(privilegeDTO);

            await _unitOfWork.Privileges.AddAsync(privilegeModel);
            await _unitOfWork.CompleteAsync();

            return CreatedAtRoute("GetPrivilege", new { privilegeId = privilegeModel.Id }, privilegeModel);
        }

        [HttpPut("{privilegeId}")]
        public async Task<IActionResult> UpdateAsync(long privilegeId, [FromBody] PrivilegeDTO privilegeDTO)
        {

            var existingPrivilege = await _unitOfWork.Privileges.GetByIdAsync(privilegeId);

            if (existingPrivilege == null) return NotFound($"Privilege {privilegeId} could not be found.");

            var PrivilegeModel = _mapper.Map<Privilege>(privilegeDTO);
            await _unitOfWork.Privileges.UpdateAsync(privilegeId, PrivilegeModel);
            await _unitOfWork.CompleteAsync();

            return Ok(existingPrivilege);
        }

        [HttpDelete("{privilegeId}")]
        public async Task<IActionResult> DeleteAsync(long privilegeId)
        {
            var privilege = await _unitOfWork.Privileges.GetByIdAsync(privilegeId);

            if (privilege == null)
            {
                return NotFound($"Privilege {privilegeId} could not be found.");
            }

            _unitOfWork.Privileges.Remove(privilege);

            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
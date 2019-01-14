using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EffortlessApi.Core;
using EffortlessApi.Core.Models;
using EffortlessApi.Persistence;
using EffortlessLibrary.DTO;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var privilegeModels = await _unitOfWork.Privileges.GetAllAsync();
            if (privilegeModels == null) return NotFound();

            var privilegeDTOs = _mapper.Map<List<PrivilegeDTO>>(privilegeModels);

            return Ok(privilegeDTOs.OrderBy(p => p.Id));
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetPrivilege")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var privilegeModel = await _unitOfWork.Privileges.GetByIdAsync(id);
            if (privilegeModel == null) return NotFound($"Privilege {id} could not be found.");

            var privilegeDTO = _mapper.Map<PrivilegeDTO>(privilegeModel);

            return Ok(privilegeDTO);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] PrivilegeDTO privilegeDTO)
        {
            if (privilegeDTO == null) return BadRequest();

            var privilegeModel = _mapper.Map<Privilege>(privilegeDTO);
            await _unitOfWork.Privileges.AddAsync(privilegeModel);
            await _unitOfWork.CompleteAsync();

            privilegeDTO = _mapper.Map<PrivilegeDTO>(privilegeModel);

            return CreatedAtRoute("GetPrivilege", new { id = privilegeDTO.Id }, privilegeDTO);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, [FromBody] PrivilegeDTO privilegeDTO)
        {
            var existing = await _unitOfWork.Privileges.GetByIdAsync(id);
            if (existing == null) return NotFound($"Privilege {id} could not be found.");

            var PrivilegeModel = _mapper.Map<Privilege>(privilegeDTO);
            await _unitOfWork.Privileges.UpdateAsync(id, PrivilegeModel);
            await _unitOfWork.CompleteAsync();

            privilegeDTO = _mapper.Map<PrivilegeDTO>(existing);

            return Ok(privilegeDTO);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var privilege = await _unitOfWork.Privileges.GetByIdAsync(id);
            if (privilege == null) return NoContent();

            _unitOfWork.Privileges.Remove(privilege);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
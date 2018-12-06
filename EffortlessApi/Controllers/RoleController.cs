using System.Linq;
using EffortlessApi.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EffortlessApi.Core.Models;
using System.Collections.Generic;
using EffortlessApi.Persistence;
using AutoMapper;
using EffortlessLibrary.DTO;

namespace EffortlessApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleController(EffortlessContext context, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = new UnitOfWork(context);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var roleModels = await _unitOfWork.Roles.GetAllAsync();
            if (roleModels == null) return NotFound();

            var roleDTOs = _mapper.Map<List<RoleSimpleDTO>>(roleModels);
            return Ok(roleDTOs.OrderBy(r => r.Id));
        }

        [HttpGet("{id}", Name = "GetRole")]
        public async Task<IActionResult> GetById(long id)
        {
            var roleModel = await _unitOfWork.Roles.GetByIdAsync(id);
            if (roleModel == null) return NotFound($"Role with id {id} could not be found.");

            var roleDTO = _mapper.Map<RoleDTO>(roleModel);

            return Ok(roleDTO);
        }

        [HttpGet("{id}/user")]
        public async Task<IActionResult> GetUsersByRoleId(long id)
        {
            var roleModel = await _unitOfWork.Roles.GetByIdWithUsersAsync(id);

            if (roleModel == null)
            {
                return NotFound($"Role with id {id} could not be found.");
            }
            var roleDTO = _mapper.Map<RoleDTO>(roleModel);

            return Ok(roleDTO.Users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] RoleDTO roleDTO)
        {
            if (roleDTO == null)
            {
                return BadRequest();
            }

            var roleModel = _mapper.Map<Role>(roleDTO);


            await _unitOfWork.Roles.AddAsync(roleModel);
            await _unitOfWork.CompleteAsync();
            //What should it return?
            return CreatedAtRoute("GetRole", new { id = roleDTO.Id }, roleDTO);
        }

        [HttpPost("{id}/privilege/{privilegeId}")]
        public async Task<IActionResult> CreateRolePrivilegeAsync(long id, long privilegeId)
        {
            var rolePrivilegeModel = await _unitOfWork.RolePrivileges.GetByIdAsync(id, privilegeId);
            if (rolePrivilegeModel != null) return Ok(_mapper.Map<RolePrivilegeDTO>(rolePrivilegeModel));

            rolePrivilegeModel = _mapper.Map<RolePrivilege>(new RolePrivilegeDTO(id, privilegeId));
            await _unitOfWork.RolePrivileges.AddAsync(rolePrivilegeModel);
            await _unitOfWork.CompleteAsync();

            var rolePrivilegeDTO = _mapper.Map<RolePrivilegeDTO>(rolePrivilegeModel);

            return CreatedAtRoute("GetRole", new { id = rolePrivilegeDTO.RoleId }, rolePrivilegeDTO);
        }

        [HttpPut("{id}/privilege/{privilegeId}")]
        public async Task<IActionResult> UpdateRolePrivilegeAsync(long id, long privilegeId, RolePrivilegeDTO newRolePrivilegeDTO)
        {
            var existing = await _unitOfWork.RolePrivileges.GetByIdAsync(id, privilegeId);
            if (existing == null) return NotFound($"Privilege {privilegeId} for role {id} does not exist.");
            if (newRolePrivilegeDTO == null) return BadRequest();

            var newRolePrivilegeModel = await _unitOfWork.RolePrivileges.GetByIdAsync(newRolePrivilegeDTO.RoleId, newRolePrivilegeDTO.PrivilegeId);
            if (newRolePrivilegeModel != null) return Ok(newRolePrivilegeDTO);

            newRolePrivilegeModel = _mapper.Map<RolePrivilege>(newRolePrivilegeDTO);
            await _unitOfWork.RolePrivileges.UpdateAsync(existing.RoleId, existing.PrivilegeId, newRolePrivilegeModel);
            await _unitOfWork.CompleteAsync();

            newRolePrivilegeDTO = _mapper.Map<RolePrivilegeDTO>(existing);

            return Ok(newRolePrivilegeDTO);
        }

        [HttpDelete("{id}/privilege/{privilegeId}")]
        public async Task<IActionResult> DeleteRolePrivilegeAsync(long id, long privilegeId)
        {
            var rolePrivilegeModel = await _unitOfWork.RolePrivileges.GetByIdAsync(id, privilegeId);
            if (rolePrivilegeModel == null) return NoContent();

            _unitOfWork.RolePrivileges.Remove(rolePrivilegeModel);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpGet("{id}/privilege/{privilegeId}")]
        public async Task<IActionResult> GetRolePrivilege(long id, long privilegeId)
        {
            var rolePrivilegeModel = await _unitOfWork.RolePrivileges.GetByIdAsync(id, privilegeId);
            if (rolePrivilegeModel == null) return NotFound($"Role {id} with privilege {privilegeId} does not exist.");

            var rolePrivilegeDTO = _mapper.Map<RolePrivilegeDTO>(rolePrivilegeModel);
            var roleDTO = _mapper.Map<RoleDTO>(await _unitOfWork.Roles.GetByIdAsync(rolePrivilegeDTO.RoleId));
            var privilegeDTO = _mapper.Map<PrivilegeDTO>(await _unitOfWork.Privileges.GetByIdAsync(rolePrivilegeDTO.PrivilegeId));

            rolePrivilegeDTO.Role = roleDTO;
            rolePrivilegeDTO.Privilege = privilegeDTO;

            return Ok(rolePrivilegeDTO);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, [FromBody] RoleDTO newRoleDTO)
        {
            var existing = await _unitOfWork.Roles.GetByIdAsync(id);
            if (existing == null) return NotFound($"Role with id {id} does not exist.");
            if (newRoleDTO == null) return BadRequest();

            var roleModel = _mapper.Map<Role>(newRoleDTO);
            await _unitOfWork.Roles.UpdateAsync(id, roleModel);
            await _unitOfWork.CompleteAsync();

            newRoleDTO = _mapper.Map<RoleDTO>(existing);

            return Ok(newRoleDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var role = await _unitOfWork.Roles.GetByIdAsync(id);

            if (role == null) return NoContent();

            _unitOfWork.Roles.Remove(role);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}

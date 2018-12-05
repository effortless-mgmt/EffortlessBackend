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

            if (roleModel == null)
            {
                return NotFound($"Role with id {id} could not be found.");
            }
            var roleDTO = _mapper.Map<RoleDTO>(roleModel);
            return Ok(roleDTO);
        }

        [HttpGet("{roleId}/user")]
        public async Task<IActionResult> GetUsersByRoleId(long roleId)
        {
            var roleModel = await _unitOfWork.Roles.GetByIdWithUsersAsync(roleId);

            if (roleModel == null)
            {
                return NotFound($"Role with id {roleId} could not be found.");
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, [FromBody] Role newRole)
        {
            var existingRole = await _unitOfWork.Roles.GetByIdAsync(id);

            if (existingRole == null) return NotFound($"Role with id {id} does not exist.");
            var RoleModel = _mapper.Map<Role>(newRole);

            await _unitOfWork.Roles.UpdateAsync(id, RoleModel);
            await _unitOfWork.CompleteAsync();
            //What should it return?
            return Ok(newRole);
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class AddressController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddressController(EffortlessContext context, IMapper mapper)
        {
            _unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var addressModels = await _unitOfWork.Addresses.GetAllAsync();
            if (addressModels == null) return NotFound();

            var addressDTOs = _mapper.Map<List<AddressDTO>>(addressModels);

            return Ok(addressDTOs.OrderBy(a => a.Id));
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetAddress")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var addressModel = await _unitOfWork.Addresses.GetByIdAsync(id);
            if (addressModel == null) return NotFound($"Address with id {id} could not be found.");

            var addressDTO = _mapper.Map<AddressDTO>(addressModel);

            return Ok(addressDTO);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AddressDTO addressDTO)
        {
            if (addressDTO == null)
            {
                return BadRequest();
            }

            var addressModel = _mapper.Map<Address>(addressDTO);
            await _unitOfWork.Addresses.AddAsync(addressModel);
            await _unitOfWork.CompleteAsync();

            addressDTO = _mapper.Map<AddressDTO>(addressModel);

            return CreatedAtRoute("GetAddress", new { id = addressDTO.Id }, addressDTO);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] AddressDTO addressDTO)
        {

            var existing = await _unitOfWork.Addresses.GetByIdAsync(id);

            if (existing == null) { return NotFound($"Address Id {id} could not be found."); }

            var addressModel = _mapper.Map<Address>(addressDTO);
            await _unitOfWork.Addresses.UpdateAsync(id, addressModel);
            await _unitOfWork.CompleteAsync();

            addressDTO = _mapper.Map<AddressDTO>(existing);

            return Ok(addressDTO);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var address = await _unitOfWork.Addresses.GetByIdAsync(id);

            if (address == null) { return NoContent(); }

            _unitOfWork.Addresses.Remove(address);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
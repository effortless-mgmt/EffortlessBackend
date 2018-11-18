using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EffortlessApi.Core;
using EffortlessApi.Core.Models;
using EffortlessApi.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace EffortlessApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddressController(EffortlessContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var addresses = await _unitOfWork.Addresses.GetAllAsync();

            if (addresses == null) return NotFound();

            return Ok(addresses);
        }

        [HttpGet("{id}", Name = "GetAddress")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var address = await _unitOfWork.Addresses.GetByIdAsync(id);

            if (address == null)
            {
                return NotFound($"Address with id {id} could not be found.");
            }

            return Ok(address);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Address address)
        {
            if (address == null)
            {
                return BadRequest();
            }

            await _unitOfWork.Addresses.AddAsync(address);
            await _unitOfWork.CompleteAsync();

            return CreatedAtRoute($"GetAddress", new { id = address.Id }, address);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] Address address)
        {
            var existing = await _unitOfWork.Addresses.GetByIdAsync(id);

            if (existing == null) { return NotFound($"Address Id {id} could not be found."); }

            await _unitOfWork.Addresses.UpdateAsync(existing.Id, address);
            await _unitOfWork.CompleteAsync();

            return Ok(existing);
        }

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
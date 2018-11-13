using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EffortlessApi.Core;
using EffortlessApi.Core.Models;
using EffortlessApi.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace EffortlessApi.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase {
        // private readonly EffortlessContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public AddressController (EffortlessContext context) {
            _unitOfWork = new UnitOfWork (context);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync () {
            var addresses = await _unitOfWork.Addresses.GetAllAsync ();

            if (addresses == null) return NotFound ();

            return Ok (addresses);
        }

        [HttpGet ("{id}", Name = "GetAddress")]
        public async Task<IActionResult> GetByIdAsync (long id) {
            var address = await _unitOfWork.Addresses.GetByIdAsync (id);

            if (address == null) {
                return NotFound ($"Address with id {id} could not be found.");
            }

            return Ok (address);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync ([FromBody] Address address) {
            // var existingAddress = await _unitOfWork.Addresses.GetByIdAsync(address.Id);

            // if (existingAddress != null)
            // {
            //     Ok(existingAddress);
            // }
            if (address == null) {
                return BadRequest ();
            }

            await _unitOfWork.Addresses.AddAsync (address);
            _unitOfWork.Complete ();

            return CreatedAtRoute ($"GetAddress", new { id = address.Id}, address);
            // return StatusCode(201, address);
        }
    }
}
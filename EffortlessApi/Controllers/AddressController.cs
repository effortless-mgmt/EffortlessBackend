using System;
using System.Collections.Generic;
using System.Linq;
using EffortlessApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EffortlessApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly EffortlessContext _context;

        public AddressController(EffortlessContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Address>> getAll()
        {
            return _context.Addresses;
        }

        [HttpGet("{id}")]
        public ActionResult<Address> getById(long id)
        {
            var address = _context.Addresses.FirstOrDefault(a => a.Id == id);

            if (address == null)
            {
                return NotFound($"Address {address} could not be found.");
            }

            return address;
        }

    }
}
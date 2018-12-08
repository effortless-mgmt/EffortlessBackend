using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EffortlessApi.Core;
using EffortlessApi.Core.Models;
using EffortlessApi.Persistence;
using EffortlessLibrary.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EffortlessApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class CompanyController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CompanyController(EffortlessContext context, IMapper mapper)
        {
            _unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            Address addressModel;
            AddressDTO addressDTO;

            var companyModels = await _unitOfWork.Companies.GetAllAsync();
            if (companyModels == null) return NotFound();

            var companyDTOs = _mapper.Map<List<CompanyDTO>>(companyModels);

            foreach (CompanyDTO c in companyDTOs)
            {
                addressModel = await _unitOfWork.Addresses.GetByIdAsync(c.AddressId);
                addressDTO = _mapper.Map<AddressDTO>(addressModel);
                c.Address = addressDTO;
            }

            return Ok(companyDTOs.OrderBy(c => c.Id));
        }

        [HttpGet("{id}", Name = "GetCompany")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var companyModel = await _unitOfWork.Companies.GetByIdAsync(id);
            if (companyModel == null) return NotFound($"Company {id} could not be found.");

            var companyDTO = _mapper.Map<CompanyDTO>(companyModel);

            return Ok(companyDTO);
        }

        [HttpGet("{id}/departments")]
        public async Task<IActionResult> GetDepartmentsAsync(long id)
        {
            var companyModel = await _unitOfWork.Companies.GetByIdAsync(id);
            var departmentModels = await _unitOfWork.Departments.FindAsync(d => d.CompanyId == companyModel.Id);

            if (departmentModels == null) return NotFound($"Company {id} does not have any departments.");

            var departmentDTOs = _mapper.Map<List<DepartmentDTO>>(departmentModels);

            foreach (DepartmentDTO c in departmentDTOs)
            {
                c.Address = _mapper.Map<AddressDTO>(await _unitOfWork.Addresses.GetByIdAsync(c.AddressId));
            }

            return Ok(departmentDTOs.OrderBy(d => d.Id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CompanyDTO companyDTO)
        {
            var companyModel = _mapper.Map<Company>(companyDTO);
            if (companyModel == null) return BadRequest();

            var addressModel = _mapper.Map<Address>(companyDTO.Address);
            if (addressModel != null)
            {
                await _unitOfWork.Addresses.AddAsync(addressModel);
                await _unitOfWork.CompleteAsync();
                companyModel.AddressId = addressModel.Id;
            }

            await _unitOfWork.Companies.AddAsync(companyModel);
            await _unitOfWork.CompleteAsync();

            companyDTO = _mapper.Map<CompanyDTO>(companyModel);

            return CreatedAtRoute("GetCompany", new { id = companyDTO.Id }, companyDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, CompanyDTO companyDTO)
        {
            var existing = await _unitOfWork.Companies.GetByIdAsync(id);
            if (existing == null) return NotFound($"Company {id} could not be found.");

            if (companyDTO.Address != null)
            {
                var companyAddressModel = _mapper.Map<Address>(companyDTO.Address);
                await _unitOfWork.Addresses.AddAsync(companyAddressModel);
                await _unitOfWork.CompleteAsync();

                companyDTO.AddressId = companyAddressModel.Id;
            }

            var companyModel = _mapper.Map<Company>(companyDTO);
            await _unitOfWork.Companies.UpdateAsync(id, companyModel);
            await _unitOfWork.CompleteAsync();
            companyDTO = _mapper.Map<CompanyDTO>(existing);

            return Ok(companyDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var company = await _unitOfWork.Companies.GetByIdAsync(id);

            if (company == null) return NoContent();

            _unitOfWork.Companies.Remove(company);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
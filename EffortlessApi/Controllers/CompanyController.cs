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
            var companyModels = await _unitOfWork.Companies.GetAllAsync();
            if (companyModels == null) return NotFound();

            var companyDTOs = _mapper.Map<List<CompanyDTO>>(companyModels);
            var companyModelList = companyModels.ToList();
            Address addressModel;
            AddressDTO addressDTO;

            for (int i = 0; i < companyDTOs.Count; i++)
            {
                addressModel = await _unitOfWork.Addresses.GetByIdAsync(companyModelList[i].AddressId);
                addressDTO = _mapper.Map<AddressDTO>(addressModel);
                companyDTOs[i].Address = addressDTO;
            }

            return Ok(companyDTOs);
        }
        [HttpGet("{companyId}", Name = "GetCompany")]
        public async Task<IActionResult> GetById(long companyId)
        {

            var companyModel = await _unitOfWork.Companies.GetByIdAsync(companyId);
            var companyDTO = _mapper.Map<CompanyDTO>(companyModel);

            // CAN'T SET ADDRESS WITHOUT AN ADDRESS.ID REFERENCE
            var addressModel = await _unitOfWork.Addresses.GetByIdAsync(companyModel?.AddressId);
            var companyAddressDTO = _mapper.Map<AddressDTO>(addressModel);
            companyDTO.Address = companyAddressDTO;

            if (companyDTO == null)
            {
                return NotFound($"Company {companyId} could not be found.");
            }

            return Ok(companyDTO);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CompanyDTO companyDTO)
        {
            var companyModel = _mapper.Map<Company>(companyDTO);
            var addressModel = _mapper.Map<Address>(companyDTO.Address);
            if (addressModel != null)
            {
                await _unitOfWork.Addresses.AddAsync(addressModel);
                await _unitOfWork.CompleteAsync();
                // addressModel = await _unitOfWork.Addresses.GetByIdAsync(addressModel.Id);
                companyModel.AddressId = addressModel.Id;
            }

            if (companyModel == null) return BadRequest();

            await _unitOfWork.Companies.AddAsync(companyModel);
            await _unitOfWork.CompleteAsync();

            return CreatedAtRoute("GetCompany", new { companyId = companyModel.Id }, companyModel);
        }

        [HttpPut("{companyId}")]
        public async Task<IActionResult> Put(long companyId, Company company)
        {
            var existingCompany = await _unitOfWork.Companies.GetByIdAsync(companyId);

            if (existingCompany == null) return NotFound($"Company {companyId} could not be found.");

            await _unitOfWork.Companies.UpdateAsync(companyId, company);
            await _unitOfWork.CompleteAsync();

            return Ok(existingCompany);
        }

        [HttpDelete("{companyId}")]
        public async Task<IActionResult> Delete(long companyId)
        {
            var company = await _unitOfWork.Companies.GetByIdAsync(companyId);

            if (company == null)
            {
                return NoContent();
            }

            _unitOfWork.Companies.Remove(company);

            await _unitOfWork.CompleteAsync();

            return Ok(company);
        }
    }
}
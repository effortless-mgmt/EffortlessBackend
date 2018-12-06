using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EffortlessApi.Core;
using EffortlessApi.Core.Models;
using EffortlessApi.Persistence;
using EffortlessLibrary.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EffortlessApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DepartmentController(EffortlessContext context, IMapper mapper)
        {
            _unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            AddressDTO addressDTO;
            CompanySimpleDTO companyDTO;

            var departmentModels = await _unitOfWork.Departments.GetAllAsync();
            if (departmentModels == null) return NotFound();

            var departmentDTOs = _mapper.Map<List<DepartmentDTO>>(departmentModels);

            foreach (DepartmentDTO d in departmentDTOs)
            {
                companyDTO = _mapper.Map<CompanySimpleDTO>(await _unitOfWork.Companies.GetByIdAsync(d.CompanyId));
                addressDTO = _mapper.Map<AddressDTO>(await _unitOfWork.Addresses.GetByIdAsync(d.AddressId));
                d.Company = companyDTO;
                d.Address = addressDTO;
            }

            return Ok(departmentDTOs);
        }

        [HttpGet("{id}", Name = "GetDepartment")]
        public async Task<IActionResult> GetById(long id)
        {
            CompanySimpleDTO companyDTO;
            AddressDTO addressDTO;

            var departmentModel = await _unitOfWork.Departments.GetByIdAsync(id);
            if (departmentModel == null) return NotFound($"Department {id} could not be found.");

            var departmentDTO = _mapper.Map<DepartmentDTO>(departmentModel);
            companyDTO = _mapper.Map<CompanySimpleDTO>(await _unitOfWork.Companies.GetByIdAsync(departmentModel.CompanyId));
            addressDTO = _mapper.Map<AddressDTO>(await _unitOfWork.Addresses.GetByIdAsync(departmentModel.AddressId));
            departmentDTO.Company = companyDTO;
            departmentDTO.Address = addressDTO;

            return Ok(departmentDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] DepartmentDTO departmentDTO)
        {
            Address addressModel;

            if (departmentDTO == null) return BadRequest();

            var companyModel = await _unitOfWork.Companies.FindByVat(departmentDTO.Company.Vat);
            if (companyModel == null) return BadRequest("You must create a company before creating a company department.");

            var departmentModel = _mapper.Map<Department>(departmentDTO);
            var companyDTO = _mapper.Map<CompanySimpleDTO>(companyModel);

            ///<text>
            ///If department is created without an address, the address will be assigned to that of the parent company.
            ///</text>
            var addressDTO = departmentDTO.Address;
            if (addressDTO == null)
            {
                addressModel = await _unitOfWork.Addresses.GetByIdAsync(companyModel.AddressId);
                addressDTO = _mapper.Map<AddressDTO>(addressModel);
                departmentModel.AddressId = addressModel.Id;
            }
            else
            {
                addressModel = _mapper.Map<Address>(addressDTO);
                await _unitOfWork.Addresses.AddAsync(addressModel);
                await _unitOfWork.CompleteAsync();
                departmentModel.AddressId = addressModel.Id;
            }

            departmentModel.CompanyId = companyModel.Id;

            await _unitOfWork.Departments.AddAsync(departmentModel);
            await _unitOfWork.CompleteAsync();

            departmentDTO.Id = departmentModel.Id;
            departmentDTO.Address = addressDTO;
            departmentDTO.Company = companyDTO;
            departmentDTO.Address.Id = departmentModel.AddressId;

            return CreatedAtRoute("GetDepartment", new { id = departmentModel.Id }, departmentDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, DepartmentDTO departmentDTO)
        {
            var existing = await _unitOfWork.Departments.GetByIdAsync(id);
            if (existing == null) return NotFound($"department {id} could not be found.");

            if (departmentDTO.Address != null)
            {
                var departmentAddressModel = _mapper.Map<Address>(departmentDTO.Address);
                await _unitOfWork.Addresses.AddAsync(departmentAddressModel);
                await _unitOfWork.CompleteAsync();

                departmentDTO.AddressId = departmentAddressModel.Id;
            }

            var companyModel = await _unitOfWork.Companies.GetByIdAsync(existing.CompanyId);
            var departmentModel = _mapper.Map<Department>(departmentDTO);
            await _unitOfWork.Departments.UpdateAsync(id, departmentModel);
            await _unitOfWork.CompleteAsync();

            var addressModel = await _unitOfWork.Addresses.GetByIdAsync(existing.Id);
            departmentDTO = _mapper.Map<DepartmentDTO>(existing);

            return Ok(departmentDTO);
        }

        [HttpDelete("{departmentId}")]
        public async Task<IActionResult> Delete(long departmentId)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(departmentId);

            if (department == null)
            {
                return NoContent();
            }

            _unitOfWork.Departments.Remove(department);

            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
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
            Company companyModel;
            Address addressModel;
            DepartmentCompanyDTO companyDTO;
            AddressDTO addressDTO;

            var departmentModels = await _unitOfWork.Department.GetAllAsync();
            if (departmentModels == null) return NotFound();

            var departmentDTOs = _mapper.Map<List<DepartmentDTO>>(departmentModels);
            var departmentModelsList = departmentModels.ToList();

            for (int i = 0; i < departmentModelsList.Count; i++)
            {
                companyModel = await _unitOfWork.Companies.GetByIdAsync(departmentModelsList[i].CompanyId);
                addressModel = await _unitOfWork.Addresses.GetByIdAsync(departmentModelsList[i].AddressId);
                companyDTO = _mapper.Map<DepartmentCompanyDTO>(companyModel);
                addressDTO = _mapper.Map<AddressDTO>(addressModel);
                departmentDTOs[i].Company = companyDTO;
                departmentDTOs[i].Address = addressDTO;
            }

            return Ok(departmentDTOs);
        }

        [HttpGet("{id}", Name = "GetDepartment")]
        public async Task<IActionResult> GetById(long id)
        {
            Company companyModel;
            Address addressModel;
            DepartmentCompanyDTO companyDTO;
            AddressDTO addressDTO;

            var departmentModel = await _unitOfWork.Department.GetByIdAsync(id);
            if (departmentModel == null) return NotFound($"Department {id} could not be found.");

            var departmentDTO = _mapper.Map<DepartmentDTO>(departmentModel);
            companyModel = await _unitOfWork.Companies.GetByIdAsync(departmentModel.CompanyId);
            addressModel = await _unitOfWork.Addresses.GetByIdAsync(departmentModel.AddressId);
            companyDTO = _mapper.Map<DepartmentCompanyDTO>(companyModel);
            addressDTO = _mapper.Map<AddressDTO>(addressModel);
            departmentDTO.Company = companyDTO;
            departmentDTO.Address = addressDTO;

            return Ok(departmentDTO);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] DepartmentDTO departmentDTO)
        {
            Address addressModel;
            if (departmentDTO == null) return BadRequest();

            var companyModel = await _unitOfWork.Companies.FindByPno(departmentDTO.Company.Pno);
            if (companyModel == null) return BadRequest("You must create a company before creating a company department.");

            var departmentModel = _mapper.Map<Department>(departmentDTO);
            var companyDTO = _mapper.Map<DepartmentCompanyDTO>(companyModel);

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

            await _unitOfWork.Department.AddAsync(departmentModel);
            await _unitOfWork.CompleteAsync();

            departmentDTO.Id = departmentModel.Id;
            departmentDTO.Address = addressDTO;
            departmentDTO.Company = companyDTO;
            departmentDTO.Address.Id = departmentModel.AddressId;

            return CreatedAtRoute("GetDepartment", new { id = departmentModel.Id }, departmentDTO);
        }

        [HttpPut("{departmentId}")]
        public async Task<IActionResult> Put(long departmentId, Department department)
        {
            var existingdepartment = await _unitOfWork.Department.GetByIdAsync(departmentId);

            if (existingdepartment == null) return NotFound($"department {departmentId} could not be found.");

            await _unitOfWork.Department.UpdateAsync(departmentId, department);
            await _unitOfWork.CompleteAsync();

            return Ok(existingdepartment);
        }

        [HttpDelete("{departmentId}")]
        public async Task<IActionResult> Delete(long departmentId)
        {
            var department = await _unitOfWork.Department.GetByIdAsync(departmentId);

            if (department == null)
            {
                return NoContent();
            }

            _unitOfWork.Department.Remove(department);

            await _unitOfWork.CompleteAsync();

            return Ok(department);
        }
    }
}
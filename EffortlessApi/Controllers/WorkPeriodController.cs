using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EffortlessApi.Core;
using EffortlessApi.Core.Models;
using EffortlessApi.Persistence;
using EffortlessLibrary.DTO;
using EffortlessLibrary.DTO.Address;
using EffortlessLibrary.DTO.PivotTables;
using Microsoft.AspNetCore.Mvc;

namespace EffortlessApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkPeriodController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorkPeriodController(EffortlessContext context, IMapper mapper)
        {
            _unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var workPeriodModels = await _unitOfWork.WorkPeriods.GetAllAsync();
            if (workPeriodModels == null) return NotFound();

            var workPeriodDTOs = _mapper.Map<List<WorkPeriodOutDTO>>(workPeriodModels);
            var workPeriodModelsList = workPeriodModels.ToList();

            for (int i = 0; i < workPeriodDTOs.Count; i++)
            {
                var agreementModel = await _unitOfWork.Agreements.GetByIdAsync(workPeriodModelsList[i].AgreementId);
                var departmentModel = await _unitOfWork.Departments.GetByIdAsync(workPeriodModelsList[i].DepartmentId);
                var agreementDTO = _mapper.Map<AgreementDTO>(agreementModel);
                var departmentDTO = _mapper.Map<DepartmentDTO>(departmentModel);
                workPeriodDTOs[i].Agreement = agreementDTO;
                workPeriodDTOs[i].Department = departmentDTO;
            }

            return Ok(workPeriodDTOs);
        }

        [HttpGet("id", Name = "GetWorkPeriod")]
        public async Task<IActionResult> GetById(long id)
        {
            var workPeriodModel = await _unitOfWork.WorkPeriods.GetByIdAsync(id);
            if (workPeriodModel == null) return NotFound($"Work period {id} could not be found.");

            var workPeriodDTO = _mapper.Map<WorkPeriodOutDTO>(workPeriodModel);
            workPeriodDTO.Agreement = _mapper.Map<AgreementDTO>(await _unitOfWork.Agreements.GetByIdAsync(workPeriodModel.AgreementId));
            workPeriodDTO.Department = _mapper.Map<DepartmentDTO>(await _unitOfWork.Agreements.GetByIdAsync(workPeriodModel.DepartmentId));
            workPeriodDTO.Department.Address = _mapper.Map<AddressDTO>(await _unitOfWork.Addresses.GetByIdAsync(workPeriodModel.Department.AddressId));
            workPeriodDTO.Department.Company = _mapper.Map<CompanySimpleDTO>(await _unitOfWork.Companies.GetByIdAsync(workPeriodModel.Department.CompanyId));
            // workPeriodDTO.Appointments = _mapper.Map<List<AppointmentWpDTO>>(await _unitOfWork.Appoint);

            return Ok(workPeriodDTO);

        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkPeriodAsync([FromBody] WorkPeriodInDTO workPeriodInDTO)
        {
            if (workPeriodInDTO == null) return BadRequest();

            var departmentModel = await _unitOfWork.Departments.GetByIdAsync(workPeriodInDTO.DepartmentId);
            if (departmentModel == null) return BadRequest("You must create a department before creating a work period.");

            if (ModelState.IsValid)
            {
                var workPeriodModel = _mapper.Map<WorkPeriod>(workPeriodInDTO);
                await _unitOfWork.WorkPeriods.AddAsync(workPeriodModel);
                await _unitOfWork.CompleteAsync();

                var workPeriodOutDTO = _mapper.Map<WorkPeriodOutDTO>(workPeriodModel);
                var agreementDTO = _mapper.Map<AgreementDTO>(await _unitOfWork.Agreements.GetByIdAsync(workPeriodModel.AgreementId));
                var departmentDTO = _mapper.Map<DepartmentDTO>(await _unitOfWork.Departments.GetByIdAsync(workPeriodModel.DepartmentId));
                departmentDTO.Company = _mapper.Map<CompanySimpleDTO>(await _unitOfWork.Companies.GetByIdAsync(departmentModel.CompanyId));
                departmentDTO.Address = _mapper.Map<AddressDTO>(await _unitOfWork.Addresses.GetByIdAsync(departmentModel.AddressId));


                if (workPeriodModel.UserWorkPeriods != null)
                {
                    var userWorkPeriods = workPeriodModel.UserWorkPeriods.ToList();
                    var userWorkPeriodDTOs = new List<UserWorkPeriodDTO>();

                    for (int i = 0; i < userWorkPeriods.Count; i++)
                    {
                        userWorkPeriodDTOs.Add(_mapper.Map<UserWorkPeriodDTO>(userWorkPeriods[i]));
                    }

                    workPeriodOutDTO.UserWorkPeriods = userWorkPeriodDTOs;
                }

                workPeriodOutDTO.Agreement = agreementDTO;
                workPeriodOutDTO.Department = departmentDTO;
                workPeriodOutDTO.Id = workPeriodModel.Id;

                return CreatedAtRoute("GetWorkPeriod", new { id = workPeriodOutDTO.Id }, workPeriodOutDTO);
            }
            else
            {
                return BadRequest("Please fill all the required fields.");
            }
        }
    }
}
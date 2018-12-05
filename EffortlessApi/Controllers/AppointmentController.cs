using System;
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

    public class AppointmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppointmentController(EffortlessContext context, IMapper mapper)
        {
            _unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            AppointmentStrippedDTO appointmentDTO;
            DepartmentStrippedDTO departmentDTO;
            WorkPeriodStrippedDTO workPeriodDTO;
            UserStrippedDTO userDTO;
            List<AppointmentStrippedDTO> appointmentDTOs = new List<AppointmentStrippedDTO>();

            var appointmentModels = await _unitOfWork.Appointments.GetAllAsync();
            if (appointmentModels == null) return NotFound($"No appointments could be found");

            foreach (Appointment a in appointmentModels)
            {
                appointmentDTO = _mapper.Map<AppointmentStrippedDTO>(await _unitOfWork.Appointments.GetByIdAsync(a.Id));
                workPeriodDTO = _mapper.Map<WorkPeriodStrippedDTO>(await _unitOfWork.WorkPeriods.GetByIdAsync(a.WorkPeriodId));
                departmentDTO = _mapper.Map<DepartmentStrippedDTO>(await _unitOfWork.Departments.GetByIdAsync(workPeriodDTO.DepartmentId));
                userDTO = _mapper.Map<UserStrippedDTO>(await _unitOfWork.Users.GetByIdAsync(appointmentDTO.OwnerId));

                if (departmentDTO == null || workPeriodDTO == null) return BadRequest("Trying to fetch faulty appointments. One or more appointments are not linked to a workperiod and/or workperiod is not linked to a department.");

                appointmentDTO.workPeriod = workPeriodDTO;
                appointmentDTO.workPeriod.Department = departmentDTO;
                appointmentDTO.Owner = userDTO;

                appointmentDTOs.Add(appointmentDTO);
            }

            return Ok(appointmentDTOs.OrderBy(a => a.Id));
        }

        [HttpGet("{id}", Name = "GetAppointment")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var appointmentModel = await _unitOfWork.Appointments.GetByIdAsync(id);
            if (appointmentModel == null) return NotFound($"Appointment {id} could not be found.");

            AppointmentDTO appointmentDTO;
            CompanySimpleDTO companyDTO;
            DepartmentDTO departmentDTO;
            WorkPeriodSimpleDTO workPeriodDTO;
            UserDTO ownerDTO;
            AddressDTO ownerAddressDTO;
            AddressDTO departmentAddressDTO;

            //Retrieve values
            appointmentDTO = _mapper.Map<AppointmentDTO>(appointmentModel);
            workPeriodDTO = _mapper.Map<WorkPeriodSimpleDTO>(await _unitOfWork.WorkPeriods.GetByIdAsync(appointmentModel.WorkPeriodId));
            departmentDTO = _mapper.Map<DepartmentDTO>(await _unitOfWork.Departments.GetByIdAsync(workPeriodDTO.DepartmentId));
            ownerDTO = _mapper.Map<UserDTO>(await _unitOfWork.Users.GetByIdAsync(appointmentDTO.OwnerId));
            ownerAddressDTO = _mapper.Map<AddressDTO>(await _unitOfWork.Addresses.GetByIdAsync(ownerDTO.AddressId));
            companyDTO = _mapper.Map<CompanySimpleDTO>(await _unitOfWork.Companies.GetByIdAsync(departmentDTO.CompanyId));
            departmentAddressDTO = _mapper.Map<AddressDTO>(await _unitOfWork.Addresses.GetByIdAsync(departmentDTO.AddressId));

            //Set values
            ownerDTO.Address = ownerAddressDTO;
            workPeriodDTO.Department = departmentDTO;
            workPeriodDTO.Department.Company = companyDTO;
            workPeriodDTO.Department.Address = departmentAddressDTO;
            appointmentDTO.WorkPeriod = workPeriodDTO;
            appointmentDTO.Owner = ownerDTO;

            return Ok(appointmentDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AppointmentInDTO appointmentDTO)
        {
            if (!ModelState.IsValid || appointmentDTO == null) return BadRequest("Please fill out the required fields.");

            var appointmentModel = _mapper.Map<Appointment>(appointmentDTO);
            var existingUserWorkPeriod = await _unitOfWork.UserWorkPeriods.FindAsync(u => u.UserId == appointmentDTO.OwnerId && u.WorkPeriodId == appointmentDTO.WorkPeriodId);

            if (existingUserWorkPeriod == null)
            {
                var userPeriodModel = _mapper.Map<UserWorkPeriod>(new UserWorkPeriodDTO(appointmentDTO.OwnerId, appointmentDTO.WorkPeriodId));
                await _unitOfWork.UserWorkPeriods.AddAsync(userPeriodModel);
            }

            await _unitOfWork.Appointments.AddAsync(appointmentModel);
            await _unitOfWork.CompleteAsync();

            appointmentDTO = _mapper.Map<AppointmentInDTO>(appointmentModel);

            return CreatedAtRoute("GetAppointment", new { id = appointmentDTO.Id }, appointmentDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, AppointmentInDTO appointmentDTO)
        {
            if (appointmentDTO == null) return BadRequest();

            var existing = await _unitOfWork.Appointments.GetByIdAsync(id);
            if (existing == null) return NotFound($"Appointment {id} could not be found");

            var newAppointmentModel = _mapper.Map<Appointment>(appointmentDTO);
            await _unitOfWork.Appointments.UpdateAsync(id, newAppointmentModel);
            await _unitOfWork.CompleteAsync();

            appointmentDTO = _mapper.Map<AppointmentInDTO>(existing);

            return Ok(appointmentDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);
            if (appointment == null) return NoContent();

            _unitOfWork.Appointments.Remove(appointment);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
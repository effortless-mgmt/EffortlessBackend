using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class AppointmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppointmentController(EffortlessContext context, IMapper mapper)
        {
            _unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            IEnumerable<Appointment> appointments = new List<Appointment>();
            var currentUser = await _unitOfWork.Users.GetByUsernameAsync(User.Identity.Name);

            switch(currentUser.PrimaryRole)
            {
                case PrimaryRoleType.Booker:
                    appointments = await _unitOfWork.Appointments.GetAllAsync();
                    break;
                case PrimaryRoleType.Substitute:
                    // Substitutes should only be able so see their own appointments
                    appointments = await _unitOfWork.Appointments.GetByUserIdAsync(currentUser.Id);
                    break;
                default:
                    throw new NotImplementedException("A user cannot be associated with a company, so this does not work yet.");
            }

            return Ok(_mapper.Map<IEnumerable<AppointmentUserDTO>>(appointments).OrderBy(a => a.Start));
        }

        [Authorize]
        [HttpGet("available")]
        public async Task<IActionResult> GetAvilableAsync()
        {
            var currentUserTask = _unitOfWork.Users.GetByUsernameAsync(User.Identity.Name);

            var availableAppointments = await _unitOfWork.Appointments.FindAsync(appointment => appointment.Owner == null);
            var availableAppointmentsAssignedToUser = new List<AppointmentUserDTO>();
            
            // Following code could replace the foreach loop below
            // var availableAppointmentsAssignedToUser = availableAppointments.Where(
            //     appointment => appointment.WorkPeriod.AssignedUsers.Any(
            //         user => user.Id == currentUser.Id
            //     )
            // );
            
            foreach (var appointment in availableAppointments)
            {
                var currentUser = await currentUserTask;
                var workperiod = await _unitOfWork.WorkPeriods.GetByIdAsync(appointment.WorkPeriodId);

                // A simply hack to make it work...
                IEnumerable<UserWorkPeriod> userWorkPeriods = await _unitOfWork.UserWorkPeriods.FindAsync(uwp => uwp.WorkPeriodId == appointment.WorkPeriodId);
                workperiod.UserWorkPeriods = new List<UserWorkPeriod>(userWorkPeriods);

                if (workperiod.AssignedUsers.Any(user => user.Id == currentUser.Id))
                {
                    availableAppointmentsAssignedToUser.Add(_mapper.Map<AppointmentUserDTO>(appointment));
                }
            }

            return Ok(availableAppointmentsAssignedToUser.OrderBy(a => a.Start));
        }

        [Authorize]
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
            // AddressDTO ownerAddressDTO;
            AddressDTO departmentAddressDTO;

            //Retrieve values
            appointmentDTO = _mapper.Map<AppointmentDTO>(appointmentModel);
            workPeriodDTO = _mapper.Map<WorkPeriodSimpleDTO>(await _unitOfWork.WorkPeriods.GetByIdAsync(appointmentModel.WorkPeriodId));
            departmentDTO = _mapper.Map<DepartmentDTO>(await _unitOfWork.Departments.GetByIdAsync(workPeriodDTO.DepartmentId));
            var owner = appointmentDTO.OwnerId == null ? null : await _unitOfWork.Users.GetByIdAsync(appointmentDTO.OwnerId ?? -1);
            ownerDTO = _mapper.Map<UserDTO>(owner);
            // ownerDTO = _mapper.Map<UserDTO>(await _unitOfWork.Users.GetByIdAsync(appointmentDTO.OwnerId));
            // ownerAddressDTO = _mapper.Map<AddressDTO>(await _unitOfWork.Addresses.GetByIdAsync(ownerDTO.AddressId));
            companyDTO = _mapper.Map<CompanySimpleDTO>(await _unitOfWork.Companies.GetByIdAsync(departmentDTO.CompanyId));
            departmentAddressDTO = _mapper.Map<AddressDTO>(await _unitOfWork.Addresses.GetByIdAsync(departmentDTO.AddressId));

            //Set values
            // ownerDTO.Address = ownerAddressDTO;
            workPeriodDTO.Department = departmentDTO;
            workPeriodDTO.Department.Company = companyDTO;
            workPeriodDTO.Department.Address = departmentAddressDTO;
            appointmentDTO.WorkPeriod = workPeriodDTO;
            appointmentDTO.Owner = ownerDTO ?? null;

            return Ok(appointmentDTO);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AppointmentInDTO appointmentDTO)
        {
            if (!ModelState.IsValid || appointmentDTO == null) return BadRequest("Please fill out the required fields.");

            var appointmentModel = _mapper.Map<Appointment>(appointmentDTO);
            var existingUserWorkPeriod = await _unitOfWork.UserWorkPeriods.FindAsync(u => u.UserId == appointmentDTO.OwnerId && u.WorkPeriodId == appointmentDTO.WorkPeriodId);

            if (existingUserWorkPeriod.Count() == 0 && appointmentDTO.OwnerId != null)
            {
                var userPeriodModel = _mapper.Map<UserWorkPeriod>(new UserWorkPeriodDTO(appointmentDTO.OwnerId ?? 0, appointmentDTO.WorkPeriodId));
                await _unitOfWork.UserWorkPeriods.AddAsync(userPeriodModel);
            }

            await _unitOfWork.Appointments.AddAsync(appointmentModel);
            await _unitOfWork.CompleteAsync();

            appointmentDTO = _mapper.Map<AppointmentInDTO>(appointmentModel);

            return CreatedAtRoute("GetAppointment", new { id = appointmentDTO.Id }, appointmentDTO);
        }

        [Authorize]
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

        [Authorize]
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
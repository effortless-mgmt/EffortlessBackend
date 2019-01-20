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

            var availableAppointments = await _unitOfWork.Appointments.FindNoOwnerAsync(appointment => appointment.Owner == null);
            var availableAppointmentsAssignedToUser = new List<AppointmentUserDTO>();
            
            // Following code could replace the foreach loop below
            // var availableAppointmentsAssignedToUser = availableAppointments.Where(
            //     appointment => appointment.WorkPeriod.AssignedUsers.Any(
            //         user => user.Id == currentUser.Id
            //     )
            // );
            
            // Loop through all future available appointments 
            foreach (var appointment in availableAppointments.Where(app => app.Stop > DateTime.Now))
            {
                var currentUser = await currentUserTask;
                var workperiod = await _unitOfWork.WorkPeriods.GetByIdAsync(appointment.WorkPeriodId);

                // A simple hack to make it work...
                IEnumerable<UserWorkPeriod> userWorkPeriods = await _unitOfWork.UserWorkPeriods.FindAsync(
                    uwp => uwp.WorkPeriodId == appointment.WorkPeriodId);
                workperiod.UserWorkPeriods = new List<UserWorkPeriod>(userWorkPeriods);

                // Add 
                // if (workperiod.AssignedUsers.Any(user => user.Id == currentUser.Id))
                // {
                //     availableAppointmentsAssignedToUser.Add(_mapper.Map<AppointmentUserDTO>(appointment));
                // }
                foreach (var user in workperiod.AssignedUsers)
                {
                    if (user != null && user.Id == currentUser.Id)
                    {
                        availableAppointmentsAssignedToUser.Add(_mapper.Map<AppointmentUserDTO>(appointment));
                    }
                }
            }

            return Ok(availableAppointmentsAssignedToUser.OrderBy(a => a.Start));
        }

        [Authorize]
        [HttpGet("unapproved/substitute")]
        public async Task<IActionResult> GetUnapprovedBySubstituteAsync()
        {
            var currentUser = await _unitOfWork.Users.GetByUsernameAsync(User.Identity.Name);

            var unapprovedAppointments = await _unitOfWork.Appointments.FindAsync(appointment => 
                appointment.OwnerId == currentUser.Id &&
                appointment.Stop < DateTime.Now &&
                appointment.ApprovedByOwner == false
            );

            return Ok(_mapper.Map<IEnumerable<AppointmentUserDTO>>(unapprovedAppointments).OrderBy(app => app.Start));
        }

        [Authorize]
        [HttpPut("{id}/approve")]
        public async Task<IActionResult> ApproveAppointmentAsync(int id, AppointmentInDTO appointmentDtoToApprove)
        {
            var currentUser = await _unitOfWork.Users.GetByUsernameAsync(User.Identity.Name);
            var appointmentTask = Task.Run(() => _unitOfWork.Appointments.GetByIdAsync(id));

            if (currentUser.PrimaryRole == PrimaryRoleType.Client)
            {
                return Forbid("Clients cannot approve appointments. Please contact the administration and make them approve the wanted appointment.");
            }

            var appointment = await appointmentTask;
            // var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);

            if (appointment == null) 
            {
                return NotFound();
            }

            if (appointment.OwnerId == null) 
            {
                return BadRequest("Appointments without an assigned user cannot be approved.");
            }
            
            if (currentUser.PrimaryRole == PrimaryRoleType.Booker)
            {
                if (appointment.ApprovedByOwner == false) 
                {
                    return BadRequest($"Substitute {appointment.Owner.FirstName} {appointment.Owner.LastName} (id: {appointment.Owner.Id}) have not " +
                    "yet approved the appointment. You cannot approve an appointment that the owner have not approved yet. Consider sending them a reminder.");
                }

                appointment.ApprovedBy = currentUser;
                appointment.ApprovedByUserId = currentUser.Id;
                appointment.ApprovedDate = DateTime.Now;
            }
            else if (currentUser.PrimaryRole == PrimaryRoleType.Substitute)
            {
                appointment.ApprovedByOwner = true;
                appointment.ApprovedByOwnerDate = DateTime.Now;

                if (appointmentDtoToApprove != null)
                {
                    var appointmentToApprove = _mapper.Map<Appointment>(appointmentDtoToApprove);
                    await _unitOfWork.Appointments.UpdateAsync(id, appointmentToApprove);
                }
            }

            await _unitOfWork.CompleteAsync();

            return Ok(_mapper.Map<AppointmentUserDTO>(appointment));
        }

        [Authorize]
        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcomingAsync(int? limit)
        {
            var currentUser = await _unitOfWork.Users.GetByUsernameAsync(User.Identity.Name);
            var upcomingAppointments = await _unitOfWork.Appointments.FindAsync(appointment =>
                appointment.Start > DateTime.Now &&
                appointment.OwnerId == currentUser.Id
            );

            var appointments = _mapper.Map<IEnumerable<AppointmentUserDTO>>(upcomingAppointments).OrderBy(appointment => appointment.Start);

            if (limit != null && limit > 0)
            {
                return Ok(appointments.Take(limit ?? 1));
            }
            else
            {
                return Ok(appointments);
            }
        }

        [Authorize]
        [HttpPut("{id}/claim")]
        public async Task<IActionResult> ClaimAppointmentAsync(int id)
        {
            var currentUserTask = Task.Run(() => _unitOfWork.Users.GetByUsernameAsync(User.Identity.Name));
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            var currentUser = await currentUserTask;

            if (currentUser.PrimaryRole != PrimaryRoleType.Substitute)
            {
                return Forbid("Only substitutes can claim appointments.");
            }

            if (appointment.OwnerId != null)
            {
                return BadRequest("Appointment is already claimed by another user.");
            }

            if (appointment.Start < DateTime.Now)
            {
                return BadRequest("Cannot claim appointments in the past.");
            }

            appointment.Owner = currentUser;
            appointment.OwnerId = currentUser.Id;

            await _unitOfWork.CompleteAsync();

            return Ok(_mapper.Map<AppointmentUserDTO>(appointment));
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
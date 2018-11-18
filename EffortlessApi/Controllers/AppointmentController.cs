using System.Threading.Tasks;
using EffortlessApi.Core;
using EffortlessApi.Core.Models;
using EffortlessApi.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace EffortlessApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AppointmentController(EffortlessContext context) => _unitOfWork = new UnitOfWork(context);

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var appointments = await _unitOfWork.Appointments.GetAllAsync();

            if (appointments == null) return NotFound();

            return Ok(appointments);
        }

        [HttpGet("{appointmentId}", Name = "Getappointment")]
        public async Task<IActionResult> GetById(long appointmentId)
        {
            var appointment = await _unitOfWork.Addresses.GetByIdAsync(appointmentId);

            if (appointment == null)
            {
                return NotFound($"Appointment {appointmentId} could not be found.");
            }

            return Ok(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Appointment appointment)
        {
            if (appointment == null)
            {
                return BadRequest();
            }

            await _unitOfWork.Appointments.AddAsync(appointment);
            await _unitOfWork.CompleteAsync();

            return CreatedAtRoute("GetAppointment", new { appointmentId = appointment.Id }, appointment);
        }

        [HttpPut("{appointmentId}")]
        public async Task<IActionResult> Put(long appointmentId, [FromBody] Appointment appointment)
        {
            var existingAppointment = await _unitOfWork.Appointments.GetByIdAsync(appointmentId);

            if (existingAppointment == null) return NotFound($"department {appointmentId} could not be found.");

            await _unitOfWork.Appointments.UpdateAsync(appointmentId, appointment);
            await _unitOfWork.CompleteAsync();

            return Ok(existingAppointment);
        }

        [HttpDelete("{appointmentId}")]
        public async Task<IActionResult> Delete(long appointmentId)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(appointmentId);

            if (appointment == null)
            {
                return NoContent();
            }

            _unitOfWork.Appointments.Remove(appointment);

            await _unitOfWork.CompleteAsync();

            return Ok(appointment);
        }

    }
}
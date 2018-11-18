using System.Threading.Tasks;
using EffortlessApi.Core.Models;
using EffortlessApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EffortlessApi.Persistence.Repositories
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(DbContext context) : base(context)
        {
        }
        public EffortlessContext EffortlessContext
        {
            get { return _context as EffortlessContext; }
        }

        public async Task UpdateAsync(long appointmentID, Appointment newAppointment)
        {
            var appointmentToEdit = await GetByIdAsync(appointmentID);
            appointmentToEdit.Owner = newAppointment.Owner;
            appointmentToEdit.OwnerId = newAppointment.OwnerId;
            appointmentToEdit.Start = newAppointment.Start;
            appointmentToEdit.Stop = newAppointment.Stop;
            appointmentToEdit.TemporaryWorkPeriod = newAppointment.TemporaryWorkPeriod;
            appointmentToEdit.TemporaryWorkPeriodId = newAppointment.TemporaryWorkPeriodId;
            appointmentToEdit.ApprovedBy = newAppointment.ApprovedBy;
            appointmentToEdit.ApprovedByOwner = newAppointment.ApprovedByOwner;
            appointmentToEdit.ApprovedByOwnerDate = newAppointment.ApprovedByOwnerDate;
            appointmentToEdit.ApprovedByUserId = newAppointment.ApprovedByUserId;
            appointmentToEdit.ApprovedDate = newAppointment.ApprovedDate;
            appointmentToEdit.Break = newAppointment.Break;
            // appointmentToEdit.BreakTimeSpan = newAppointment.BreakTimeSpan;
            appointmentToEdit.CreatedBy = newAppointment.CreatedBy;
            appointmentToEdit.CreatedByUserId = newAppointment.CreatedByUserId;
            appointmentToEdit.CreatedDate = newAppointment.CreatedDate;
            appointmentToEdit.Earnings = newAppointment.Earnings;

            _context.Set<Appointment>().Update(appointmentToEdit);


        }
    }
}
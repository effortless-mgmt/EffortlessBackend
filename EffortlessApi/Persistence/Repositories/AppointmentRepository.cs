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

        public EffortlessContext effortlessContext
        {
            get { return _context as EffortlessContext; }
        }

        public override async Task UpdateAsync(Appointment newAppointment)
        {
            await UpdateAsync(newAppointment.Id, newAppointment);
        }

        public async Task UpdateAsync(long id, Appointment newAppointment)
        {
            var oldAppointment = await GetByIdAsync(id);

            oldAppointment.Start = newAppointment.Start;
            oldAppointment.Stop = newAppointment.Stop;
            oldAppointment.Break = newAppointment.Break;
            oldAppointment.OwnerId = newAppointment.OwnerId;
            oldAppointment.ApprovedByUserId = newAppointment.ApprovedByUserId;
            oldAppointment.ApprovedDate = newAppointment.ApprovedDate;
            oldAppointment.ApprovedByOwner = newAppointment.ApprovedByOwner;
            oldAppointment.ApprovedByOwnerDate = newAppointment.ApprovedByOwnerDate;
            oldAppointment.Earnings = newAppointment.Earnings;

            _context.Set<Appointment>().Update(oldAppointment);
        }
    }
}
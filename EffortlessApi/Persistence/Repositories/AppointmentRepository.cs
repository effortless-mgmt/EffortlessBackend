using System.Collections.Generic;
using System.Linq;
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

        public override async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await _context.Set<Appointment>()
                .Include(a => a.Owner)
                .Include(a => a.WorkPeriod)
                    .ThenInclude(wp => wp.Department)
                    .ThenInclude(d => d.Address)
                .Include(a => a.ApprovedBy)
                .ToListAsync();
        }

        public override async Task<Appointment> GetByIdAsync(long id)
        {
            return await _context.Set<Appointment>()
                .Include(a => a.Owner)
                .Include(a => a.WorkPeriod)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Appointment>> GetByWorkPeriodId(long id)
        {
            return await EffortlessContext.Appointments
                .Where(a => a.WorkPeriodId == id)
                    .Include(a => a.Owner)
                    .Include(a => a.WorkPeriod)
                        .ThenInclude(wp => wp.Department)
                        .ThenInclude(d => d.Address)
                    .Include(a => a.ApprovedBy)
                    .ToListAsync();

        }
        public async Task<IEnumerable<Appointment>> GetByUserIdAsync(long ownerId)
        {
            return await _context.Set<Appointment>()
                    .Where(a => a.OwnerId == ownerId)
                    .Include(a => a.WorkPeriod)
                        .ThenInclude(wp => wp.Department)
                        .ThenInclude(d => d.Address)
                    .Include(a => a.ApprovedBy)
                    .ToListAsync();
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
            // oldAppointment.OwnerId = newAppointment.OwnerId;
            // oldAppointment.ApprovedByUserId = newAppointment.ApprovedByUserId;
            // oldAppointment.ApprovedDate = newAppointment.ApprovedDate;
            // oldAppointment.ApprovedByOwner = newAppointment.ApprovedByOwner;
            // oldAppointment.ApprovedByOwnerDate = newAppointment.ApprovedByOwnerDate;
            // oldAppointment.Earnings = newAppointment.Earnings;

            _context.Set<Appointment>().Update(oldAppointment);
        }

    }
}
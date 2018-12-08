using System.Collections.Generic;
using System.Threading.Tasks;
using EffortlessApi.Core.Models;
using EffortlessApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EffortlessApi.Persistence.Repositories
{
    public class WorkPeriodRepository : Repository<WorkPeriod>, IWorkPeriodRepository
    {
        public WorkPeriodRepository(DbContext context) : base(context) { }

        public EffortlessContext EffortlessContext
        {
            get { return _context as EffortlessContext; }
        }

        public override async Task<WorkPeriod> GetByIdAsync(long id)
        {
            return await _context.Set<WorkPeriod>()
                .Include(wp => wp.UserWorkPeriods)
                .Include(wp => wp.Department).ThenInclude(d => d.Address)
                .Include(wp => wp.Department.Company)
                .Include(wp => wp.Agreement)
                .Include(wp => wp.Appointments).ThenInclude(a => a.Owner)
                .FirstOrDefaultAsync(wp => wp.Id == id);
        }

        public async Task UpdateAsync(long id, WorkPeriod newWorkPeriod)
        {
            var workPeriodToEdit = await GetByIdAsync(id);

            workPeriodToEdit.AgreementId = newWorkPeriod.AgreementId;
            workPeriodToEdit.Name = newWorkPeriod.Name;
            workPeriodToEdit.Start = newWorkPeriod.Start;

            _context.Set<WorkPeriod>().Update(workPeriodToEdit);
        }
        public override async Task UpdateAsync(WorkPeriod newWorkPeriod)
        {
            await UpdateAsync(newWorkPeriod.Id, newWorkPeriod);
        }

    }
}
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
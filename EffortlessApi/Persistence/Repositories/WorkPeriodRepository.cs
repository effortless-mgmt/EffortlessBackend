using System.Threading.Tasks;
using EffortlessApi.Core.Models;
using EffortlessApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EffortlessApi.Persistence.Repositories
{
    public class WorkPeriodRepository : Repository<WorkPeriod>, IWorkPeriodRepository
    {
        public WorkPeriodRepository(DbContext context) : base(context)
        {
        }

        public override Task UpdateAsync(WorkPeriod newEntity)
        {
            throw new System.NotImplementedException();
        }
    }
}
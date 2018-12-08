using System.Collections.Generic;
using System.Threading.Tasks;
using EffortlessApi.Core.Models;

namespace EffortlessApi.Core.Repositories
{
    public interface IWorkPeriodRepository : IRepository<WorkPeriod>
    {
        Task UpdateAsync(long id, WorkPeriod newWorkPeriod);
    }
}
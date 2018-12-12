using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EffortlessApi.Core.Models;

namespace EffortlessApi.Core.Repositories
{
    public interface IUserWorkPeriodRepository : IRepository<UserWorkPeriod>
    {
        Task<UserWorkPeriod> GetByIdAsync(long userId, long workPeriodId);
        Task<IEnumerable<UserWorkPeriod>> GetByWorkPeriodId(long id);
    }
}
using System.Threading.Tasks;
using EffortlessApi.Core.Models;
using EffortlessApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EffortlessApi.Persistence.Repositories
{
    public class UserWorkPeriodRepository : Repository<UserWorkPeriod>, IUserWorkPeriodRepository
    {
        public UserWorkPeriodRepository(DbContext context) : base(context)
        {
        }

        public async Task<UserWorkPeriod> GetByIdAsync(long userID, long workPeriodId)
        {
            return await _context.Set<UserWorkPeriod>()
                .Include(uwp => uwp.User)
                .FirstOrDefaultAsync(uw => uw.UserId == userID && uw.WorkPeriodId == workPeriodId);
        }
        public EffortlessContext EffortlessContext
        {
            get { return _context as EffortlessContext; }
        }

        public override Task UpdateAsync(UserWorkPeriod newEntity)
        {
            throw new System.NotImplementedException();
        }
    }
}
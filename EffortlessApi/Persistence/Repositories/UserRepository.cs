using System.Threading.Tasks;
using EffortlessApi.Core.Repositories;
using EffortlessApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EffortlessApi.Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public EffortlessContext EffortlessContext
        {
            get { return _context as EffortlessContext; }
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Set<User>().SingleOrDefaultAsync(user => user.UserName == username);
        }
    }
}
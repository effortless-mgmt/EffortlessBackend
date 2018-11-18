using System.Threading.Tasks;
using EffortlessApi.Core.Models;

namespace EffortlessApi.Core.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task UpdateAsync(long id, Role newRole);
        Task<Role> GetByIdWithUsersAsync(long roleId);
    }
}
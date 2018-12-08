using System.Threading.Tasks;
using EffortlessApi.Core.Models;

namespace EffortlessApi.Core.Repositories
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        Task<UserRole> GetByIdAsync(long userId, long roleId);
        Task UpdateAsync(long userId, long roleId, UserRole newUserRole);
    }
}
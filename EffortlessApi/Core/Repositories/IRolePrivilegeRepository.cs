using System.Threading.Tasks;
using EffortlessApi.Core.Models;

namespace EffortlessApi.Core.Repositories
{
    public interface IRolePrivilegeRepository : IRepository<RolePrivilege>
    {
        Task<RolePrivilege> GetByIdAsync(long roleId, long privilegeId);
        Task UpdateAsync(long roleId, long privilegeid, RolePrivilege newRolePrivilege);
    }
}
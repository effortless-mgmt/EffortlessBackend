using System.Threading.Tasks;
using EffortlessApi.Core.Models;

namespace EffortlessApi.Core.Repositories
{
    public interface IPrivilegeRepository : IRepository<Privilege>
    {
        Task UpdateAsync(long privilegeId, Privilege newPrivilege);
    }
}
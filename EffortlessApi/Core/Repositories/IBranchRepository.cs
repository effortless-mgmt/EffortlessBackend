using System.Threading.Tasks;
using EffortlessApi.Core.Models;

namespace EffortlessApi.Core.Repositories
{
    public interface IBranchRepository : IRepository<Branch>
    {
        Task UpdateAsync(long branchId, Branch newBranch);

    }
}
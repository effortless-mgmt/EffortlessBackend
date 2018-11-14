using System.Threading.Tasks;
using EffortlessApi.Core.Models;
using EffortlessApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EffortlessApi.Persistence.Repositories
{
    public class BranchRepository : Repository<Branch>, IBranchRepository
    {
        public BranchRepository(DbContext context) : base(context)
        {
        }
        public EffortlessContext EffortlessContext
        {
            get { return _context as EffortlessContext; }
        }

        public async Task UpdateAsync(long branchId, Branch newBranch)
        {
            var branchToEdit = await GetByIdAsync(branchId);
            branchToEdit.Name = newBranch.Name;
            branchToEdit.CompanyId = newBranch.CompanyId;

            _context.Set<Branch>().Update(branchToEdit);
        }
    }
}
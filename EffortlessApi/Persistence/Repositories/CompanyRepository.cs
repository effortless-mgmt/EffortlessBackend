using System.Threading.Tasks;
using EffortlessApi.Core.Models;
using EffortlessApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EffortlessApi.Persistence.Repositories
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(DbContext context) : base(context)
        {
        }

        public EffortlessContext EffortlessContext
        {
            get { return _context as EffortlessContext; }
        }
        public async Task UpdateAsync(long companyId, Company newCompany)
        {
            var companyToEdit = await GetByIdAsync(companyId);

            companyToEdit.Name = newCompany.Name;

            _context.Set<Company>().Update(companyToEdit);
        }
    }
}
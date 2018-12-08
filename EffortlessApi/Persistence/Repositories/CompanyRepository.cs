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

        public async override Task<Company> GetByIdAsync(long id)
        {
            return await _context.Set<Company>()
                .Include(c => c.Address)
                .Include(c => c.ParentCompany)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Company> FindByVat(int vat)
        {
            return await _context.Set<Company>().FirstOrDefaultAsync(c => c.Vat == vat);
        }

        public async Task UpdateAsync(long companyId, Company newCompany)
        {
            var companyToEdit = await GetByIdAsync(companyId);

            companyToEdit.Name = newCompany.Name;

            if (newCompany.ParentCompanyId != 0)
            {
                companyToEdit.ParentCompanyId = newCompany.ParentCompanyId;
            }

            if (newCompany.AddressId != 0)
            {
                companyToEdit.AddressId = newCompany.AddressId;
            }

            _context.Set<Company>().Update(companyToEdit);
        }

        public override async Task UpdateAsync(Company newCompany)
        {
            await UpdateAsync(newCompany.Id, newCompany);
        }
    }
}
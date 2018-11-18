using System.Threading.Tasks;
using EffortlessApi.Core.Models;

namespace EffortlessApi.Core.Repositories
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task UpdateAsync(long companyId, Company newCompany);

    }
}
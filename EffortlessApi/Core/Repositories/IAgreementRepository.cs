using System.Threading.Tasks;
using EffortlessApi.Core.Models;

namespace EffortlessApi.Core.Repositories
{
    public interface IAgreementRepository : IRepository<Agreement>
    {
        Task UpdateAsync(long id, Agreement newAgreement);
    }
}
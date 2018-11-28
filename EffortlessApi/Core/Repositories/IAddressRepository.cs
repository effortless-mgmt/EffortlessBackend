using System.Threading.Tasks;
using EffortlessApi.Core.Models;

namespace EffortlessApi.Core.Repositories
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task UpdateAsync(long id, Address newAddress);
        Task<Address> GetByIdAsync(long? id);
    }
}
using System.Threading.Tasks;
using EffortlessApi.Core.Models;
using EffortlessApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EffortlessApi.Persistence.Repositories
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(DbContext context) : base(context)
        {
        }

        public EffortlessContext EffortlessContext
        {
            get { return _context as EffortlessContext; }
        }

        public Task<Address> GetByIdAsync(long? id)
        {
            return id == null ? null : _context.Set<Address>().FindAsync(id);
        }


        public async Task UpdateAsync(long id, Address newAddress)
        {
            var addressToEdit = await GetByIdAsync(id);

            addressToEdit.Street = newAddress.Street;
            addressToEdit.No = newAddress.No;
            addressToEdit.Floor = newAddress.Floor;
            addressToEdit.Side = newAddress.Side;
            addressToEdit.ZipCode = newAddress.ZipCode;
            addressToEdit.City = newAddress.City;
            addressToEdit.Country = newAddress.Country;

            _context.Set<Address>().Update(addressToEdit);
        }

        public override async Task UpdateAsync(Address newAddress)
        {
            await UpdateAsync(newAddress.Id, newAddress);
        }
    }
}
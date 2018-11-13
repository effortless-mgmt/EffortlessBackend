using System.Threading.Tasks;
using EffortlessApi.Core;
using EffortlessApi.Core.Repositories;
using EffortlessApi.Persistence.Repositories;

namespace EffortlessApi.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EffortlessContext _context;

        public UnitOfWork(EffortlessContext context) 
        {
            _context = context;
            Users = new UserRepository(_context);
            Addresses = new AddressRepository(_context);
        }

        public IUserRepository Users { get; private set; }
        public IAddressRepository Addresses { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
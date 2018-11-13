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

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
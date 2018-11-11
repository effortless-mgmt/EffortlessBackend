using EffortlessApi.Models;

namespace EffortlessApi.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EffortlessContext _context;

        public UnitOfWork(EffortlessContext context) 
        {
            _context = context;
            Users = new UserRepository(_context);
        }

        public IUserRepository Users { get; private set; }

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
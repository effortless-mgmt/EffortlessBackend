using System.Threading.Tasks;
using EffortlessApi.Core.Models;
using EffortlessApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EffortlessApi.Persistence.Repositories
{
    public class AgreementRepository : Repository<Agreement>, IAgreementRepository
    {
        public AgreementRepository(DbContext context) : base(context)
        {
        }

        public EffortlessContext effortlessContext
        {
            get { return _context as EffortlessContext; }
        }

        /* Must not be implemented as agreements are final.
           The system user must manually create new agreements when new agreements are made. */
        public Task UpdateAsync(long id, Agreement agreement)
        {
            throw new System.NotImplementedException();
        }
    }
}
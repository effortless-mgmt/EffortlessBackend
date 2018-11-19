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

        /// <summary>
        /// Must not be implemented as agreements are final.
        /// The system user must manually create new agreements when new agreements are made.
        /// The below method is bound by an interface contract.
        /// <summary>
        public override Task UpdateAsync(Agreement newEntity) => throw new System.NotImplementedException();
    }
}
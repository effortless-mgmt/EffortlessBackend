using System.Threading.Tasks;
using EffortlessApi.Core.Models;
using EffortlessApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EffortlessApi.Persistence.Repositories
{
    public class PrivilegeRepository : Repository<Privilege>, IPrivilegeRepository
    {

        public PrivilegeRepository(DbContext context) : base(context) { }

        public EffortlessContext EffortlessContext
        {
            get { return _context as EffortlessContext; }
        }

        public async Task UpdateAsync(long privilegeId, Privilege newPrivilege)
        {
            var privilegeToEdit = await GetByIdAsync(privilegeId);

            privilegeToEdit.Name = newPrivilege.Name;

            _context.Set<Privilege>().Update(privilegeToEdit);
        }
    }
}
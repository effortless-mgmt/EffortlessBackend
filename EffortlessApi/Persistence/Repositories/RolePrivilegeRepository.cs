using System.Threading.Tasks;
using EffortlessApi.Core.Models;
using EffortlessApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EffortlessApi.Persistence.Repositories
{
    public class RolePrivilegeRepository : Repository<RolePrivilege>, IRolePrivilegeRepository
    {
        public RolePrivilegeRepository(DbContext context) : base(context)
        {
        }

        public async Task<RolePrivilege> GetByIdAsync(long roleId, long privilegeId)
        {
            return await _context.Set<RolePrivilege>()
                .Include(r => r.Role)
                .Include(r => r.Privilege)
                .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PrivilegeId == privilegeId);
        }

        /// <summary>
        /// As a RolePrivilege entity has a composite key of RoleId and PrivilegeId, the entity must be removed and re-added upon update due to key constraints.
        /// </summary>
        public async Task UpdateAsync(long roleId, long privilegeId, RolePrivilege newRolePrivilege)
        {
            var rolePrivilegeToEdit = await GetByIdAsync(roleId, privilegeId);
            _context.Set<RolePrivilege>().Remove(rolePrivilegeToEdit);
            await _context.SaveChangesAsync();

            rolePrivilegeToEdit.PrivilegeId = newRolePrivilege.PrivilegeId;

            await _context.Set<RolePrivilege>().AddAsync(rolePrivilegeToEdit);
        }

        public override async Task UpdateAsync(RolePrivilege newRolePrivilege)
        {
            await UpdateAsync(newRolePrivilege.RoleId, newRolePrivilege.PrivilegeId, newRolePrivilege);
        }
    }
}
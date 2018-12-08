using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EffortlessApi.Core.Models;
using EffortlessApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EffortlessApi.Persistence.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(DbContext context) : base(context) { }

        public EffortlessContext EffortlessContext
        {
            get { return _context as EffortlessContext; }
        }

        public override async Task<Role> GetByIdAsync(long id)
        {
            return await _context.Set<Role>()
                .Include(r => r.RolePrivileges)
                .ThenInclude(rp => rp.Privilege)
                .Include(r => r.UserRoles)
                .ThenInclude(ur => ur.User)
                .SingleOrDefaultAsync(r => r.Id == id);
        }

        /// <summary>
        /// Gets the role including all users attached to the role.
        /// </summary>
        /// <param name="roleId">The id of the role</param>
        /// <returns>Role specified by <see cref="id"/> with all users.</returns>
        public async Task<Role> GetByIdWithUsersAsync(long roleId)
        {
            return await _context.Set<Role>()
                .Include(r => r.RolePrivileges)
                .ThenInclude(rp => rp.Privilege)
                .Include(r => r.UserRoles)
                .ThenInclude(ur => ur.User)
                .SingleOrDefaultAsync(r => r.Id == roleId);
        }

        public override async Task UpdateAsync(Role newRole)
        {
            var existingRole = await GetByIdAsync(newRole.Id);

            existingRole.Name = newRole.Name;

            _context.Set<Role>().Update(existingRole);
        }

        public async Task UpdateAsync(long id, Role newRole)
        {
            var roleToEdit = await GetByIdAsync(id);

            roleToEdit.Name = newRole.Name;

            _context.Set<Role>().Update(roleToEdit);
        }
    }
}
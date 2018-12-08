using System.Threading.Tasks;
using EffortlessApi.Core.Models;
using EffortlessApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EffortlessApi.Persistence.Repositories
{
    public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(DbContext context) : base(context)
        {
        }

        public async Task<UserRole> GetByIdAsync(long userId, long roleId)
        {
            //         return await _context.Set<Role>()
            // .Include(r => r.RolePrivileges)
            // .ThenInclude(rp => rp.Privilege)
            // .SingleOrDefaultAsync(r => r.Id == id);
            return await _context.Set<UserRole>().FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        }

        public async Task UpdateAsync(long userId, long roleId, UserRole newUserRole)
        {
            var userRoleToEdit = await GetByIdAsync(userId, roleId);

            userRoleToEdit.RoleId = newUserRole.RoleId;

            _context.Set<UserRole>().Update(userRoleToEdit);
        }
        public override async Task UpdateAsync(UserRole newUserRole)
        {
            await UpdateAsync(newUserRole.UserId, newUserRole.RoleId, newUserRole);
        }
    }
}
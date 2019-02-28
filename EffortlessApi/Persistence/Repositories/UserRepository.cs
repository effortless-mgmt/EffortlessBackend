using System.Collections.Generic;
using System.Threading.Tasks;
using EffortlessApi.Core.Models;
using EffortlessApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EffortlessApi.Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context) { }

        public EffortlessContext EffortlessContext
        {
            get { return _context as EffortlessContext; }
        }


        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Set<User>().Include(u => u.Address).ToListAsync();
        }

        public async Task<User> GetByUsernameAsync(string userName)
        {
            return await _context.Set<User>()
                .Include(u => u.Address)
                .Include(u => u.UserRoles)
                .ThenInclude(userRole => userRole.Role)
                .ThenInclude(role => role.RolePrivileges)
                .ThenInclude(rolePrivilege => rolePrivilege.Privilege)
                .SingleOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task UpdateAsync(string userName, User newUser)
        {
            var userToEdit = await GetByUsernameAsync(userName);

            userToEdit.FirstName = newUser.FirstName ?? userToEdit.FirstName;
            userToEdit.LastName = newUser.LastName ?? userToEdit.LastName;
            userToEdit.Address = newUser.Address ?? userToEdit.Address;
            userToEdit.Password = newUser.Password ?? userToEdit.Password;
            userToEdit.Email = newUser.Email ?? userToEdit.Email;
            userToEdit.Phone = newUser.Phone ?? userToEdit.Phone;
            userToEdit.ProfilePictureUrl = newUser.ProfilePictureUrl ?? userToEdit.ProfilePictureUrl;

            _context.Set<User>().Update(userToEdit);
        }

        public override async Task UpdateAsync(User newUser)
        {
            await UpdateAsync(newUser.UserName, newUser);
        }
    }
}
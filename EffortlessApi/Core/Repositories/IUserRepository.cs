using System.Threading.Tasks;
using EffortlessApi.Core.Models;

namespace EffortlessApi.Core.Repositories {
    public interface IUserRepository : IRepository<User> {
        Task<User> GetByUsernameAsync(string username);
    }
}
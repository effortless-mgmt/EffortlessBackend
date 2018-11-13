using System.Threading.Tasks;
using EffortlessApi.Models;

namespace EffortlessApi.Core.Repositories {
    public interface IUserRepository : IRepository<User> {
        Task<User> GetByUsernameAsync(string username);
    }
}
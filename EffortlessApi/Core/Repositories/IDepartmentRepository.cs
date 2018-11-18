using System.Threading.Tasks;
using EffortlessApi.Core.Models;

namespace EffortlessApi.Core.Repositories
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task UpdateAsync(long departmentId, Department newDepartment);

    }
}
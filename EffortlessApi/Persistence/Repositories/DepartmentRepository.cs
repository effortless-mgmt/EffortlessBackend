using System.Threading.Tasks;
using EffortlessApi.Core.Models;
using EffortlessApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EffortlessApi.Persistence.Repositories
{
    public class JobRepository : Repository<Department>, IDepartmentRepository
    {
        public JobRepository(DbContext context) : base(context)
        {
        }
        public EffortlessContext EffortlessContext
        {
            get { return _context as EffortlessContext; }
        }

        public async Task UpdateAsync(long departmentId, Department newDepartment)
        {
            var departmentToEdit = await GetByIdAsync(departmentId);

            departmentToEdit.Name = newDepartment.Name;
            departmentToEdit.AddressId = newDepartment.AddressId;
            departmentToEdit.CompanyId = newDepartment.CompanyId;

            _context.Set<Department>().Update(departmentToEdit);
        }
    }
}
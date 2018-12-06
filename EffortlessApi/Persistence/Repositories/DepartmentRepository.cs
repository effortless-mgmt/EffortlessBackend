using System.Threading.Tasks;
using EffortlessApi.Core.Models;
using EffortlessApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EffortlessApi.Persistence.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(DbContext context) : base(context)
        {
        }
        public EffortlessContext EffortlessContext
        {
            get { return _context as EffortlessContext; }
        }

        public async Task UpdateAsync(long id, Department newDepartment)
        {
            var departmentToEdit = await GetByIdAsync(id);

            departmentToEdit.Name = newDepartment.Name;

            if (newDepartment.AddressId != 0)
            {
                departmentToEdit.AddressId = newDepartment.AddressId;
            }

            _context.Set<Department>().Update(departmentToEdit);
        }

        public override async Task UpdateAsync(Department newDepartment)
        {
            await UpdateAsync(newDepartment.Id, newDepartment);
        }
    }
}
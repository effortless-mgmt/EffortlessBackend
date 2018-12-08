using System.Threading.Tasks;
using EffortlessApi.Core;
using EffortlessApi.Core.Repositories;
using EffortlessApi.Persistence.Repositories;

namespace EffortlessApi.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EffortlessContext _context;

        public UnitOfWork(EffortlessContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Addresses = new AddressRepository(_context);
            Agreements = new AgreementRepository(_context);
            Appointments = new AppointmentRepository(_context);
            Departments = new DepartmentRepository(_context);
            Companies = new CompanyRepository(_context);
            Privileges = new PrivilegeRepository(_context);
            Roles = new RoleRepository(_context);
            RolePrivileges = new RolePrivilegeRepository(_context);
            WorkPeriods = new WorkPeriodRepository(_context);
            UserRoles = new UserRoleRepository(_context);
            UserWorkPeriods = new UserWorkPeriodRepository(_context);

        }

        public IUserRepository Users { get; private set; }
        public IAddressRepository Addresses { get; private set; }
        public IAppointmentRepository Appointments { get; private set; }
        public IAgreementRepository Agreements { get; private set; }
        public IDepartmentRepository Departments { get; private set; }
        public ICompanyRepository Companies { get; private set; }
        public IPrivilegeRepository Privileges { get; private set; }
        public IRoleRepository Roles { get; private set; }
        public IRolePrivilegeRepository RolePrivileges { get; private set; }
        public IUserRoleRepository UserRoles { get; private set; }
        public IUserWorkPeriodRepository UserWorkPeriods { get; private set; }
        public IWorkPeriodRepository WorkPeriods { get; set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
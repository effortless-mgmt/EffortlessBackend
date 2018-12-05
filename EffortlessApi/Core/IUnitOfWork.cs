using System;
using System.Threading.Tasks;
using EffortlessApi.Core.Repositories;

namespace EffortlessApi.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IAddressRepository Addresses { get; }
        IAgreementRepository Agreements { get; }
        IAppointmentRepository Appointments { get; }
        IDepartmentRepository Departments { get; }
        ICompanyRepository Companies { get; }
        IPrivilegeRepository Privileges { get; }
        IRoleRepository Roles { get; }
        IUserWorkPeriodRepository UserWorkPeriods { get; }
        IWorkPeriodRepository WorkPeriods { get; }
        Task<int> CompleteAsync();
    }
}
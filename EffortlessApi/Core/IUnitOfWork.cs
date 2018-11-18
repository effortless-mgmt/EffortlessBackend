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
        IDepartmentRepository Department { get; }
        ICompanyRepository Companies { get; }
        IPrivilegeRepository Privileges { get; }
        Task<int> CompleteAsync();
    }
}
using System.Threading.Tasks;
using EffortlessApi.Core.Models;
using EffortlessApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EffortlessApi.Persistence.Repositories
{
    public class AgreementRepository : Repository<Agreement>, IAgreementRepository
    {
        public AgreementRepository(DbContext context) : base(context)
        {
        }

        public EffortlessContext effortlessContext
        {
            get { return _context as EffortlessContext; }
        }

        public async Task UpdateAsync(long id, Agreement newAgreement)
        {
            var agreementToEdit = await GetByIdAsync(id);

            agreementToEdit.Name = newAgreement.Name;
            agreementToEdit.Version = newAgreement.Version;
            agreementToEdit.UnitPrice = newAgreement.UnitPrice;
            agreementToEdit.Salary = newAgreement.Salary;
            agreementToEdit.NightSubsidy = newAgreement.NightSubsidy;
            agreementToEdit.WeekendSubsidy = newAgreement.WeekendSubsidy;
            agreementToEdit.HolidaySubsidy = newAgreement.HolidaySubsidy;
            agreementToEdit.IsBreakPaid = newAgreement.IsBreakPaid;

            _context.Set<Agreement>().Update(agreementToEdit);
        }

        public override async Task UpdateAsync(Agreement newAgreement)
        {
            await UpdateAsync(newAgreement.Id, newAgreement);
        }
    }
}
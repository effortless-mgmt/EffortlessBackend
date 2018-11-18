using EffortlessApi.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EffortlessApi.Persistence.EntityConfigurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasOne<Company>(c => c.Parent).WithMany().HasForeignKey(c => c.ParentCompanyId);
            builder.HasOne<Address>(c => c.Address).WithMany().HasForeignKey(c => c.AddressId);
        }
    }
}
using EffortlessApi.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EffortlessApi.Persistence.EntityConfigurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasOne<Company>(d => d.Company).WithMany().HasForeignKey(d => d.CompanyId);
            builder.HasOne<Address>(d => d.Address).WithMany().HasForeignKey(d => d.AddressId);
        }
    }
}
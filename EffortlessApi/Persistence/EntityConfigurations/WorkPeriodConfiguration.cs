using EffortlessApi.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EffortlessApi.Persistence.EntityConfigurations
{
    public class WorkPeriodConfiguration : IEntityTypeConfiguration<WorkPeriod>
    {
        public void Configure(EntityTypeBuilder<WorkPeriod> builder)
        {
            builder
                .HasMany<Appointment>()
                .WithOne(a => a.WorkPeriod)
                .HasForeignKey(a => a.WorkPeriodId);

            builder
                .HasOne<Department>(tw => tw.Department)
                .WithMany()
                .HasForeignKey(tw => tw.DepartmentId);
        }
    }
}
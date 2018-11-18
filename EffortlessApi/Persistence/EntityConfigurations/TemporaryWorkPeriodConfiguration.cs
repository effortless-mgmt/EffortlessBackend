using EffortlessApi.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EffortlessApi.Persistence.EntityConfigurations
{
    public class TemporaryWorkPeriodConfiguration : IEntityTypeConfiguration<TemporaryWorkPeriod>
    {
        public void Configure(EntityTypeBuilder<TemporaryWorkPeriod> builder)
        {
            builder.HasMany<Appointment>().WithOne(a => a.TemporaryWorkPeriod).HasForeignKey(a => a.TemporaryWorkPeriodId);
        }
    }
}
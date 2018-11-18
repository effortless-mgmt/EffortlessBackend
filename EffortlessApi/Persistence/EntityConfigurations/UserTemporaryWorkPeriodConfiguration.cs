using EffortlessApi.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EffortlessApi.Persistence.EntityConfigurations
{
    public class UserTemporaryWorkPeriodConfiguration : IEntityTypeConfiguration<UserTemporaryWorkPeriod>
    {
        public void Configure(EntityTypeBuilder<UserTemporaryWorkPeriod> builder)
        {
            builder.HasKey(ut => new { ut.UserId, ut.TemporaryWorkPeriodId });
            builder
                .HasOne<User>(ut => ut.User)
                .WithMany()
                .HasForeignKey(ut => ut.UserId);
            builder
                .HasOne<TemporaryWorkPeriod>(ut => ut.TemporaryWorkPeriod)
                .WithMany()
                .HasForeignKey(ut => ut.TemporaryWorkPeriodId);
        }
    }
}
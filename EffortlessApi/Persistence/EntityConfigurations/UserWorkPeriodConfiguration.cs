using EffortlessApi.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EffortlessApi.Persistence.EntityConfigurations
{
    public class UserWorkPeriodConfiguration : IEntityTypeConfiguration<UserWorkPeriod>
    {
        public void Configure(EntityTypeBuilder<UserWorkPeriod> builder)
        {
            builder.HasKey(ut => new { ut.UserId, ut.WorkPeriodId });
            builder
                .HasOne<User>(ut => ut.User)
                .WithMany()
                .HasForeignKey(ut => ut.UserId);
            builder
                .HasOne<WorkPeriod>(ut => ut.WorkPeriod)
                .WithMany()
                .HasForeignKey(ut => ut.WorkPeriodId);
        }
    }
}
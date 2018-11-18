using EffortlessApi.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EffortlessApi.Persistence.EntityConfigurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasOne<User>(a => a.ApprovedBy).WithMany().HasForeignKey(a => a.ApprovedByUserId);
            builder.HasOne<User>(a => a.CreatedBy).WithMany().HasForeignKey(a => a.CreatedByUserId);
        }
    }
}
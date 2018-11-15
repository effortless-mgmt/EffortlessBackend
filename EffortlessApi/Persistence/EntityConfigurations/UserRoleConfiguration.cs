using EffortlessApi.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EffortlessApi.Persistence.EntityConfigurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(ur => new {ur.UserId, ur.RoleId});
            builder
                .HasOne(ur => ur.User)
                .WithMany(user => user.UserRoles)
                .HasForeignKey(ur => ur.UserId);
            builder
                .HasOne(ur => ur.Role)
                .WithMany(role => role.UserRoles)
                .HasForeignKey(ur => ur.RoleId);
        }
    }
}
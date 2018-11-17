using EffortlessApi.Core.Models;
using EffortlessApi.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace EffortlessApi.Persistence 
{
    public class EffortlessContext : DbContext 
    {
        public EffortlessContext(DbContextOptions<EffortlessContext> options) : base(options) { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Privilege> Privileges { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePrivilege> RolePrivileges { get; set; }
        public DbSet<TemporaryWorkPeriod> TemporaryWorkPeriods { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<UserJobActive> UsersJobActive { get; set; }
        public DbSet<UserJobInactive> UsersJobInactive { get; set; }
        public DbSet<UserTemporaryWorkPeriod> UserTemoraryWorkPeriods { get; set; }
        public DbSet<WorkingHours> WorkingHours { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new PrivilegeConfiguration());
            modelBuilder.ApplyConfiguration(new RolePrivilegeConfiguration());
        }
    }
}
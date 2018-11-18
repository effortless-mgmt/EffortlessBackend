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
        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Privilege> Privileges { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePrivilege> RolePrivileges { get; set; }
        public DbSet<TemporaryWorkPeriod> TemporaryWorkPeriods { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<UserTemporaryWorkPeriod> UserTemporaryWorkPeriods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new PrivilegeConfiguration());
            modelBuilder.ApplyConfiguration(new RolePrivilegeConfiguration());
            modelBuilder.ApplyConfiguration(new TemporaryWorkPeriodConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserTemporaryWorkPeriodConfiguration());
        }
    }
}
using Microsoft.EntityFrameworkCore;

namespace EffortlessApi.Models
{
    public class EffortlessContext : DbContext
    {
        public EffortlessContext(DbContextOptions<EffortlessContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Hour> Hours { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}
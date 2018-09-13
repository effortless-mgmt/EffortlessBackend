using Microsoft.EntityFrameworkCore;

namespace EffortlessApi.Models
{
    public class CompanyContext : DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options) { }
        public DbSet<Company> Companies { get; set; }
    }
}
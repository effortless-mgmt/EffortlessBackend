using Microsoft.EntityFrameworkCore;

namespace EffortlessApi.Models
{
    public class HourContext : DbContext
    {
        public HourContext(DbContextOptions<HourContext> options) : base(options) { }
        public DbSet<Hour> Hours { get; set; }
    }
}
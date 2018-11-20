using System;
using System.Linq;
using System.Threading.Tasks;
using EffortlessApi.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace EffortlessApi.Persistence
{
    public partial class EffortlessContext
    {
        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            AddTimestamps();
            return await base.SaveChangesAsync(true, cancellationToken);
        }

        private void AddTimestamps()
        {
            var entries = ChangeTracker.Entries()
                .Where(
                    x => x.Entity is AuditableEntity && 
                    (x.State == EntityState.Added || x.State == EntityState.Modified)
                );
            
            foreach(var entry in entries)
            {
                var auditableEntity = (AuditableEntity) entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    auditableEntity.CreatedDate = DateTime.UtcNow;
                }

                auditableEntity.UpdatedDate = DateTime.UtcNow;
            }
        }
    }
}
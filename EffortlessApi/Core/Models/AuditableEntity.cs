using System;

namespace EffortlessApi.Core.Models
{
    public abstract class AuditableEntity
    {
        public DateTime? CreatedDate { get; set; }
        // public long CreatedById { get; set; }
        // public virtual User CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        // public long UpdatedById { get; set; }
        // public virtual User UpdatedBy { get; set; }
    }
}
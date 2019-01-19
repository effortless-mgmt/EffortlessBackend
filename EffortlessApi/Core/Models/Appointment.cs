using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace EffortlessApi.Core.Models
{
    public class Appointment
    {
        public long Id { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime Stop { get; set; }
        public long Break { get; set; }
        public TimeSpan BreakTimeSpan => new TimeSpan(Break);
        public long? OwnerId { get; set; }
        public virtual User Owner { get; set; }
        [Required]
        public long WorkPeriodId { get; set; }
        public virtual WorkPeriod WorkPeriod { get; set; }
        public bool ApprovedByOwner { get; set; }
        public DateTime ApprovedByOwnerDate { get; set; }
        public long ApprovedByUserId { get; set; }
        public virtual User ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public long CreatedByUserId { get; set; }
        public virtual User CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Earnings { get; set; }
    }
}
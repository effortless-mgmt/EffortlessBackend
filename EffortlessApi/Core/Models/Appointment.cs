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
        [NotMapped]
        public TimeSpan BreakTimeSpan => new TimeSpan(Break);
        [JsonIgnore]
        public long OwnerId { get; set; }
        public virtual User Owner { get; set; }
        [JsonIgnore]
        [Required]
        public long WorkPeriodId { get; set; }
        public virtual WorkPeriod WorkPeriod { get; set; }
        public bool ApprovedByOwner { get; set; }
        public DateTime ApprovedByOwnerDate { get; set; }
        [JsonIgnore]
        public long ApprovedByUserId { get; set; }
        public virtual User ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        [JsonIgnore]
        public long CreatedByUserId { get; set; }
        public virtual User CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Earnings { get; set; }
    }
}
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
        public long UserId { get; set; }
        public virtual User User { get; set; }
        [JsonIgnore]
        [Required]
        public long TemporaryWorkPeriodId { get; set; }
        public virtual TemporaryWorkPeriod TemporaryWorkPeriod { get; set;}
    }
}
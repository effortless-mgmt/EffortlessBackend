using System;

namespace EffortlessApi.Models
{
    public class Appointment
    {
        public long Id { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime Stop { get; set; }
        public int Break { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public long TemporaryWorkPeriodId { get; set; }
    }
}
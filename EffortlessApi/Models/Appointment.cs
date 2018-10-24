using System;

namespace EffortlessApi.Models
{
    public class Appointment
    {
        public long Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public int Break { get; set; }
        public long UserId { get; set; }
        public long TemporaryWorkPeriodId { get; set; }
    }
}
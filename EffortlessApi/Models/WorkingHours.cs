using System;

namespace EffortlessApi.Models
{
    public class WorkingHours
    {
        public long Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public long UserId { get; set; }
        public long AppointmentId { get; set; }
        public int Break { get; set; }
        public bool IsApproved { get; set; } = false;
    }
}
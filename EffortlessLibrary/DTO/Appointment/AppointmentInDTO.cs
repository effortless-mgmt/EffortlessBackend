using System;

namespace EffortlessLibrary.DTO
{
    public class AppointmentInDTO
    {
        public long Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public long Break { get; set; }
        public long? OwnerId { get; set; }
        public long WorkPeriodId { get; set; }
    }
}
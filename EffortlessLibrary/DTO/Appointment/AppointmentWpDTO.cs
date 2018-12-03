using System;

namespace EffortlessLibrary.DTO
{
    public class AppointmentWpDTO
    {
        public long Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public long Break { get; set; }
        public TimeSpan BreakTimeSpan => new TimeSpan(Break);
        public UserDTO Owner { get; set; }
        public long WorkPeriodId { get; set; }
        public UserDTO CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Earnings { get; set; }
    }
}
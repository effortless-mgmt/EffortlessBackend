using System;
using Newtonsoft.Json;

namespace EffortlessLibrary.DTO
{
    public class AppointmentUserDTO
    {
        public long Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public long Break { get; set; }
        public WorkPeriodUserAppointmentDTO WorkPeriod { get; set; }
        public TimeSpan BreakTimeSpan => new TimeSpan(Break);
        public bool ApprovedByOwner { get; set; }
        public DateTime ApprovedByOwnerDate { get; set; }
        public long ApprovedByUserId { get; set; }
        public UserDTO ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public long CreatedByUserId { get; set; }
        public UserDTO CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Earnings { get; set; }
    }
}
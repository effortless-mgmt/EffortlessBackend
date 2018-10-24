using System;

namespace EffortlessApi.Models
{
    public class TemporaryWorkPeriod
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long JobId { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public float UnitPrice { get; set; }
        public float Salary { get; set; }
        public bool BreakIsPaid { get; set; }
    }
}
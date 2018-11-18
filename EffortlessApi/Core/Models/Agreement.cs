using System;

namespace EffortlessApi.Core.Models
{
    public class Agreement
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Salary { get; set; }
        public decimal NightSubsidy { get; set; }
        public decimal WeekendSubsidy { get; set; }
        public decimal HolidaySubsidy { get; set; }
        public bool IsBreakPaid { get; set; }
    }
}
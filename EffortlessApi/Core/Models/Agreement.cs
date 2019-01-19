using System;
using System.ComponentModel.DataAnnotations;

namespace EffortlessApi.Core.Models
{
    public class Agreement
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Version { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public decimal Salary { get; set; }
        // TODO: NightSubsidy starts at 18:00 for now (later a datetime)
        public decimal NightSubsidy { get; set; }
        public decimal WeekendSubsidy { get; set; }
        public decimal HolidaySubsidy { get; set; }
        public bool IsBreakPaid { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace EffortlessApi.Models
{
    public class TemporaryWorkPeriod
    {
        public long Id { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public long JobId { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime Stop { get; set; }
        [Required]
        public float UnitPrice { get; set; }
        [Required]
        public float Salary { get; set; }
        public bool BreakIsPaid { get; set; } = false;
    }
}
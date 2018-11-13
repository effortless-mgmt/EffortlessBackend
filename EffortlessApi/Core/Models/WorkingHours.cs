using System;
using System.ComponentModel.DataAnnotations;

namespace EffortlessApi.Core.Models
{
    public class WorkingHours
    {
        public long Id { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime Stop { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public long AppointmentId { get; set; }
        [Required]
        public int Break { get; set; }
        public bool IsApproved { get; set; } = false;
    }
}
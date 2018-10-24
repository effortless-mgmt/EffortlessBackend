using System;

namespace EffortlessApi.Models
{
    public class Job
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public long BranchId { get; set; }
        [Required]
        public long AdressId { get; set; }
    }
}
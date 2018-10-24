using System;

namespace EffortlessApi.Models
{
    public class UserJobInactive
    {
        public long Id { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public long JobId { get; set; }
    }
}
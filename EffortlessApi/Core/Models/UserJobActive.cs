using System.ComponentModel.DataAnnotations;

namespace EffortlessApi.Core.Models
{
    public class UserJobActive
    {
        public long Id { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public long JobId { get; set; }
    }
}
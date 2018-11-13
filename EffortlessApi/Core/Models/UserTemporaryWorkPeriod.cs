using System.ComponentModel.DataAnnotations;

namespace EffortlessApi.Core.Models
{
    public class UserTemporaryWorkPeriod
    {
        public long Id { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public long TemporaryWorkPeriodId { get; set; }
    }
}
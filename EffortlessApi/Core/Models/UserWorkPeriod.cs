using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace EffortlessApi.Core.Models
{
    public class UserWorkPeriod
    {
        [Required]
        [JsonIgnore]
        public long UserId { get; set; }
        public virtual User User { get; set; }
        [Required]
        [JsonIgnore]
        public long WorkPeriodId { get; set; }
        public virtual WorkPeriod WorkPeriod { get; set; }
    }
}
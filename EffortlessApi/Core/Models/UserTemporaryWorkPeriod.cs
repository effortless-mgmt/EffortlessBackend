using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace EffortlessApi.Core.Models
{
    public class UserTemporaryWorkPeriod
    {
        [Required]
        [JsonIgnore]
        public long UserId { get; set; }
        public virtual User User { get; set; }
        [Required]
        [JsonIgnore]
        public long TemporaryWorkPeriodId { get; set; }
        public virtual TemporaryWorkPeriod TemporaryWorkPeriod { get; set; }
    }
}
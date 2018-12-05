using Newtonsoft.Json;

namespace EffortlessLibrary.DTO
{
    public class UserWorkPeriodDTO
    {
        public UserWorkPeriodDTO() { }
        public UserWorkPeriodDTO(long userId, long wpId)
        {
            UserId = userId;
            WorkPeriodId = wpId;
        }

        public long UserId { get; set; }
        public UserStrippedDTO User { get; set; }
        public long WorkPeriodId { get; set; }
        [JsonIgnore]
        public WorkPeriodSimpleDTO WorkPeriod { get; set; }
    }
}
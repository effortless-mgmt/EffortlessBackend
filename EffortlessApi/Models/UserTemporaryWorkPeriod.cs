using System;

namespace EffortlessApi.Models
{
    public class UserTemporaryWorkPeriod
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long TemporaryWorkPeriodId { get; set; }
    }
}
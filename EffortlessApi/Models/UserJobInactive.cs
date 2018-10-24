using System;

namespace EffortlessApi.Models
{
    public class UserJobInactive
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long JobId { get; set; }
    }
}
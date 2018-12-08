using System;
using Newtonsoft.Json;

namespace EffortlessLibrary.DTO
{
    public class AppointmentStrippedDTO
    {
        public long Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public long Break { get; set; }
        [JsonIgnore]
        public long WorkPeriodId { get; set; }
        public WorkPeriodStrippedDTO workPeriod { get; set; }
        [JsonIgnore]
        public long OwnerId { get; set; }
        public UserStrippedDTO Owner { get; set; }
    }
}
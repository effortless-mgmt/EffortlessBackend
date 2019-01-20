using System;
using Newtonsoft.Json;

namespace EffortlessLibrary.DTO
{
    public class WorkPeriodSimpleDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public long DepartmentId { get; set; }
        public DepartmentDTO Department { get; set; }
        public DateTime Start { get; set; }
        public DateTime LastAppointment { get; set; }
    }
}
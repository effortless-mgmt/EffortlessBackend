using Newtonsoft.Json;

namespace EffortlessLibrary.DTO
{
    public class WorkPeriodStrippedDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public long DepartmentId { get; set; }
        public DepartmentStrippedDTO Department { get; set; }
    }
}
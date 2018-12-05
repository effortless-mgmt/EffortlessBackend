using Newtonsoft.Json;

namespace EffortlessLibrary.DTO
{
    public class RolePrivilegeDTO
    {
        [JsonIgnore]
        public long RoleId { get; set; }
        [JsonIgnore]
        public long PrivilegeId { get; set; }
        public RoleDTO Role { get; set; }
        public PrivilegeDTO Privilege { get; set; }
    }
}
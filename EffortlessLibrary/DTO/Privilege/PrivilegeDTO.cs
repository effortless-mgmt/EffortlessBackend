using System.Collections.Generic;
using Newtonsoft.Json;

namespace EffortlessLibrary.DTO
{
    public class PrivilegeDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }
        // [JsonIgnore]
        // public ICollection<RolePrivilegeDTO> RolePrivileges { get; set; }
    }
}
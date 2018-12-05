using System.Collections.Generic;
using EffortlessLibrary.DTO.PivotTables;
using Newtonsoft.Json;

namespace EffortlessLibrary.DTO.Privilege
{
    public class PrivilegeDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<RolePrivilegeDTO> RolePrivileges { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace EffortlessApi.Core.Models
{
    public class RolePrivilege
    {
        [JsonIgnore]
        public long RoleId { get; set; }
        [JsonIgnore]
        public long PrivilegeId { get; set; }
        public Role Role { get; set; }
        public Privilege Privilege { get; set; }
    }
}
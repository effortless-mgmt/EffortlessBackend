
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace EffortlessApi.Core.Models
{
    public class RolePrivilege
    {
        public long RoleId { get; set; }
        public long PrivilegeId { get; set; }
        public Role Role { get; set; }
        public Privilege Privilege { get; set; }
    }
}
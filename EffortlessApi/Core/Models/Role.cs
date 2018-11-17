
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Linq;

namespace EffortlessApi.Core.Models
{
    public class Role
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<RolePrivilege> RolePrivileges { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserRole> UserRoles { get; set; }
        [JsonIgnore]
        [NotMapped]
        public IList<Privilege> Privileges => RolePrivileges.Select(rp => rp.Privilege).ToList();
        [NotMapped]
        public IList<string> PrivilegeNames => Privileges.Select(p => p.Name).ToList();

    }
}
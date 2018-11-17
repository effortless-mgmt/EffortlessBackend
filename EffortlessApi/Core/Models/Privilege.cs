using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EffortlessApi.Core.Models
{
    public class Privilege
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<RolePrivilege> RolePrivileges { get; set; }
    }

}
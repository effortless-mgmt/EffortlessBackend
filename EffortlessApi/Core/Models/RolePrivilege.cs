
using System.ComponentModel.DataAnnotations;

namespace EffortlessApi.Core.Models
{
    public class RolePrivilege
    {
        public long Id { get; set; }
        [Required]
        public long RoleId { get; set; }
        public Role Role { get; set; }
        [Required]
        public long PrivilegeId { get; set; }
        public Privilege Privilege { get; set; }
    }
}
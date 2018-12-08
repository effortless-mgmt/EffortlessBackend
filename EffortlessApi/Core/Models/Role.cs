
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
        public virtual ICollection<RolePrivilege> RolePrivileges { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        [NotMapped]
        public virtual IList<User> Users
        {
            get
            {
                if (UserRoles == null) return null;
                return UserRoles.Select(ur => ur.User).ToList();
            }
        }
        [NotMapped]
        public IList<string> UserNames
        {
            get
            {
                if (Users == null) return null;
                return Users.Select(u => u.UserName).ToList();
            }
        }

        [NotMapped]
        public virtual IList<Privilege> Privileges
        {
            get
            {
                if (RolePrivileges == null) return null;
                return RolePrivileges.Select(rp => rp.Privilege).ToList();
            }
        }
        [NotMapped]
        public IList<string> PrivilegeNames
        {
            get
            {
                if (Privileges == null) return null;
                return Privileges.Select(p => p.Name).ToList();
            }
        }

    }
}
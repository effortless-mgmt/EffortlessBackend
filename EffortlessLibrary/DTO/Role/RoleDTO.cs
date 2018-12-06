using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace EffortlessLibrary.DTO
{
    public class RoleDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<RolePrivilegeDTO> RolePrivileges { get; set; }
        [JsonIgnore]
        public ICollection<UserRoleDTO> UserRoles { get; set; }
        public IList<UserDTO> Users
        {
            get
            {
                if (UserRoles == null) return null;
                return UserRoles.Select(ur => ur.User).ToList();
            }
        }
        [JsonIgnore]
        public IList<PrivilegeDTO> Privileges
        {
            get
            {
                if (RolePrivileges == null) return null;
                return RolePrivileges.Select(rp => rp.Privilege).ToList();
            }
        }

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
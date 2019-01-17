using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace EffortlessLibrary.DTO
{
    public class UserDTO
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [JsonIgnore]
        public long? AddressId { get; set; }
        public AddressDTO Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public PrimaryRoleTypeDTO PrimaryRole { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserRoleDTO> UserRoles { get; set; }
        [JsonIgnore]
        public IList<RoleDTO> Roles
        {
            get
            {
                if (UserRoles == null) return null;
                return UserRoles.Select(ur => ur.Role).ToList();
            }
        }
        public IList<string> Privileges
        {

            get
            {
                if (Roles == null) return null;

                var pvNames = new List<string>();
                foreach (RoleDTO r in Roles)
                {
                    foreach (string pvName in r.PrivilegeNames)
                    {
                        pvNames.Add(pvName);
                    }
                }
                return pvNames;
            }
        }
    }
}
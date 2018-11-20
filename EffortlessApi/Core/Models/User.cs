using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace EffortlessApi.Core.Models
{
    public class User : AuditableEntity
    {
        public long Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public long AddressId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserRole> UserRoles { get; set; }
        [NotMapped]
        public virtual IList<string> Privileges
        {
            get 
            {
                if (UserRoles == null) return null;
                return UserRoles.Select(ur => ur.Role).SelectMany(r => r.PrivilegeNames).ToList();
            }
        }
    }
}
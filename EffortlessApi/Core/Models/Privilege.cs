using System.ComponentModel.DataAnnotations;

namespace EffortlessApi.Core.Models
{
    public class Privilege
    {
        public long Id { get; set; }
        [Required]
        public long RoleId { get; set; }
        public Role Role { get; set; }
        [Required]
        public string Name { get; set; }
    }

}
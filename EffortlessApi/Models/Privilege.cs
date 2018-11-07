using System.ComponentModel.DataAnnotations;

namespace EffortlessApi.Models
{
    public class Privilege
    {
        public long Id { get; set; }
        [Required]
        public long RoleId { get; set; }
        [Required]
        public string Name { get; set; }
    }

}
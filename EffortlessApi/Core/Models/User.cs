using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EffortlessApi.Core.Models
{
    public class User
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
    }
}
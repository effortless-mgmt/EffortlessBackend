using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EffortlessApi.Models
{
    public class User
    {
        public long Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
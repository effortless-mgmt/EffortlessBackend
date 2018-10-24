using System.Collections.Generic;

namespace EffortlessApi.Models
{
    public class Company
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
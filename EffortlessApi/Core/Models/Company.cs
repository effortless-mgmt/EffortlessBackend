using System.ComponentModel.DataAnnotations;

namespace EffortlessApi.Core.Models
{
    public class Company
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace EffortlessApi.Core.Models
{
    public class Branch
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public long CompanyId { get; set; }
        
    }
}
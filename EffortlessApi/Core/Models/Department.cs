using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace EffortlessApi.Core.Models
{
    public class Department
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [JsonIgnore]
        public long CompanyId { get; set; }
        public virtual Company Company { get; set; }
        [Required]
        [JsonIgnore]
        public long AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}
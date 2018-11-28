using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace EffortlessApi.Core.Models
{
    public class Company
    {
        public long Id { get; set; }
        [Required]
        public int Vat { get; set; }
        public int Pno { get; set; }
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        public long? ParentCompanyId { get; set; }
        [JsonIgnore]
        public long? AddressId { get; set; }
    }
}
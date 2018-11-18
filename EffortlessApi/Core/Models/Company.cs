using System.ComponentModel.DataAnnotations;

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
        public int ParentCompanyId { get; set; }
        public virtual Company Parent { get; set; }
        public long AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}
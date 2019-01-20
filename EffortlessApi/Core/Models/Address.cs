using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EffortlessApi.Core.Models
{
    public class Address
    {
        public long Id { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public int No { get; set; }
        public string Floor { get; set; }
        public string Side { get; set; }
        [Required]
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        [Required]
        public string Country { get; set; }
        [NotMapped]        
        public string ReadableAddress
        {
            get
            {
                if (this.Side == null && this.Floor == null)
                {
                    return $"{this.Street} {this.No}\n{this.ZipCode} {this.City}\n{this.Country}";
                }
                else if (this.Side == null)
                {
                    return $"{this.Street} {this.No}, {this.Floor}\n{this.ZipCode} {this.City}\n{this.Country}";
                }
                else if (this.Floor == null)
                {
                    return $"{this.Street} {this.No}, {this.Side}\n{this.ZipCode} {this.City}\n{this.Country}";
                }

                return $"{this.Street} {this.No}, {this.Floor}{this.Side}\n{this.ZipCode} {this.City}\n{this.Country}";
            }
        }
    }
}
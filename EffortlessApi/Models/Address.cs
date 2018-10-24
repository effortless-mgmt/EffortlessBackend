using System;

namespace EffortlessApi.Models
{
    public class Address
    {
        public long Id { get; set; }
        [Required]
        public string Road { get; set; }
        [Required]
        public int No { get; set; }
        public int Floor { get; set; }
        public string Side { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public int ZipCode { get; set; }
        [Required]
        public string Country { get; set; }

        public override string ToString()
        {
            if (this.getSide() == null && this.getFloor() == null)
            {
                return this.getRoad() + " " + this.getNo() + "\n"
                    + this.getPcode() + " " + this.getCity() + "\n"
                    + this.getCountry();
            }
            else if (this.getSide() == null)
            {
                return this.getRoad() + " " + this.getNo() + " " + this.getFloor() + "\n"
                + this.getPcode() + " " + this.getCity() + "\n"
                + this.getCountry();
            }
            else if (this.getFloor() == null)
            {
                return this.getRoad() + " " + this.getNo() + " " + this.getSide() + "\n"
                + this.getPcode() + " " + this.getCity() + "\n"
                + this.getCountry();
            }

            return this.getRoad() + " " + this.getNo() + " " + this.getFloor() + this.getSide() + "\n"
                + this.getPcode() + " " + this.getCity() + "\n"
                + this.getCountry();
        }
    }
}
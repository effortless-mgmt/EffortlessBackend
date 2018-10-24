using System;

namespace EffortlessApi.Models
{
    public class Address
    {

        long id { get; set; }
        [Required]
        string road { get; set; }
        [Required]
        int no { get; set; }
        int floor { get; set; }
        string side { get; set; }
        [Required]
        string city { get; set; }
        [Required]
        int pcode { get; set; }
        [Required]
        string country { get; set; }

        public string toString()
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
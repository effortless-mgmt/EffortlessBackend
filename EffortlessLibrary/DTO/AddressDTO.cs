namespace EffortlessLibrary.DTO
{
    public class AddressDTO
    {
        public long Id { get; set; }
        public string Street { get; set; }
        public int No { get; set; }
        public string Floor { get; set; }
        public string Side { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public string Country { get; set; }
    }
}
namespace EffortlessLibrary.DTO
{
    public class AddressSimpleDTO
    {
        public long Id { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public string Country { get; set; }
        public string ReadableAddress { get; set; }
    }
}
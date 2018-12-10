namespace EffortlessLibrary.DTO
{
    public class CompanyDetailsDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Vat { get; set; }
        // public int Pno { get; set; }
        public AddressDTO Address { get; set; }
    }
}
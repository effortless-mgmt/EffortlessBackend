using EffortlessLibrary.DTO.Address;

namespace EffortlessLibrary.DTO
{
    public class CompanyDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Vat { get; set; }
        public int Pno { get; set; }
        public AddressDTO Address { get; set; }
    }
}
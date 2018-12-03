using Newtonsoft.Json;

namespace EffortlessLibrary.DTO.Department
{
    public class DepartmentDetailsDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Pno { get; set; }
        [JsonIgnore]
        public long CompanyId { get; set; }
        public CompanySimpleDTO Company { get; set; }
        [JsonIgnore]
        public long AddressId { get; set; }
        public AddressDTO Address { get; set; }
    }
}
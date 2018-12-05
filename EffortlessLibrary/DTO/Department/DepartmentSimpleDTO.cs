using EffortlessLibrary.DTO.Address;
using Newtonsoft.Json;

namespace EffortlessLibrary.DTO
{
    public class DepartmentSimpleDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public long AddressId { get; set; }
        public AddressDTO Address { get; set; }
    }
}
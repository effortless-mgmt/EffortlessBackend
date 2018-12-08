using Newtonsoft.Json;

namespace EffortlessLibrary.DTO
{
    public class UserSimpleDTO
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        [JsonIgnore]
        public string FirstName { get; set; }
        [JsonIgnore]
        public string LastName { get; set; }
        public string Name
        {
            get { return $"{FirstName} {LastName}"; }
        }
        [JsonIgnore]
        public long AddressId { get; set; }
        public AddressSimpleDTO Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
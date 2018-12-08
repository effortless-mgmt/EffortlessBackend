using Newtonsoft.Json;

namespace EffortlessLibrary.DTO
{
    public class UserStrippedDTO
    {
        public long Id { get; set; }
        [JsonIgnore]
        public string FirstName { get; set; }
        [JsonIgnore]
        public string LastName { get; set; }
        public string Name
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
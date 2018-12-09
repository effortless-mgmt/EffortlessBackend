using Newtonsoft.Json;

namespace EffortlessLibrary.DTO
{
    public class UserStrippedDTO
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [JsonIgnore]
        public string Name
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
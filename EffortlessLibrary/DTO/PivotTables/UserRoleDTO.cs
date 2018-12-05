using EffortlessLibrary.DTO.Role;
using Newtonsoft.Json;

namespace EffortlessLibrary.DTO.PivotTables
{
    public class UserRoleDTO
    {
        [JsonIgnore]
        public long UserId { get; set; }
        [JsonIgnore]
        public long RoleId { get; set; }
        public UserDTO User { get; set; }
        public RoleDTO Role { get; set; }
    }
}
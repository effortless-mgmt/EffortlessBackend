using Newtonsoft.Json;

namespace EffortlessLibrary.DTO
{
    public class UserRoleDTO
    {
        public UserRoleDTO(long userId, long roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }

        [JsonIgnore]
        public long UserId { get; set; }
        [JsonIgnore]
        public long RoleId { get; set; }
        public UserDTO User { get; set; }
        public RoleDTO Role { get; set; }
    }
}
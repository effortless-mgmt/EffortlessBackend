using System;

namespace EffortlessApi.Models
{
    public class RolePrivilege
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public long PrivilegeId { get; set; }
    }
}
using System.Collections.Generic;

namespace EffortlessApi.Core.Models
{
    public class CompanyToUser
    {
        public ICollection<Company> Companies { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
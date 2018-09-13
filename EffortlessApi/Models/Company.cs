using System.Collections.Generic;

namespace EffortlessApi.Models
{
    public class Company
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IList<User> Employees { get; set; }
    }
}
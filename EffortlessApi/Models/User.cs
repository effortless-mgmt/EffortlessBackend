using System.Collections.Generic;

namespace EffortlessApi.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public IList<Hour> Hours { get; set; }
        public IList<Company> Companies { get; set; }
    }
}
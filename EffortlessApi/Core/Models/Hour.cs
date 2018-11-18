using System;

namespace EffortlessApi.Core.Models
{
    public class Hour
    {
        public long Id { get; set; }
        public User Owner { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public Company Company { get; set; }
    }
}
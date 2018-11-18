using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;

namespace EffortlessApi.Core.Models
{
    public class TemporaryWorkPeriod
    {
        public long Id { get; set; }
        [JsonIgnore]
        public ICollection<UserTemporaryWorkPeriod> UserTemporaryWorkPeriods { get; set; }
        public virtual IList<User> AssignedUsers 
        {
            get
            {
                if (UserTemporaryWorkPeriods == null) return null;
                return UserTemporaryWorkPeriods.Select(ut => ut.User).ToList();
            }    
        }

        [Required]
        public long JobId { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime Stop { get; set; }
        [Required]
        public float UnitPrice { get; set; }
        [Required]
        public float Salary { get; set; }
        public bool BreakIsPaid { get; set; } = false;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace EffortlessApi.Core.Models
{
    public class WorkPeriod
    {
        public long Id { get; set; }
        [JsonIgnore]
        public ICollection<UserWorkPeriod> UserWorkPeriods { get; set; }
        [NotMapped]
        public virtual IList<User> AssignedUsers
        {
            get
            {
                if (UserWorkPeriods == null) return null;
                return UserWorkPeriods.Select(ut => ut.User).ToList();
            }
        }

        [Required]
        public long DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime Stop { get; set; }
    }
}
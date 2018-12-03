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
        public string Name { get; set; }
        public ICollection<UserWorkPeriod> UserWorkPeriods { get; set; }
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
        public virtual Agreement Agreement { get; set; }
        public long AgreementId { get; set; }
        [Required]
        public DateTime Start { get; set; }
        public DateTime LastAppointment { get; set; }
        public bool Active { get; set; }
    }
}
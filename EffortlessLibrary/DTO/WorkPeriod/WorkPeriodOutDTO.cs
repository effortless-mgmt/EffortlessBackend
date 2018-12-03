using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace EffortlessLibrary.DTO
{
    public class WorkPeriodOutDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public AgreementDTO Agreement { get; set; }
        [JsonIgnore]
        public ICollection<UserWorkPeriodDTO> UserWorkPeriods { get; set; }
        public IList<UserDTO> AssignedUsers
        {
            get
            {
                if (UserWorkPeriods == null) return null;
                return UserWorkPeriods.Select(ut => ut.User).ToList();
            }
        }

        public DepartmentDTO Department { get; set; }
        public ICollection<AppointmentSimpleDTO> Appointments { get; set; }
        public DateTime Start { get; set; }
        public DateTime LastAppointmentDate
        {
            get
            {
                if (Appointments == null) return Start;
                var lastAppointment = Appointments.OrderBy(a => a.Stop).ToList()[Appointments.Count - 1];
                return lastAppointment.Stop;
            }
        }
        public bool Active { get; set; }
    }
}
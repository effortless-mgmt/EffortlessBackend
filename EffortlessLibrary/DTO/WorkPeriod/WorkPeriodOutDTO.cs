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
        [JsonIgnore]
        public long AgreementId { get; set; }
        public AgreementDTO Agreement { get; set; }
        [JsonIgnore]
        public ICollection<UserWorkPeriodDTO> UserWorkPeriods { get; set; }
        public IList<UserSimpleDTO> AssignedUsers
        {
            get
            {
                if (UserWorkPeriods == null) return null;
                return UserWorkPeriods.Select(ut => ut.User).ToList();
            }
        }
        [JsonIgnore]
        public long DepartmentId { get; set; }
        public DepartmentDTO Department { get; set; }
        public IList<AppointmentWpDTO> Appointments { get; set; }
        public DateTime Start { get; set; }
        public DateTime LastAppointmentDate
        {
            get
            {
                if (Appointments.Count == 0) return Start;
                var lastAppointment = Appointments.OrderBy(a => a.Stop).ToList()[Appointments.Count - 1];
                return lastAppointment.Stop;
            }
        }
        public bool Active { get; set; }
    }
}
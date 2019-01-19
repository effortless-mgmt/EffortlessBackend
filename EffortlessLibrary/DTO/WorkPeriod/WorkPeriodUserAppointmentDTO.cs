using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace EffortlessLibrary.DTO
{
    public class WorkPeriodUserAppointmentDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public long DepartmentId { get; set; }
        public DepartmentSimpleDTO Department { get; set; }
        public IList<AppointmentUserDTO> Appointments { get; set; }
        public AgreementDTO Agreement { get; set; }
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
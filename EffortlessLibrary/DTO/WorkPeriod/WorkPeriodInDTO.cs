using System;
using System.Collections.Generic;
using System.Linq;

namespace EffortlessLibrary.DTO
{
    public class WorkPeriodInDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long AgreementId { get; set; }

        public long DepartmentId { get; set; }
        public DateTime Start { get; set; }
        public bool Active { get; set; }
    }
}
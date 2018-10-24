using System;

namespace EffortlessApi.Models
{

    public class Job
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long BranchId { get; set; }
        public long AdressId { get; set; }
    }
}
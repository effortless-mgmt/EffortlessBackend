using System;

namespace EffortlessApi.Models
{
    public class Branch
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CompanyId { get; set; }
    }
}
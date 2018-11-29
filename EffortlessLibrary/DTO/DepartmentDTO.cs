namespace EffortlessLibrary.DTO
{
    public class DepartmentDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DepartmentCompanyDTO Company { get; set; }
        public AddressDTO Address { get; set; }
    }
}
namespace EffortlessLibrary.DTO.PivotTables
{
    public class UserWorkPeriodDTO
    {
        public long UserId { get; set; }
        public UserDTO User { get; set; }
        public WorkPeriodSimpleDTO WorkPeriod { get; set; }
    }
}
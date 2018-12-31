namespace WebAPI.Model.TimeKeeping
{
    public class TimeKeepingMonthModelReq
    {
        public string EmployeeId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public double? NoWorkStandard { get; set; }
    }
}
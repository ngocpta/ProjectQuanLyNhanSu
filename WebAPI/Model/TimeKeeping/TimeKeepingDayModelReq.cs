namespace WebAPI.Model.TimeKeeping
{
    public class TimeKeepingDayModelReq
    {
        public string EmployeeId { get; set; }
        public string Date { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
    }
}
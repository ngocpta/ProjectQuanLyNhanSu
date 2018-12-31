namespace WebAPI.Model.TimeKeeping
{
    public class TimeKeepingDayModelReq
    {
        public string EmployeeId { get; set; }
        public string Date { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
        public bool? IsLeaveWithPermission { get; set; } //nghỉ có phép 
        public bool? IsLeaveWithoutPermission { get; set; }//nghỉ không phép
        public bool? IsGetVacation { get; set; } //được nghỉ phép
        public string UpDatedBy { get; set; }
    }
}
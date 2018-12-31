using System;

namespace WebAPI.Model.TimeKeeping
{
    public class TimeKeepingDayRes
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
        public string Date { get; set; }
        public bool? IsLate { get; set; }//đi muộn
        public bool? IsLeaveWithPermission { get; set; } //nghỉ có phép 
        public bool? IsLeaveWithoutPermission { get; set; } //nghỉ không phép
        public bool? IsGetVacation { get; set; } //được nghỉ phép 
        public double? NoWork { get; set; }
        public double? NoTimeWork { get; set; }
        public double? ExtractTimeWork { get; set; }
        public string Note { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
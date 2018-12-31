using System.Collections.Generic;

namespace WebAPI.Model.TimeKeeping
{
    public class TimeKeepingMonthRes
    {
        public string EmployeeId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public List<TimeKeepingDayRes> TimeKeepingDay { get; set; }
        public double? NoLeaveWithPermission { get; set; } //số ngày nghỉ có phép 
        public double? NoLeaveWithoutPermission { get; set; }//số ngày nghỉ không phép
        public double? NoGetVacation { get; set; } //số ngày được nghỉ phép 
        public double? NoLate { get; set; }//số lần đi muộn
        public double NoWorkStandard { get; set; }//số công chuẩn
        public double NoWork { get; set; }//số công
    }
}
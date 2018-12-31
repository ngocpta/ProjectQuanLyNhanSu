using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class TimeKeeping
    {
        public string EmployeeId { get; set; }
        public TimeSpan? TimeIn { get; set; }
        public TimeSpan? TimeOut { get; set; }
        public DateTime Date { get; set; }
        public double? NoWork { get; set; }
        public double? NoTimeWork { get; set; }
        public double? ExtractTimeWork { get; set; }
        public string Note { get; set; }
        public bool? IsLate { get; set; }
        public bool? IsLeaveWithoutPermission { get; set; }
        public bool? IsLeaveWithPermission { get; set; }
        public bool? IsGetVacation { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}

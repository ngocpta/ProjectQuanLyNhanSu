using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class TimeWork
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public DateTime? Month { get; set; }
        public decimal? TotalWorkday { get; set; }
        public bool? UnpaidLeave { get; set; }
        public bool? VacationLeave { get; set; }
        public bool? SickLeave { get; set; }
        public bool? HolidayLeave { get; set; }
        public decimal? TotalExtraTime { get; set; }
        public decimal? TotalExtraTimeWork { get; set; }
        public decimal? TotalTimeWork { get; set; }
    }
}

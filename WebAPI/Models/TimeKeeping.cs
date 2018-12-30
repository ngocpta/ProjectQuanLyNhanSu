using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class TimeKeeping
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public TimeSpan? TimeIn { get; set; }
        public TimeSpan? TimeOut { get; set; }
        public DateTime? Date { get; set; }
        public decimal? NoWork { get; set; }
        public decimal? NoTimeWork { get; set; }
        public decimal? ExtractTimeWork { get; set; }
        public string Note { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public Employee Employee { get; set; }
    }
}

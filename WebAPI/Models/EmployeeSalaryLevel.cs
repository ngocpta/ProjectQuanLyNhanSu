using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class EmployeeSalaryLevel
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public string SalaryLevelId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public Employee Employee { get; set; }
        public SalaryLevel SalaryLevel { get; set; }
    }
}

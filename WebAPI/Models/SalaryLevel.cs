using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class SalaryLevel
    {
        public SalaryLevel()
        {
            EmployeeSalaryLevel = new HashSet<EmployeeSalaryLevel>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public double? BasicFactor { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public string Note { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsEdit { get; set; }
        public bool? IsDelete { get; set; }

        public ICollection<EmployeeSalaryLevel> EmployeeSalaryLevel { get; set; }
    }
}

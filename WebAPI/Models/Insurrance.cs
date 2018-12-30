using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Insurrance
    {
        public Insurrance()
        {
            EmployeeInsurrance = new HashSet<EmployeeInsurrance>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsDelete { get; set; }
        public decimal? Amount { get; set; }
        public decimal? PercentTax { get; set; }

        public ICollection<EmployeeInsurrance> EmployeeInsurrance { get; set; }
    }
}

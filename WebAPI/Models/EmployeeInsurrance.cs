using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class EmployeeInsurrance
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public string InsurranceId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public Employee Employee { get; set; }
        public Insurrance Insurrance { get; set; }
    }
}

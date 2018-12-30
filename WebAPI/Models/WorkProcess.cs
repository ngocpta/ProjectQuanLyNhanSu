using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class WorkProcess
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public string CompanyWorkedName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime? StartWork { get; set; }
        public DateTime? EndWork { get; set; }

        public Employee Employee { get; set; }
    }
}

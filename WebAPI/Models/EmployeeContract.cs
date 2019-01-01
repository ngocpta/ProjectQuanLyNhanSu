using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class EmployeeContract
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public string ContractId { get; set; }
        public DateTime SigningDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Note { get; set; }

        public Contracts Contract { get; set; }
        public Employee Employee { get; set; }
    }
}

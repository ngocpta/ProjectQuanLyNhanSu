using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Contracts
    {
        public Contracts()
        {
            EmployeeContract = new HashSet<EmployeeContract>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public string ContractTypeId { get; set; }
        public int? Time { get; set; }
        public bool? Active { get; set; }
        public decimal? SalaryFactor { get; set; }

        public ContractType ContractType { get; set; }
        public ICollection<EmployeeContract> EmployeeContract { get; set; }
    }
}

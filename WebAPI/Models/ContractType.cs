using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class ContractType
    {
        public ContractType()
        {
            Contract = new HashSet<Contracts>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int? Time { get; set; }
        public int? Type { get; set; }
        public string Note { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public ICollection<Contracts> Contract { get; set; }
    }
}

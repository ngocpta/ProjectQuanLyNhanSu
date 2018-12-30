using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class PersonalTaxRate
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double? Scale { get; set; }
        public double? MaxLevel { get; set; }
        public double? MinLevel { get; set; }
        public string Note { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}

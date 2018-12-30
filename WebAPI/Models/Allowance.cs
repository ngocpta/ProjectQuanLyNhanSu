using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Allowance
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? Type { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public double? Factor { get; set; }
        public string Note { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsDelete { get; set; }
    }
}

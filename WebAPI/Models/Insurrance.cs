using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public partial class Insurrance
    {
        public Insurrance()
        {
            EmployeeInsurrance = new HashSet<EmployeeInsurrance>();
        }

        [Column("ID")]
        [StringLength(50)]
        public string Id { get; set; }
        [StringLength(250)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        public bool? IsDelete { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? PercentTax { get; set; }

        [InverseProperty("Insurrance")]
        public virtual ICollection<EmployeeInsurrance> EmployeeInsurrance { get; set; }
    }
}

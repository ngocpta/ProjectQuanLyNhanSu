using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public partial class Allowance
    {
        public Allowance()
        {
            EmployeeAllowance = new HashSet<EmployeeAllowance>();
        }

        [Column("ID")]
        [StringLength(50)]
        public string Id { get; set; }
        [StringLength(250)]
        public string Name { get; set; }
        public int? Type { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EffectiveDate { get; set; }
        public double? Factor { get; set; }
        [StringLength(255)]
        public string Note { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        public bool? IsDelete { get; set; }

        [InverseProperty("Allowance")]
        public virtual ICollection<EmployeeAllowance> EmployeeAllowance { get; set; }
    }
}

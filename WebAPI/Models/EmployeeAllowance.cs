using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public partial class EmployeeAllowance
    {
        [Column("ID")]
        [StringLength(50)]
        public string Id { get; set; }
        [Required]
        [Column("EmployeeID")]
        [StringLength(50)]
        public string EmployeeId { get; set; }
        [Required]
        [Column("AllowanceID")]
        [StringLength(50)]
        public string AllowanceId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SigningDate { get; set; }
        [StringLength(255)]
        public string Note { get; set; }

        [ForeignKey("AllowanceId")]
        [InverseProperty("EmployeeAllowance")]
        public virtual Allowance Allowance { get; set; }
        [ForeignKey("EmployeeId")]
        [InverseProperty("EmployeeAllowance")]
        public virtual Employee Employee { get; set; }
    }
}

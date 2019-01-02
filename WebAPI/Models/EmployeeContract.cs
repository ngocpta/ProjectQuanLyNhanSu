using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public partial class EmployeeContract
    {
        [Column("ID")]
        [StringLength(50)]
        public string Id { get; set; }
        [Required]
        [Column("EmployeeID")]
        [StringLength(50)]
        public string EmployeeId { get; set; }
        [Required]
        [Column("ContractID")]
        [StringLength(50)]
        public string ContractId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SigningDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EndDate { get; set; }
        [StringLength(255)]
        public string Note { get; set; }

        [ForeignKey("EmployeeId")]
        [InverseProperty("EmployeeContract")]
        public virtual Employee Employee { get; set; }
    }
}

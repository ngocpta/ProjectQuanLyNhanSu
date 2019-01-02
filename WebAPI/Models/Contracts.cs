using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public partial class Contracts
    {
        [Column("ID")]
        [StringLength(50)]
        public string Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Note { get; set; }
        [Column("ContractTypeID")]
        [StringLength(50)]
        public string ContractTypeId { get; set; }
        public int? Time { get; set; }
        public bool? Active { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal SalaryFactor { get; set; }

        [ForeignKey("ContractTypeId")]
        [InverseProperty("Contracts")]
        public virtual ContractType ContractType { get; set; }
    }
}

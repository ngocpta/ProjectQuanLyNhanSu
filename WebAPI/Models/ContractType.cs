using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public partial class ContractType
    {
        public ContractType()
        {
            Contracts = new HashSet<Contracts>();
        }

        [Column("ID")]
        [StringLength(50)]
        public string Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        public int? Time { get; set; }
        public int? Type { get; set; }
        [StringLength(255)]
        public string Note { get; set; }
        public bool? Active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        [InverseProperty("ContractType")]
        public virtual ICollection<Contracts> Contracts { get; set; }
    }
}

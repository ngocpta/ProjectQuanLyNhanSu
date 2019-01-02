using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public partial class EmployeeInsurrance
    {
        [Column("ID")]
        [StringLength(50)]
        public string Id { get; set; }
        [Column("EmployeeID")]
        [StringLength(50)]
        public string EmployeeId { get; set; }
        [Column("InsurranceID")]
        [StringLength(50)]
        public string InsurranceId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        [ForeignKey("EmployeeId")]
        [InverseProperty("EmployeeInsurrance")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("InsurranceId")]
        [InverseProperty("EmployeeInsurrance")]
        public virtual Insurrance Insurrance { get; set; }
    }
}

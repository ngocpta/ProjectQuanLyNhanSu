using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public partial class EmployeeDiscipline
    {
        [Column("ID")]
        [StringLength(50)]
        public string Id { get; set; }
        [Required]
        [Column("EmployeeID")]
        [StringLength(50)]
        public string EmployeeId { get; set; }
        [Required]
        [Column("DisciplineID")]
        [StringLength(50)]
        public string DisciplineId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }
        [StringLength(255)]
        public string Note { get; set; }

        [ForeignKey("DisciplineId")]
        [InverseProperty("EmployeeDiscipline")]
        public virtual Discipline Discipline { get; set; }
        [ForeignKey("EmployeeId")]
        [InverseProperty("EmployeeDiscipline")]
        public virtual Employee Employee { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public partial class Discipline
    {
        public Discipline()
        {
            EmployeeDiscipline = new HashSet<EmployeeDiscipline>();
        }

        [Column("ID")]
        [StringLength(50)]
        public string Id { get; set; }
        [StringLength(50)]
        public string DicisionNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EffectiveDate { get; set; }
        [StringLength(255)]
        public string Title { get; set; }
        [Column("RewardAndDisciplineMethodID")]
        [StringLength(50)]
        public string RewardAndDisciplineMethodId { get; set; }
        public int? Type { get; set; }
        public double? Amount { get; set; }
        public double? PercentDiscipline { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SignDate { get; set; }
        [StringLength(250)]
        public string SignBy { get; set; }
        [StringLength(255)]
        public string Desciption { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("RewardAndDisciplineMethodId")]
        [InverseProperty("Discipline")]
        public virtual RewardAndDisciplineMethod RewardAndDisciplineMethod { get; set; }
        [InverseProperty("Discipline")]
        public virtual ICollection<EmployeeDiscipline> EmployeeDiscipline { get; set; }
    }
}

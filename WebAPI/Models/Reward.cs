using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public partial class Reward
    {
        public Reward()
        {
            EmployeeReward = new HashSet<EmployeeReward>();
        }

        [Column("ID")]
        [StringLength(50)]
        public string Id { get; set; }
        [StringLength(50)]
        public string DicisionNo { get; set; }
        [Column(TypeName = "date")]
        public DateTime? EffectiveDate { get; set; }
        [StringLength(255)]
        public string Title { get; set; }
        [Column("RewardAndDisciplineMethodID")]
        [StringLength(50)]
        public string RewardAndDisciplineMethodId { get; set; }
        public int? Type { get; set; }
        public double? Amount { get; set; }
        public double? PercentReward { get; set; }
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
        [InverseProperty("Reward")]
        public virtual RewardAndDisciplineMethod RewardAndDisciplineMethod { get; set; }
        [InverseProperty("Reward")]
        public virtual ICollection<EmployeeReward> EmployeeReward { get; set; }
    }
}

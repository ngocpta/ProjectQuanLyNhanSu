using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public partial class EmployeeReward
    {
        [Column("ID")]
        [StringLength(50)]
        public string Id { get; set; }
        [Required]
        [Column("EmployeeID")]
        [StringLength(50)]
        public string EmployeeId { get; set; }
        [Required]
        [Column("RewardID")]
        [StringLength(50)]
        public string RewardId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }
        [StringLength(255)]
        public string Note { get; set; }

        [ForeignKey("EmployeeId")]
        [InverseProperty("EmployeeReward")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("RewardId")]
        [InverseProperty("EmployeeReward")]
        public virtual Reward Reward { get; set; }
    }
}

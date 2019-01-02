using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public partial class RewardAndDisciplineMethod
    {
        public RewardAndDisciplineMethod()
        {
            Discipline = new HashSet<Discipline>();
            Reward = new HashSet<Reward>();
        }

        [Column("ID")]
        [StringLength(50)]
        public string Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        public int? Type { get; set; }
        [StringLength(255)]
        public string Reason { get; set; }
        public bool? IsEdit { get; set; }
        public bool? IsDelete { get; set; }

        [InverseProperty("RewardAndDisciplineMethod")]
        public virtual ICollection<Discipline> Discipline { get; set; }
        [InverseProperty("RewardAndDisciplineMethod")]
        public virtual ICollection<Reward> Reward { get; set; }
    }
}

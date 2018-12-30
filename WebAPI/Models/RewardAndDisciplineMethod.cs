using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class RewardAndDisciplineMethod
    {
        public RewardAndDisciplineMethod()
        {
            Discipline = new HashSet<Discipline>();
            Reward = new HashSet<Reward>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int? Type { get; set; }
        public bool? IsEdit { get; set; }
        public bool? IsDelete { get; set; }
        public string Reason { get; set; }

        public ICollection<Discipline> Discipline { get; set; }
        public ICollection<Reward> Reward { get; set; }
    }
}

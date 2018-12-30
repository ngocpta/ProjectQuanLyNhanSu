using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class EmployeeReward
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public string RewardId { get; set; }
        public DateTime? Date { get; set; }
        public string Note { get; set; }

        public Employee Employee { get; set; }
        public Reward Reward { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Discipline
    {
        public Discipline()
        {
            EmployeeDiscipline = new HashSet<EmployeeDiscipline>();
        }

        public string Id { get; set; }
        public string DicisionNo { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public string Title { get; set; }
        public string RewardAndDisciplineMethodId { get; set; }
        public double? Amount { get; set; }
        public double? PercentDiscipline { get; set; }
        public DateTime? SignDate { get; set; }
        public string SignBy { get; set; }
        public string Desciption { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public RewardAndDisciplineMethod RewardAndDisciplineMethod { get; set; }
        public ICollection<EmployeeDiscipline> EmployeeDiscipline { get; set; }
    }
}

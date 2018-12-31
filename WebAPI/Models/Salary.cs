using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("Salary")]
    public partial class Salary
    {
        [Key]
        public string EmployeeId { get; set; }
        [Key]
        public int Month { get; set; }
        [Key]
        public int Year { get; set; }
        public decimal? SalaryBasic { get; set; }
        public double? TotalTimeWork { get; set; }
        public decimal? AllowanceHaveLunch { get; set; }
        public decimal? AllowanceParking { get; set; }
        public decimal? AllowanceCall { get; set; }
        public decimal? AllowanceOther { get; set; }
        public double? NoLeaveWithPermission { get; set; }
        public double? NoLeaveWithoutPermission { get; set; }
        public double? NoLate { get; set; }
        public double? NoGetVacation { get; set; }
        public double? NoWorkStandard { get; set; }
        public double? NoWork { get; set; }
        public decimal? SalaryPerDay { get; set; }
        public decimal? SalaryReal { get; set; }
        public decimal? PercentInsurance { get; set; }
        public decimal? PercentPersonalTaxRate { get; set; }
        public decimal? DisciplineLateMoney { get; set; }
        public decimal? DisciplineKPI { get; set; }
        public decimal? DisciplineOther { get; set; }
        public decimal? RewardKPI { get; set; }
        public decimal? RewardOther { get; set; }

    }
}
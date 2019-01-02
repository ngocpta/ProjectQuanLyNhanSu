using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public partial class Salary
    {
        [Column("EmployeeID")]
        [StringLength(50)]
        public string EmployeeId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SalaryBasic { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AllowanceHaveLunch { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AllowanceParking { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AllowanceCall { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AllowanceOther { get; set; }
        public double? NoLate { get; set; }
        public double? NoWorkStandard { get; set; }
        public double? NoWork { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SalaryPerDay { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SalaryReal { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? PercentInsurance { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? PercentPersonalTaxRate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? DisciplineLateMoney { get; set; }
        [Column("DisciplineKPI", TypeName = "decimal(18, 2)")]
        public decimal? DisciplineKpi { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? DisciplineOther { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? RewardKpi { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? RewardOther { get; set; }
        [StringLength(250)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
    }
}

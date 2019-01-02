namespace WebAPI.Model.Salary
{
    public class SalaryRes
    {
        public string EmployeeId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal? SalaryBasic { get; set; }
        public decimal? AllowanceHaveLunch { get; set; }
        public decimal? AllowanceParking { get; set; }
        public decimal? AllowanceCall { get; set; }
        public decimal? AllowanceOther { get; set; }
        public double? NoLate { get; set; }
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
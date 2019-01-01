namespace WebAPI.Model.Salary
{
    public class SalaryModelReq
    {
        public string EmployeeId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal? AllowanceHaveLunchPerDay { get; set; }
        public decimal? AllowanceParking { get; set; }
        public decimal? AllowanceCall { get; set; }
        public decimal? AllowanceOther { get; set; }
        public decimal? PercentInsurance { get; set; }
        public decimal? PercentPersonalTaxRate { get; set; }
        public decimal? DisciplineLateMoneyNumberOfTime { get; set; }
        public decimal? DisciplineLeaveWithoutPermission { get; set; }
        public decimal? DisciplineKPI { get; set; }
        public decimal? DisciplineOther { get; set; }
        public decimal? RewardKPI { get; set; }
        public decimal? RewardOther { get; set; }
        public decimal? SalaryBasic { get; set; }
        public string UpdatedBy { get; set; }
    }
}
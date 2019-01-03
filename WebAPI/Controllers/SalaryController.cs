using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ExceptionModel.ContractException;
using WebAPI.ExceptionModel.SalaryException;
using WebAPI.Model;
using WebAPI.Model.Constant;
using WebAPI.Model.Salary;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/salary")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly QuanLyNhanSuContext _context;

        public SalaryController(QuanLyNhanSuContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("get-all-salary")]
        public IActionResult GetAllSalary([FromBody] Req<SalaryMonthModelReq> req)
        {
            Res res = new Res();

            try
            {
                var salarys = _context.Salary.Where(x => x.Month == req.value.Month && x.Year == req.value.Year);
                if (salarys == null)
                    throw new SalaryNotFoundException();
                var list = new List<SalaryRes>();
                foreach (var s in salarys)
                {
                    var salary = new SalaryRes();
                    try
                    {
                        salary.DisciplineLateMoney = s.DisciplineLateMoney;
                        salary.DisciplineKPI = s.DisciplineKpi;
                        salary.DisciplineOther = s.DisciplineOther;
                        salary.PercentInsurance = s.PercentInsurance;
                        salary.RewardKPI = s.RewardKpi;
                        salary.RewardOther = s.RewardOther;
                        salary.SalaryBasic = s.SalaryBasic;
                        salary.SalaryReal = s.SalaryReal;
                        salary.AllowanceCall = s.AllowanceCall;
                        salary.AllowanceHaveLunch = s.AllowanceHaveLunch;
                        salary.AllowanceOther = s.AllowanceOther;
                        salary.AllowanceParking = s.AllowanceParking;
                        salary.Month = s.Month;
                        salary.Year = s.Year;
                        salary.NoWork = s.NoWork;
                        salary.NoLate = s.NoLate;
                        salary.NoWorkStandard = s.NoWorkStandard;
                        salary.EmployeeId = s.EmployeeId;
                        salary.EmployeeName =
                            _context.Employee.FirstOrDefault(x => x.Id == s.EmployeeId).Fullname == null
                                ? string.Empty
                                : _context.Employee.FirstOrDefault(x => x.Id == s.EmployeeId).Fullname;
                        salary.PercentPersonalTaxRate = s.PercentPersonalTaxRate;

                        list.Add(salary);
                        return HandleSuccess(list);
                    }
                    
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            }
            catch (SalaryNotFoundException e)
            {
                res.Status = SalaryStatus.SalaryNotFound;
                res.Value = e.Message;
            }

            return Ok(res);
        }
        [HttpPost]
        [Route("get-salary")]
        public IActionResult GetSalary([FromBody] Req<SalaryModelReq> req)
        {
            Res res = new Res();

            try
            {
                var salary = _context.Salary.FirstOrDefault(x =>
                    x.EmployeeId == req.value.EmployeeId && x.Month == req.value.Month && x.Year == req.value.Year);
                if (salary == null)
                    throw new SalaryNotFoundException();
                var salaryMonth = new SalaryRes();
                try
                {
                    salaryMonth.EmployeeId = req.value.EmployeeId;

                    var allowanceCall = _context.EmployeeAllowance.FirstOrDefault(x =>
                        x.EmployeeId == req.value.EmployeeId && x.AllowanceId == "PC21201904".Trim());

                    if (allowanceCall == null)
                    {
                        salary.AllowanceCall = 0;
                    }

                    else salary.AllowanceCall =
                        _context.Allowance.FirstOrDefault(x => x.Id == allowanceCall.AllowanceId).Factor == null
                            ? 0
                            : Convert.ToDecimal(_context.Allowance
                                .FirstOrDefault(x => x.Id == allowanceCall.AllowanceId).Factor);
                    salaryMonth.AllowanceCall = salary.AllowanceCall;

                    salaryMonth.NoWorkStandard = salary.NoWorkStandard;

                    var allowanceHaveLunch = _context.EmployeeAllowance.FirstOrDefault(x =>
                        x.EmployeeId == req.value.EmployeeId && x.AllowanceId == "PC21201901".Trim());

                    if (allowanceHaveLunch == null)
                    {
                        salary.AllowanceHaveLunch = 0;
                    }

                    else
                    {
                        salary.AllowanceHaveLunch =
                            _context.Allowance.FirstOrDefault(x => x.Id == allowanceHaveLunch.AllowanceId).Factor == null
                                ? 0
                                : Convert.ToDecimal(_context.Allowance
                                      .FirstOrDefault(x => x.Id == allowanceHaveLunch.AllowanceId).Factor) *
                                  Convert.ToDecimal(salary.NoWork);
                    }
                    salaryMonth.AllowanceHaveLunch = salary.AllowanceHaveLunch;

                    var allowanceParking = _context.EmployeeAllowance.FirstOrDefault(x =>
                        x.EmployeeId == req.value.EmployeeId && x.AllowanceId == "PC21201902".Trim());

                    if (allowanceParking == null)
                    {
                        salary.AllowanceParking = 0;
                    }
                    else
                    {
                        salary.AllowanceParking =
                            _context.Allowance.FirstOrDefault(x => x.Id == allowanceParking.AllowanceId).Factor == null
                                ? 0
                                : Convert.ToDecimal(_context.Allowance
                                    .FirstOrDefault(x => x.Id == allowanceParking.AllowanceId).Factor);
                    }
                    
                    salaryMonth.AllowanceParking = salary.AllowanceParking;

                    var allowanceOther = _context.EmployeeAllowance.FirstOrDefault(x =>
                        x.EmployeeId == req.value.EmployeeId && x.AllowanceId == "PC21201903".Trim());

                    if (allowanceOther == null)
                    {
                        salary.AllowanceOther = 0;
                    }

                    else salary.AllowanceOther =
                        _context.Allowance.FirstOrDefault(x => x.Id == allowanceOther.AllowanceId).Factor == null
                            ? 0
                            : Convert.ToDecimal(_context.Allowance
                                .FirstOrDefault(x => x.Id == allowanceOther.AllowanceId).Factor);

                    salaryMonth.AllowanceOther = salary.AllowanceOther;

                    var monthYear = Convert.ToDateTime("31-" + salary.Month.ToString() + "-" + salary.Year.ToString());

                    var contractId = _context.EmployeeContract.FirstOrDefault(x =>
                            x.EmployeeId == salary.EmployeeId && (x.SigningDate).Subtract(x.StartDate).Days >= 0 &&
                            (x.SigningDate).Subtract(x.EndDate).Days < 0 && x.StartDate.Subtract(monthYear).Days <= 0 &&
                            x.EndDate.Subtract(monthYear).Days >= 0)
                        .ContractId;
                    if (contractId == null)
                        throw new ContractNotFoundException();
                    salary.SalaryBasic = _context.Contracts.FirstOrDefault(x => x.Id == contractId).SalaryFactor;
                    salaryMonth.SalaryBasic = salary.SalaryBasic;

                    salary.SalaryPerDay = salary.SalaryBasic / Convert.ToDecimal(salary.NoWorkStandard);
                    salaryMonth.SalaryPerDay = salary.SalaryPerDay;


                    var disciplineKPITest = _context.EmployeeDiscipline.FirstOrDefault(x =>
                        x.EmployeeId == req.value.EmployeeId && x.DisciplineId == "KL111201801".Trim());

                    if (disciplineKPITest == null )
                    {
                        salary.DisciplineKpi = 0;
                    }
                    else
                    {
                        var disciplineKpi =
                            _context.Discipline.FirstOrDefault(x => x.Id == disciplineKPITest.DisciplineId);
                        if (disciplineKpi.PercentDiscipline == null)
                        {
                            salary.DisciplineKpi = 0;
                            salaryMonth.DisciplineKPI = salary.DisciplineKpi;
                        }

                        else salary.DisciplineKpi = Convert.ToDecimal(disciplineKpi.PercentDiscipline);
                    }
                    
                    salaryMonth.DisciplineKPI = salary.DisciplineKpi;

                    var disciplineLateTest = _context.EmployeeDiscipline.FirstOrDefault(x =>
                        x.EmployeeId == req.value.EmployeeId && x.DisciplineId == "KL111201802".Trim());

                    if (disciplineLateTest == null)
                    {
                        salary.DisciplineLateMoney = 0;
                    }
                    else
                    {
                        var disciplineLate =
                            _context.Discipline.FirstOrDefault(x => x.Id == disciplineLateTest.DisciplineId);
                        if (disciplineLate.Type == 0 && disciplineLate.PercentDiscipline == null)
                        {
                            salary.DisciplineLateMoney = 0;
                        }

                        else salary.DisciplineLateMoney =
                            Convert.ToDecimal(disciplineLate.Amount) * Convert.ToDecimal(salary.NoLate);
                    }
                    salaryMonth.DisciplineLateMoney = salary.DisciplineLateMoney;

                    var disciplineOtherTest = _context.EmployeeDiscipline.FirstOrDefault(x =>
                        x.EmployeeId == req.value.EmployeeId && x.DisciplineId == "KL111201803".Trim());

                    if (disciplineOtherTest == null)
                    {
                        salary.DisciplineOther = 0;
                        salaryMonth.DisciplineOther = 0;
                    }
                    else
                    {
                        var disciplineOther =
                            _context.Discipline.FirstOrDefault(x => x.Id == disciplineOtherTest.DisciplineId);
                        if (disciplineOther.Amount == null)
                        {
                            salary.DisciplineOther = 0;
                        }

                        else salary.DisciplineOther = Convert.ToDecimal(disciplineOther.Amount);
                    }
                    
                    salaryMonth.DisciplineOther = salary.DisciplineOther;


                    var percentInsuranceTest = _context.EmployeeInsurrance.FirstOrDefault(x =>
                        x.EmployeeId == req.value.EmployeeId && x.InsurranceId == "BH21201901".Trim());

                    if (percentInsuranceTest == null)
                    {
                        salary.PercentInsurance = 0;
                    }
                    else
                    {
                        var percentInsurance =
                            _context.Insurrance.FirstOrDefault(x => x.Id == percentInsuranceTest.InsurranceId);
                        if (percentInsurance.PercentTax == null)
                        {
                            salary.PercentInsurance = 0;
                        }

                        else salary.PercentInsurance = Convert.ToDecimal(percentInsurance.Amount);
                    }
                    
                    salaryMonth.PercentInsurance = salary.PercentInsurance;



                    var rewardKPITest = _context.EmployeeReward.FirstOrDefault(x =>
                        x.EmployeeId == req.value.EmployeeId && x.RewardId == "KT111201801".Trim());

                    if (rewardKPITest == null)
                    {
                        salary.RewardKpi = 0;
                    }
                    else
                    {
                        var rewardKpi =
                            _context.Reward.FirstOrDefault(x => x.Id == rewardKPITest.RewardId);
                        if (rewardKpi.PercentReward == null)
                        {
                            salary.RewardKpi = 0;
                        }

                        else salary.RewardKpi = Convert.ToDecimal(rewardKpi.PercentReward);
                    }
                    
                    salaryMonth.RewardKPI = salary.RewardKpi;

                    var rewardOtherTest = _context.EmployeeReward.FirstOrDefault(x =>
                        x.EmployeeId == req.value.EmployeeId && x.RewardId == "KT111201802".Trim());

                    if (rewardOtherTest == null)
                    {
                        salary.RewardOther = 0;
                    }
                    else
                    {
                        var rewardOther =
                            _context.Reward.FirstOrDefault(x => x.Id == rewardOtherTest.RewardId);
                        if (rewardOther.Amount == null)
                        {
                            salary.RewardOther = 0;
                        }

                        else salary.RewardOther = Convert.ToDecimal(rewardOther.Amount);
                    }
                    
                    salaryMonth.RewardOther = salary.RewardOther;


                    var salaryByNoWork = Convert.ToDecimal(salary.SalaryPerDay * Convert.ToDecimal(salary.NoWork)) +
                                         Convert.ToDecimal(salary.AllowanceCall) +
                                         Convert.ToDecimal(salary.AllowanceHaveLunch) +
                                         Convert.ToDecimal(salary.AllowanceParking) +
                                         Convert.ToDecimal(salary.AllowanceOther) -
                                         Convert.ToDecimal(salary.PercentInsurance) * salary.SalaryBasic +
                                         Convert.ToDecimal(salary.RewardKpi) * salary.SalaryBasic +
                                         Convert.ToDecimal(salary.RewardOther);
                    var desciplineMoney = salary.DisciplineLateMoney + salary.DisciplineKpi * salary.SalaryBasic +
                                     salary.DisciplineOther;
                    var taxTNCN = _context.PersonalTaxRate.FirstOrDefault(x => x.Id == "TAX01".Trim());
                    if (taxTNCN == null) salary.PercentPersonalTaxRate = 0;
                    salary.PercentPersonalTaxRate = Convert.ToDecimal(taxTNCN.Scale);
                    var sumSalary = salaryByNoWork - desciplineMoney;
                    if (sumSalary >= Convert.ToDecimal(taxTNCN.MinLevel) &&
                        sumSalary <= Convert.ToDecimal(taxTNCN.MaxLevel))
                        salary.SalaryReal = Convert.ToDecimal(sumSalary) * salary.PercentPersonalTaxRate;

                    salaryMonth.SalaryReal = salary.SalaryReal;
                    salaryMonth.NoWork = salary.NoWork;

                    _context.SaveChanges();

                    return HandleSuccess(salaryMonth);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            catch (SalaryNotFoundException e)
            {
                res.Status = SalaryStatus.SalaryNotFound;
                res.Value = e.Message;
            }
            catch (ContractNotFoundException e)
            {
                res.Status = ContractStatus.ContractNotFound;
                res.Value = e.Message;
            }

            return Ok(res);
        }
        private IActionResult HandleSuccess(object data)
        {
            return Ok(new Res(data));
        }
    }
}
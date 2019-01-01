using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                var salaryMonth=new SalaryRes();
                try
                {
                    salaryMonth.AllowanceCall = req.value.AllowanceCall;

                    salary.AllowanceHaveLunch = req.value.AllowanceHaveLunchPerDay * Convert.ToDecimal(salary.NoWork);
                    salaryMonth.AllowanceHaveLunch = salary.AllowanceHaveLunch;

                    salary.AllowanceParking = req.value.AllowanceParking;
                    salaryMonth.AllowanceParking = salary.AllowanceParking;

                    salary.AllowanceOther = req.value.AllowanceOther;
                    salaryMonth.AllowanceOther = salary.AllowanceOther;

                    salary.DisciplineKPI = req.value.DisciplineKPI;
                    salaryMonth.DisciplineKPI = salary.DisciplineKPI;

                    salary.DisciplineLateMoney =
                        req.value.DisciplineLateMoneyNumberOfTime * Convert.ToDecimal(salary.NoLate);
                    salaryMonth.DisciplineLateMoney = salary.DisciplineLateMoney;

                    salary.DisciplineOther = req.value.DisciplineOther;
                    salaryMonth.DisciplineOther = salary.DisciplineOther;

                    salary.PercentInsurance = req.value.PercentInsurance;
                    salaryMonth.PercentInsurance = salary.PercentInsurance;

                    salary.PercentPersonalTaxRate = req.value.PercentPersonalTaxRate;
                    salaryMonth.PercentPersonalTaxRate = salary.PercentPersonalTaxRate;

                    salary.RewardKPI = req.value.RewardKPI;
                    salaryMonth.RewardKPI = salary.RewardKPI;

                    salary.RewardOther = req.value.RewardOther;
                    salaryMonth.RewardOther = salary.RewardOther;

                    var monthYear = Convert.ToDateTime("31-"+salary.Month.ToString()+"-" + salary.Year.ToString());

                    var contractId = _context.EmployeeContract.FirstOrDefault(x =>
                            x.EmployeeId == salary.EmployeeId && (x.SigningDate).Subtract(x.StartDate).Days >= 0 &&
                            (x.SigningDate).Subtract(x.EndDate).Days < 0 && x.StartDate.Subtract(monthYear).Days <= 0 &&
                            x.EndDate.Subtract(monthYear).Days >= 0)
                        .ContractId;
                    if (contractId == null || contractId == String.Empty) salary.SalaryBasic = req.value.SalaryBasic;
                    salary.SalaryBasic = _context.Contracts.FirstOrDefault(x => x.Id == contractId).SalaryFactor;
                    salaryMonth.SalaryBasic = salary.SalaryBasic;

                    salary.SalaryPerDay = salary.SalaryBasic / Convert.ToDecimal(salary.NoWorkStandard);
                    salaryMonth.SalaryPerDay = salary.SalaryPerDay;

                    salary.DisciplineLeaveWithoutPermission =
                        Convert.ToDecimal(salary.NoLeaveWithoutPermission) * req.value.DisciplineLeaveWithoutPermission;
                    salaryMonth.DisciplineLeaveWithoutPermission = salary.DisciplineLeaveWithoutPermission;
                    salary.SalaryReal = Convert.ToDecimal(salary.SalaryPerDay * Convert.ToDecimal(salary.NoWork)) +
                                        Convert.ToDecimal(salary.AllowanceHaveLunch) + Convert.ToDecimal(salary.AllowanceCall) + Convert.ToDecimal(salary.AllowanceOther) +
                                        Convert.ToDecimal(salary.AllowanceParking) + Convert.ToDecimal(salary.RewardOther + salary.RewardKPI) -
                                        Convert.ToDecimal(salary.DisciplineLateMoney) - Convert.ToDecimal(salary.DisciplineLeaveWithoutPermission) -
                                        Convert.ToDecimal(salary.DisciplineKPI) - Convert.ToDecimal(salary.DisciplineOther) -
                                        Convert.ToDecimal(salary.SalaryBasic * salary.PercentInsurance) -
                                        Convert.ToDecimal(salary.SalaryBasic * salary.PercentPersonalTaxRate);
                    salaryMonth.SalaryReal = salary.SalaryReal;
                    salaryMonth.NoWork = salary.NoWork;
                    salaryMonth.NoGetVacation = salary.NoGetVacation;
                    salaryMonth.NoLate = salary.NoLate;
                    salaryMonth.NoLeaveWithPermission = salary.NoLeaveWithPermission;
                    salaryMonth.NoLeaveWithoutPermission = salary.NoLeaveWithoutPermission;

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

            return Ok(res);
        }
        private IActionResult HandleSuccess(object data)
        {
            return Ok(new Res(data));
        }
    }
}
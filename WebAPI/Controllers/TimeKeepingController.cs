using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ExceptionModel.EmployeeException;
using WebAPI.ExceptionModel.SalaryException;
using WebAPI.ExceptionModel.TimeKeepingException;
using WebAPI.Model;
using WebAPI.Model.Constant;
using WebAPI.Model.TimeKeeping;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/timekeeping")]
    [ApiController]
    public class TimeKeepingController : ControllerBase
    {
        private readonly QuanLyNhanSuContext _context;

        public TimeKeepingController(QuanLyNhanSuContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateTimeKeepingDay([FromBody] Req<TimeKeepingDayModelReq> req)
        {
            Res res = new Res();

            try
            {
                var timeKeepingDay = new TimeKeeping();
                if (_context.Employee.FirstOrDefault(x=>x.Id==req.value.EmployeeId)==null)
                    throw new EmployeeNotFoundException();
                var check = _context.TimeKeeping.Any(x =>
                    x.EmployeeId == req.value.EmployeeId && x.Date == Convert.ToDateTime(req.value.Date));
                if (check) throw new TimeKeepingAlreadyExistException();
                try
                {
                    timeKeepingDay.EmployeeId = req.value.EmployeeId;
                    timeKeepingDay.Date = Convert.ToDateTime(req.value.Date).Date;
                    timeKeepingDay.TimeIn = Convert.ToDateTime(req.value.TimeIn).TimeOfDay;
                    timeKeepingDay.TimeOut = Convert.ToDateTime(req.value.TimeOut).TimeOfDay;
                    
                    var timeInOutStandard = _context.ConfigureTimeWork.FirstOrDefault(x => x.Id == 1);
                    var late = Convert.ToDateTime(req.value.TimeIn).TimeOfDay.Subtract(timeInOutStandard.TimeIn1)
                        .Minutes;
                    if (late > 0) timeKeepingDay.IsLate = true;
                    else timeKeepingDay.IsLate = false;
                    var date = DateTime.Today.ToShortDateString();
                    var extractTimeWork= Convert.ToDateTime(req.value.TimeOut).TimeOfDay.Subtract(timeInOutStandard.TimeOut2)
                        .Hours;
                    if (extractTimeWork >= 4) timeKeepingDay.ExtractTimeWork = extractTimeWork;
                    else extractTimeWork = 0;
                    var timeWorksPerDay = (Convert.ToDouble(Convert.ToDateTime(req.value.TimeOut).TimeOfDay
                        .Subtract(Convert.ToDateTime(req.value.TimeIn).TimeOfDay)
                        .Hours));

                    timeKeepingDay.NoTimeWork = timeWorksPerDay;
                    if (Convert.ToDateTime(req.value.Date).DayOfWeek == DayOfWeek.Sunday)
                    {
                        if (timeWorksPerDay < (timeInOutStandard.TimeOut1).Subtract(timeInOutStandard.TimeIn1).Hours)
                            timeKeepingDay.NoWork = 0;
                        else
                        {
                            if (timeWorksPerDay < (timeInOutStandard.TimeOut2).Subtract(timeInOutStandard.TimeIn1).Hours)
                             timeKeepingDay.NoWork = 0.75;
                            else
                            {
                                if (timeWorksPerDay < (timeInOutStandard.TimeOut3)
                                        .Subtract(timeInOutStandard.TimeIn1).Hours)
                                    timeKeepingDay.NoWork = 1.5;
                                else timeKeepingDay.NoWork = 2.25;
                            }
                        }
                    }
                    if (timeWorksPerDay < (timeInOutStandard.TimeOut1).Subtract(timeInOutStandard.TimeIn1).Hours)
                        timeKeepingDay.NoWork = 0;
                    else
                    {
                        if (timeWorksPerDay < (timeInOutStandard.TimeOut2).Subtract(timeInOutStandard.TimeIn1).Hours)
                            timeKeepingDay.NoWork = 0.75;
                        else
                        {
                            if (timeWorksPerDay < (timeInOutStandard.TimeOut3)
                                .Subtract(timeInOutStandard.TimeIn1).Hours)
                                timeKeepingDay.NoWork = 1.5;
                            else timeKeepingDay.NoWork = 2.25;
                        }
                    }

                    timeKeepingDay.UpdatedDate = DateTime.Today;

                    _context.TimeKeeping.Add(timeKeepingDay);
                    _context.SaveChanges();
                }
                catch (Exception e) { }

                return HandleSuccess(res.Value = "Success created!");
            }
            catch (EmployeeNotFoundException e)
            {
                res.Status = EmployeeStatus.EmployeeNotFound;
                res.Value = e.Message;
            }
            catch (TimeKeepingAlreadyExistException e)
            {
                res.Status = TimeKeepingStatus.TimeKeepingAlreadyExist;
                res.Value = e.Message;
            }

            return Ok(res);
        }

        [HttpPost]
        [Route("update")]
        public IActionResult UpdateTimeKeepingDay([FromBody] Req<TimeKeepingDayModelReq> req)
        {
            Res res = new Res();

            try
            {
                var timeKeepingDay = _context.TimeKeeping.FirstOrDefault(x => x.Date == Convert.ToDateTime(req.value.Date));
                if (timeKeepingDay == null || timeKeepingDay.EmployeeId != req.value.EmployeeId)
                    throw new TimeKeepingNotFoundException();

                if (_context.Employee.FirstOrDefault(x => x.Id == req.value.EmployeeId) == null)
                    throw new EmployeeNotFoundException();
                try
                {
                    timeKeepingDay.TimeIn = Convert.ToDateTime(req.value.TimeIn).TimeOfDay;
                    timeKeepingDay.TimeOut = Convert.ToDateTime(req.value.TimeOut).TimeOfDay;
                    var timeInOutStandard = _context.ConfigureTimeWork.FirstOrDefault(x => x.Id == 1);
                    var late = Convert.ToDateTime(req.value.TimeIn).TimeOfDay.Subtract(timeInOutStandard.TimeIn1)
                        .Minutes;
                    if (late > 0) timeKeepingDay.IsLate = true;
                    else timeKeepingDay.IsLate = false;
                    var extractTimeWork = Convert.ToDateTime(req.value.TimeOut).TimeOfDay.Subtract(timeInOutStandard.TimeOut2)
                        .Hours;
                    if (extractTimeWork >= 4) timeKeepingDay.ExtractTimeWork = extractTimeWork;
                    else extractTimeWork = 0;
                    var timeWorksPerDay = (Convert.ToDouble(Convert.ToDateTime(req.value.TimeOut).TimeOfDay
                        .Subtract(Convert.ToDateTime(req.value.TimeIn).TimeOfDay)
                        .Hours));
                    
                    timeKeepingDay.NoTimeWork = timeWorksPerDay;
                    if (Convert.ToDateTime(req.value.Date).DayOfWeek == DayOfWeek.Sunday)
                    {
                        if (timeWorksPerDay < (timeInOutStandard.TimeOut1).Subtract(timeInOutStandard.TimeIn1).Hours)
                            timeKeepingDay.NoWork = 0;
                        else
                        {
                            if (timeWorksPerDay < (timeInOutStandard.TimeOut2).Subtract(timeInOutStandard.TimeIn1).Hours)
                                timeKeepingDay.NoWork = 0.75;
                            else
                            {
                                if (timeWorksPerDay < (timeInOutStandard.TimeOut3)
                                    .Subtract(timeInOutStandard.TimeIn1).Hours)
                                    timeKeepingDay.NoWork = 1.5;
                                else timeKeepingDay.NoWork = 2.25;
                            }
                        }
                    }
                    if (timeWorksPerDay < (timeInOutStandard.TimeOut1).Subtract(timeInOutStandard.TimeIn1).Hours)
                        timeKeepingDay.NoWork = 0;
                    else
                    {
                        if (timeWorksPerDay < (timeInOutStandard.TimeOut2).Subtract(timeInOutStandard.TimeIn1).Hours)
                            timeKeepingDay.NoWork = 0.75;
                        else
                        {
                            if (timeWorksPerDay < (timeInOutStandard.TimeOut3)
                                .Subtract(timeInOutStandard.TimeIn1).Hours)
                                timeKeepingDay.NoWork = 1.5;
                            else timeKeepingDay.NoWork = 2.25;
                        }
                    }

                    timeKeepingDay.UpdatedDate = DateTime.Today;

                    _context.SaveChanges();
                }
                catch (Exception e) { }

                return HandleSuccess(res.Value = "Success created!");
            }
            catch (EmployeeNotFoundException e)
            {
                res.Status = EmployeeStatus.EmployeeNotFound;
                res.Value = e.Message;
            }

            return Ok(res);
        }

        [HttpPost]
        [Route("get-timekeeping-day")]
        public IActionResult GetTimeKeepingDay([FromBody] Req<TimeKeepingDayReq> req)
        {
            Res res = new Res();

            try
            {
                var timeKeepingDay = new TimeKeepingDayRes();
                var time = _context.TimeKeeping.FirstOrDefault(x => x.Date == Convert.ToDateTime(req.value.Date));
                if (time == null || time.EmployeeId != req.value.EmployeeId)
                    throw new TimeKeepingNotFoundException();
                if (_context.Employee.FirstOrDefault(x => x.Id == time.EmployeeId) == null)
                    throw new EmployeeNotFoundException();
                try
                {
                    timeKeepingDay.EmployeeId = time.EmployeeId;
                    timeKeepingDay.EmployeeName =
                        _context.Employee.FirstOrDefault(x => x.Id == time.EmployeeId).Fullname == null
                            ? string.Empty
                            : _context.Employee.FirstOrDefault(x => x.Id == time.EmployeeId).Fullname;
                    timeKeepingDay.Date = time.Date.ToString();
                    timeKeepingDay.TimeIn = time.TimeIn.ToString();
                    timeKeepingDay.TimeOut = time.TimeOut.ToString();
                    timeKeepingDay.IsLate = time.IsLate;
                    timeKeepingDay.ExtractTimeWork = time.ExtractTimeWork;
                    timeKeepingDay.NoTimeWork = time.NoTimeWork;
                    timeKeepingDay.NoWork = time.NoWork;
                    timeKeepingDay.UpdatedBy = time.UpdatedBy;
                    timeKeepingDay.UpdatedDate = time.UpdatedDate.ToString();
                }
                catch (Exception e) { }

                return HandleSuccess(timeKeepingDay);
            }
            catch (EmployeeNotFoundException e)
            {
                res.Status = EmployeeStatus.EmployeeNotFound;
                res.Value = e.Message;
            }
            catch (TimeKeepingNotFoundException e)
            {
                res.Status = TimeKeepingStatus.TimeKeepingNotFound;
                res.Value = e.Message;
            }

            return Ok(res);
        }

        [HttpPost]
        [Route("get-time-keeping-month")]
        public IActionResult GetTimeKeepingMonth([FromBody] Req<TimeKeepingMonthModelReq> req)
        {
            Res res = new Res();
            try
            {
                if (_context.Employee.FirstOrDefault(x => x.Id == req.value.EmployeeId) == null)
                    throw new EmployeeNotFoundException();
                var checkAny = _context.TimeKeeping.Any(x =>
                    x.EmployeeId == req.value.EmployeeId && x.Date.Month == req.value.Month &&
                    x.Date.Year == req.value.Year);
                if(!checkAny) throw new TimeKeepingNotFoundException();
                var months = _context.TimeKeeping.Where(x =>
                    x.EmployeeId == req.value.EmployeeId && x.Date.Month == req.value.Month &&
                    x.Date.Year == req.value.Year);

                var timeMonthRes = new TimeKeepingMonthRes();
                //if (_context.Salary.Any(x =>
                //    x.EmployeeId == req.value.EmployeeId && x.Month == req.value.Month && x.Year == req.value.Year))
                //    throw new SalaryAlreadyExistException();
                
                var salary = new Salary();
                try
                {
                    salary.EmployeeId = req.value.EmployeeId;
                    salary.Month = req.value.Month;
                    salary.Year = req.value.Year;

                    timeMonthRes.EmployeeId = req.value.EmployeeId;
                    timeMonthRes.Month = req.value.Month;
                    timeMonthRes.Year = req.value.Year;

                    timeMonthRes.NoLate =Convert.ToDouble(months.Where(x => x.IsLate == true).Count());
                    salary.NoLate = timeMonthRes.NoLate;

                    timeMonthRes.NoWork = Convert.ToDouble(months.Sum(x => x.NoWork));
                    salary.NoWork = timeMonthRes.NoWork;
                    salary.NoWorkStandard = req.value.NoWorkStandard;

                    timeMonthRes.TimeKeepingDay = months.Select(t => new TimeKeepingDayRes
                    {
                        EmployeeId = t.EmployeeId,
                        EmployeeName =
                            _context.Employee.FirstOrDefault(x => x.Id == t.EmployeeId).Fullname == null
                                ? string.Empty
                                : _context.Employee.FirstOrDefault(x => x.Id == t.EmployeeId).Fullname,
                        NoWork = t.NoWork,
                        IsLate = t.IsLate,
                        Date = t.Date.ToShortDateString(),
                        ExtractTimeWork = t.ExtractTimeWork,
                        NoTimeWork = t.NoTimeWork,
                        Note = t.Note,
                        TimeIn = t.TimeIn.ToString(),
                        TimeOut = t.TimeOut.ToString(),
                        UpdatedBy = t.UpdatedBy,
                        UpdatedDate = t.UpdatedDate.ToString()
                    }).ToList();
                    
                    _context.SaveChanges();

                    return HandleSuccess(timeMonthRes);
                }
                
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            catch (EmployeeNotFoundException e)
            {
                res.Status = EmployeeStatus.EmployeeNotFound;
                res.Value = e.Message;
            }
            catch (TimeKeepingNotFoundException e)
            {
                res.Status = TimeKeepingStatus.TimeKeepingNotFound;
                res.Value = e.Message;
            }
            catch (SalaryAlreadyExistException e)
            {
                res.Status = SalaryStatus.SalaryAlreadyExist;
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
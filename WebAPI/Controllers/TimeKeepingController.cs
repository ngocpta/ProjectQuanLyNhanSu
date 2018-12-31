﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ExceptionModel.EmployeeException;
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
                    timeKeepingDay.IsGetVacation = req.value.IsGetVacation;
                    timeKeepingDay.IsLeaveWithPermission = req.value.IsLeaveWithPermission;
                    timeKeepingDay.IsLeaveWithoutPermission = req.value.IsLeaveWithoutPermission;
                    var timeInOutStandard = _context.ConfigureTimeWork.FirstOrDefault(x => x.Id == 1);
                    var late = Convert.ToDateTime(req.value.TimeIn).TimeOfDay.Subtract(timeInOutStandard.TimeIn1)
                        .Minutes;
                    if (late > 0) timeKeepingDay.IsLate = true;
                    timeKeepingDay.IsLate = false;
                    var extractTimeWork= Convert.ToDateTime(req.value.TimeOut).TimeOfDay.Subtract(timeInOutStandard.TimeOut2)
                        .Hours;
                    if (extractTimeWork >= 4) timeKeepingDay.ExtractTimeWork = extractTimeWork;
                    extractTimeWork = 0;
                    var timeWorksPerDay = Convert.ToDateTime(req.value.TimeOut).TimeOfDay
                                              .Subtract(Convert.ToDateTime(req.value.TimeIn).TimeOfDay)
                                              .Hours - 2;
                    timeKeepingDay.NoTimeWork = timeWorksPerDay;
                    if (Convert.ToDateTime(req.value.Date).DayOfWeek==DayOfWeek.Sunday)
                    {
                        if (timeWorksPerDay < 4) timeKeepingDay.NoWork = 0;
                        else
                        {
                            if (timeWorksPerDay < 8) timeKeepingDay.NoWork = 0.75;
                            else
                            {
                                if (timeWorksPerDay < 12) timeKeepingDay.NoWork = 1.5;
                                else timeKeepingDay.NoWork = 2.25;
                            }
                        }
                    }
                    if (timeWorksPerDay < 4) timeKeepingDay.NoWork = 0;
                    else
                    {
                        if (timeWorksPerDay < 8) timeKeepingDay.NoWork = 0.5;
                        else
                        {
                            if (timeWorksPerDay < 12) timeKeepingDay.NoWork = 1;
                            else timeKeepingDay.NoWork = 1.5;
                        }
                    }

                    timeKeepingDay.UpdatedBy = req.value.UpDatedBy;
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
                    timeKeepingDay.IsGetVacation = req.value.IsGetVacation;
                    timeKeepingDay.IsLeaveWithPermission = req.value.IsLeaveWithPermission;
                    timeKeepingDay.IsLeaveWithoutPermission = req.value.IsLeaveWithoutPermission;
                    var timeInOutStandard = _context.ConfigureTimeWork.FirstOrDefault(x => x.Id == 1);
                    var late = Convert.ToDateTime(req.value.TimeIn).TimeOfDay.Subtract(timeInOutStandard.TimeIn1)
                        .Minutes;
                    if (late > 0) timeKeepingDay.IsLate = true;
                    timeKeepingDay.IsLate = false;
                    var extractTimeWork = Convert.ToDateTime(req.value.TimeOut).TimeOfDay.Subtract(timeInOutStandard.TimeOut2)
                        .Hours;
                    if (extractTimeWork >= 4) timeKeepingDay.ExtractTimeWork = extractTimeWork;
                    extractTimeWork = 0;
                    var timeWorksPerDay = Convert.ToDateTime(req.value.TimeOut).TimeOfDay
                                              .Subtract(Convert.ToDateTime(req.value.TimeIn).TimeOfDay)
                                              .Hours - 2;
                    timeKeepingDay.NoTimeWork = timeWorksPerDay;
                    if (Convert.ToDateTime(req.value.Date).DayOfWeek == DayOfWeek.Sunday)
                    {
                        if (timeWorksPerDay < 4) timeKeepingDay.NoWork = 0;
                        else
                        {
                            if (timeWorksPerDay < 8) timeKeepingDay.NoWork = 0.75;
                            else
                            {
                                if (timeWorksPerDay < 12) timeKeepingDay.NoWork = 1.5;
                                else timeKeepingDay.NoWork = 2.25;
                            }
                        }
                    }
                    if (timeWorksPerDay < 4) timeKeepingDay.NoWork = 0;
                    else
                    {
                        if (timeWorksPerDay < 8) timeKeepingDay.NoWork = 0.5;
                        else
                        {
                            if (timeWorksPerDay < 12) timeKeepingDay.NoWork = 1;
                            else timeKeepingDay.NoWork = 1.5;
                        }
                    }

                    timeKeepingDay.UpdatedBy = req.value.UpDatedBy;
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
                    timeKeepingDay.IsGetVacation = time.IsGetVacation;
                    timeKeepingDay.IsLeaveWithPermission = time.IsLeaveWithPermission;
                    timeKeepingDay.IsLeaveWithoutPermission = time.IsLeaveWithoutPermission;
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
                var timeMonth=new TimeKeepingMonthRes();
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return Ok(res);
        }


        private IActionResult HandleSuccess(object data)
        {
            return Ok(new Res(data));
        }
    }
}
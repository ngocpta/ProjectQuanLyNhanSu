using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ExceptionModel.AllowanceException;
using WebAPI.ExceptionModel.EmployeeException;
using WebAPI.Model;
using WebAPI.Model.Allowance;
using WebAPI.Model.Constant;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/employee-allowance")]
    [ApiController]
    public class EmployeeAllowanceController : ControllerBase
    {
        private readonly QuanLyNhanSuContext _context;

        public EmployeeAllowanceController(QuanLyNhanSuContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("get-all")]
        public IActionResult GetAll()
        {
            Res res = new Res();
            try
            {
                var allowance = _context.EmployeeAllowance;
                if (allowance == null)
                    throw new EmployeeAllowanceNotFoundException();
                var list = new List<EmployeeAllowanceRes>();

                foreach (var a in allowance)
                {
                    var al = new EmployeeAllowanceRes();
                    try
                    {
                        al.Id = a.Id;
                        al.AllowanceId = a.AllowanceId;
                        al.Note = a.Note;
                        al.SigningDate = a.SigningDate.ToString();
                    }
                    catch (Exception e) { }
                    list.Add(al);
                }

                return HandleSuccess(list);
            }
            catch (EmployeeAllowanceNotFoundException e)
            {
                res.Status = AllowanceStatus.EmployeeAllowanceNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] Req<EmployeeAllowanceModelReq> req)
        {
            Res res = new Res();

            try
            {
                EmployeeAllowance allowance = new EmployeeAllowance();
                try
                {
                    var checkId = _context.EmployeeAllowance.Any(m => m.Id == req.value.Id);
                    if (checkId)
                        throw new EmployeeAllowanceAlreadyExistException();
                    if (_context.Employee.FirstOrDefault(x => x.Id == req.value.EmployeeId) == null)
                        throw new EmployeeNotFoundException();
                    if(_context.Allowance.FirstOrDefault(x=>x.Id==req.value.AllowanceId)==null)
                        throw new AllowanceNotFoundException();
                    allowance.Id = "NVPC" + Convert.ToString(DateTime.Today.Day) +
                                   Convert.ToString(DateTime.Today.Month) + Convert.ToString(DateTime.Today.Year) +
                                   req.value.Id;
                    allowance.AllowanceId = req.value.AllowanceId;
                    allowance.Note = req.value.Note;
                    allowance.SigningDate = Convert.ToDateTime(req.value.SigningDate);
                    allowance.EmployeeId = req.value.EmployeeId;

                    _context.EmployeeAllowance.Add(allowance);
                    _context.SaveChanges();

                }

                catch (Exception e)
                {
                }

                return HandleSuccess(res.Value = "SUCCESS CREATED !");
            }
            catch (EmployeeAllowanceAlreadyExistException e)
            {
                res.Status = AllowanceStatus.EmployeeAllowanceAlreadyExist;
                res.Value = e.Message;
            }
            catch (EmployeeNotFoundException e)
            {
                res.Status = EmployeeStatus.EmployeeNotFound;
                res.Value = e.Message;
            }
            catch (AllowanceNotFoundException e)
            {
                res.Status = AllowanceStatus.AllowanceNotFound;
                res.Value = e.Message;
            }

            return Ok(res);
        }

        [HttpPost]
        [Route("update")]
        public IActionResult UpdateAllowance([FromBody] Req<EmployeeAllowanceModelReq> req)
        {
            Res res = new Res();
            var allowance = _context.EmployeeAllowance.FirstOrDefault(m => m.Id == req.value.Id);
            if (allowance == null)
                throw new EmployeeAllowanceNotFoundException();
            try
            {
                if (_context.Employee.FirstOrDefault(x => x.Id == req.value.EmployeeId) == null)
                    throw new EmployeeNotFoundException();
                if (_context.Allowance.FirstOrDefault(x => x.Id == req.value.AllowanceId) == null)
                    throw new AllowanceNotFoundException();
               
                allowance.AllowanceId = req.value.AllowanceId;
                allowance.Note = req.value.Note;
                allowance.SigningDate = Convert.ToDateTime(req.value.SigningDate);
                allowance.EmployeeId = req.value.EmployeeId;

                _context.SaveChanges();

                return HandleSuccess(res.Value = "SUCCESS UPDATED !");
            }

            catch (EmployeeAllowanceAlreadyExistException e)
            {
                res.Status = AllowanceStatus.EmployeeAllowanceAlreadyExist;
                res.Value = e.Message;
            }
            catch (EmployeeNotFoundException e)
            {
                res.Status = EmployeeStatus.EmployeeNotFound;
                res.Value = e.Message;
            }
            catch (AllowanceNotFoundException e)
            {
                res.Status = AllowanceStatus.AllowanceNotFound;
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
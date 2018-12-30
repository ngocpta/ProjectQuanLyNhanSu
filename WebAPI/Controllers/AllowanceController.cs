using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ExceptionModel.AllowanceException;
using WebAPI.Model;
using WebAPI.Model.Allowance;
using WebAPI.Model.Constant;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/allowance")]
    [ApiController]
    public class AllowanceController : ControllerBase
    {
        private readonly QuanLyNhanSuContext _context;

        public AllowanceController(QuanLyNhanSuContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("get-all-allowances")]
        public IActionResult GetAllAllowances()
        {
            Res res = new Res();
            try
            {
                var allowance = _context.Allowance;
                if (allowance == null)
                    throw new AllowanceNotFoundException();
                var list = new List<AllowanceRes>();

                foreach (var a in allowance)
                {
                    var al = new AllowanceRes();
                    try
                    {
                        al.Id = a.Id;
                        al.Name = a.Name;
                        al.EffectiveDate = a.EffectiveDate.ToString();
                        al.Factor = a.Factor;
                        al.Type = a.Type;
                        al.Note = a.Note;
                    }
                    catch (Exception e) { }
                    list.Add(al);
                }

                return HandleSuccess(list);
            }
            catch (AllowanceNotFoundException e)
            {
                res.Status = AllowanceStatus.AllowanceNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("get-allowance")]
        public IActionResult GetAllowance([FromBody] Req<AllowanceReq> req)
        {
            Res res = new Res();
            try
            {
                var allowance = _context.Allowance.FirstOrDefault(p => p.Id == req.value.Id);
                if (allowance == null)
                    throw new AllowanceNotFoundException();
                var al = new AllowanceRes();
                try
                {
                    al.Id = allowance.Id;
                    al.EffectiveDate = allowance.EffectiveDate.ToString();
                    al.Factor = allowance.Factor;
                    al.Name = allowance.Name;
                    al.Note = allowance.Note;
                    al.Type = allowance.Type;
                }
                catch (Exception e) { }

                return HandleSuccess(al);
            }
            catch (AllowanceNotFoundException e)
            {
                res.Status = DepartmentStatus.DepartmentNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateAllowance([FromBody] Req<AllowanceModelReq> req)
        {
            Res res = new Res();

            try
            {
                Allowance allowance = new Allowance();
                try
                {
                    var checkId = _context.Allowance.Any(m => m.Id == req.value.Id);
                    if (checkId)
                        throw new AllowanceAlreadyExistException();

                    allowance.Id = "ALLOWANCE" + Convert.ToString(DateTime.Today.Day) +
                                    Convert.ToString(DateTime.Today.Month) + Convert.ToString(DateTime.Today.Year) +
                                    req.value.Id;
                    allowance.EffectiveDate = Convert.ToDateTime(req.value.EffectiveDate);
                    allowance.Factor = req.value.Factor;
                    allowance.Name = req.value.Name;
                    allowance.Note = req.value.Note;
                    allowance.Type = req.value.Type;
                    allowance.CreatedDate = Convert.ToDateTime(DateTime.Today.ToString());

                    _context.Allowance.Add(allowance);
                    _context.SaveChanges();

                }

                catch (Exception e)
                {
                }

                return HandleSuccess(res.Value = "SUCCESS CREATED ALLOWANCE!");
            }
            catch (AllowanceAlreadyExistException e)
            {
                res.Status = AllowanceStatus.AllowanceAlreadyExist;
                res.Value = e.Message;
            }

            return Ok(res);
        }

        [HttpPost]
        [Route("update")]
        public IActionResult UpdateAllowance([FromBody] Req<AllowanceModelReq> req)
        {
            Res res = new Res();
            var allowance = _context.Allowance.FirstOrDefault(m => m.Id == req.value.Id);
            if (allowance == null)
                throw new AllowanceNotFoundException();
            try
            {
                allowance.EffectiveDate = Convert.ToDateTime(req.value.EffectiveDate);
                allowance.Factor = req.value.Factor;
                allowance.Name = req.value.Name;
                allowance.Note = req.value.Note;
                allowance.Type = req.value.Type;
                allowance.UpdatedDate = Convert.ToDateTime(DateTime.Today.ToString());

                _context.SaveChanges();

                return HandleSuccess(res.Value = "SUCCESS UPDATED ALLOWANCE!");
            }

            catch (AllowanceNotFoundException e)
            {
                res.Status = AllowanceStatus.AllowanceNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult DeleteDepartment([FromBody] Req<AllowanceReq> req)
        {
            Res res = new Res();
            var allowance = _context.Allowance.FirstOrDefault(m => m.Id == req.value.Id);
            if (allowance == null)
                throw new AllowanceNotFoundException();
            try
            {
                _context.Remove(allowance);

                _context.SaveChanges();

                return HandleSuccess(res.Value = "SUCCESS DELETED ALLOWANCE!");
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ExceptionModel.RewardAndDisciplineException;
using WebAPI.Model;
using WebAPI.Model.Constant;
using WebAPI.Model.RewardAndDiscipline;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/reward-and-discipline")]
    [ApiController]
    public class RewardAndDisciplineMethodController : ControllerBase
    {
        private readonly QuanLyNhanSuContext _context;

        public RewardAndDisciplineMethodController(QuanLyNhanSuContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("get-all-methods")]
        public IActionResult GetAllMethods()
        {
            Res res = new Res();
            try
            {
                var method = _context.RewardAndDisciplineMethod;
                if (method == null)
                    throw new MethodNotFoundException();
                var list = new List<MethodRes>();

                foreach (var m in method)
                {
                    var t = new MethodRes();
                    try
                    {
                        t.Id = m.Id;
                        t.Name = m.Name;
                        t.Reason = m.Reason;
                        t.Type = m.Type;
                    }
                    catch (Exception e) { }
                    list.Add(t);
                }

                return HandleSuccess(list);
            }
            catch (MethodNotFoundException e)
            {
                res.Status = RewardAndDisciplineStatus.RewardAndDisciplineMethodNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("get-method")]
        public IActionResult GetMethod([FromBody] Req<MethodReq> req)
        {
            Res res = new Res();
            try
            {
                var method = _context.RewardAndDisciplineMethod.FirstOrDefault(p => p.Id == req.value.Id);
                if (method == null)
                    throw new MethodNotFoundException();
                var m = new MethodRes();
                try
                {
                    m.Id = method.Id;
                    m.Name = method.Name;
                    m.Reason = method.Reason;
                    m.Type = method.Type;
                }
                catch (Exception e) { }

                return HandleSuccess(m);
            }
            catch (MethodNotFoundException e)
            {
                res.Status = RewardAndDisciplineStatus.RewardAndDisciplineMethodNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] Req<MethodModelReq> req)
        {
            Res res = new Res();

            try
            {
                RewardAndDisciplineMethod method = new RewardAndDisciplineMethod();
                try
                {
                    var checkCode = _context.RewardAndDisciplineMethod.Any(m => m.Id == req.value.Id);
                    if (checkCode)
                        throw new MethodAlreadyExistException();

                    method.Id = "KTKL" + Convert.ToString(DateTime.Today.Day) +
                                    Convert.ToString(DateTime.Today.Month) + Convert.ToString(DateTime.Today.Year) +
                                    req.value.Id;
                    method.Name = req.value.Name;
                    method.Reason = req.value.Reason;
                    method.Type = req.value.Type;

                    _context.RewardAndDisciplineMethod.Add(method);
                    _context.SaveChanges();

                }

                catch (Exception e)
                {
                }

                return HandleSuccess(res.Value = "SUCCESS CREATED!");
            }
            catch (MethodAlreadyExistException e)
            {
                res.Status = RewardAndDisciplineStatus.RewardAndDisciplineMethodAlreadyExist;
                res.Value = e.Message;
            }

            return Ok(res);
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update([FromBody] Req<MethodModelReq> req)
        {
            Res res = new Res();
            var method = _context.RewardAndDisciplineMethod.FirstOrDefault(m => m.Id == req.value.Id);
            if (method == null)
                throw new MethodNotFoundException();
            try
            {
                method.Name = req.value.Name;
                method.Type = req.value.Type;
                method.Reason = req.value.Reason;

                _context.SaveChanges();

                return HandleSuccess(res);
            }

            catch (MethodNotFoundException e)
            {
                res.Status = RewardAndDisciplineStatus.RewardAndDisciplineMethodNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult Delete([FromBody] Req<MethodReq> req)
        {
            Res res = new Res();
            var method = _context.RewardAndDisciplineMethod.FirstOrDefault(m => m.Id == req.value.Id);
            if (method == null)
                throw new MethodNotFoundException();
            try
            {
                _context.Remove(method);

                _context.SaveChanges();

                return HandleSuccess(res);
            }

            catch (MethodNotFoundException e)
            {
                res.Status = RewardAndDisciplineStatus.RewardAndDisciplineMethodNotFound;
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
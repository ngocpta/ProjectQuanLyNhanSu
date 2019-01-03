using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ExceptionModel.SpecializeException;
using WebAPI.Model;
using WebAPI.Model.Constant;
using WebAPI.Model.Specialize;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/specialize")]
    [ApiController]
    public class SpecializeController : ControllerBase
    {
        private readonly QuanLyNhanSuContext _context;

        public SpecializeController(QuanLyNhanSuContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("get-all-specializes")]
        public IActionResult GetAllPositions()
        {
            Res res = new Res();
            try
            {
                var positions = _context.Specialize;
                if (positions == null)
                    throw new SpecializeNotFoundException();
                var list = new List<SpecializeRes>();

                foreach (var d in positions)
                {
                    var de = new SpecializeRes();
                    try
                    {
                        de.Id = d.Id;
                        de.Name = d.Name;
                        de.Active = d.Active;
                    }
                    catch (Exception e) { }
                    list.Add(de);
                }

                return HandleSuccess(list);
            }
            catch (SpecializeNotFoundException e)
            {
                res.Status = SpecializeStatus.SpecializeNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("get-specialize")]
        public IActionResult GetPosition([FromBody] Req<SpecializeReq> req)
        {
            Res res = new Res();
            try
            {
                var position = _context.Specialize.FirstOrDefault(p => p.Id == req.value.Id);
                if (position == null)
                    throw new SpecializeNotFoundException();
                var de = new SpecializeRes();
                try
                {
                    de.Id = position.Id;
                    de.Name = position.Name;
                    de.Active = position.Active;
                }
                catch (Exception e) { }

                return HandleSuccess(de);
            }
            catch (SpecializeNotFoundException e)
            {
                res.Status = SpecializeStatus.SpecializeNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] Req<SpecializeModelReq> req)
        {
            Res res = new Res();

            try
            {
                Specialize position = new Specialize();
                try
                {
                    var checkCode = _context.Specialize.Any(m => m.Id == req.value.Id);
                    if (checkCode)
                        throw new SpecializeAlreadyExistException();

                    position.Id = "CV" + Convert.ToString(DateTime.Today.Day) +
                                    Convert.ToString(DateTime.Today.Month) + Convert.ToString(DateTime.Today.Year) +
                                    req.value.Id;
                    position.Active = req.value.Active;
                    position.Name = req.value.Name;

                    _context.Specialize.Add(position);
                    _context.SaveChanges();

                }

                catch (Exception e)
                {
                }

                return HandleSuccess(res.Value = "SUCCESS CREATED !");
            }
            catch (SpecializeAlreadyExistException e)
            {
                res.Status = SpecializeStatus.SpecializeAlreadyExist;
                res.Value = e.Message;
            }

            return Ok(res);
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update([FromBody] Req<SpecializeModelReq> req)
        {
            Res res = new Res();
            var position = _context.Specialize.FirstOrDefault(m => m.Id == req.value.Id);
            if (position == null)
                throw new SpecializeNotFoundException();
            try
            {
                position.Active = req.value.Active;
                position.Name = req.value.Name;

                _context.SaveChanges();

                return HandleSuccess(res.Value = "SUCCESS UPDATED !");
            }

            catch (SpecializeNotFoundException e)
            {
                res.Status = SpecializeStatus.SpecializeNotFound;
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
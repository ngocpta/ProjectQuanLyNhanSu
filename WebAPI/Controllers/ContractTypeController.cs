using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ExceptionModel.ContractException;
using WebAPI.Model;
using WebAPI.Model.Constant;
using WebAPI.Model.Contract;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/contracttype")]
    [ApiController]
    public class ContractTypeController : ControllerBase
    {
        private readonly QuanLyNhanSuContext _context;

        public ContractTypeController(QuanLyNhanSuContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("get-all-contract-types")]
        public IActionResult GetAllContractTypes()
        {
            Res res = new Res();
            try
            {
                var type = _context.ContractType;
                if (type == null)
                    throw new ContractTypeNotFoundException();
                var list = new List<ContractTypeRes>();

                foreach (var c in type)
                {
                    var t = new ContractTypeRes();
                    try
                    {
                        t.Id = c.Id;
                        t.Active = c.Active;
                        t.Name = c.Name;
                        t.Note = c.Note;
                        t.Type = c.Type;
                        t.Time = c.Time;
                    }
                    catch (Exception e) { }
                    list.Add(t);
                }

                return HandleSuccess(list);
            }
            catch (ContractTypeNotFoundException e)
            {
                res.Status = ContractStatus.ContractTypeNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("get-contract-type")]
        public IActionResult GetContracttype([FromBody] Req<ContractTypeReq> req)
        {
            Res res = new Res();
            try
            {
                var type = _context.ContractType.FirstOrDefault(p => p.Id == req.value.Id);
                if (type == null)
                    throw new ContractTypeNotFoundException();
                var ty = new ContractTypeRes();
                try
                {
                    ty.Id = type.Id;
                    ty.Active = type.Active;
                    ty.Name = type.Name;
                    ty.Note = type.Note;
                    ty.Time = type.Time;
                    ty.Type = type.Type;
                }
                catch (Exception e) { }

                return HandleSuccess(ty);
            }
            catch (ContractTypeNotFoundException e)
            {
                res.Status = ContractStatus.ContractTypeNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] Req<ContractTypeModelReq> req)
        {
            Res res = new Res();

            try
            {
                ContractType type = new ContractType();
                try
                {
                    var checkCode = _context.ContractType.Any(m => m.Id == req.value.Id);
                    if (checkCode)
                        throw new ContractTypeAlreadyExistException();

                    type.Id = "LHD" + Convert.ToString(DateTime.Today.Day) +
                                    Convert.ToString(DateTime.Today.Month) + Convert.ToString(DateTime.Today.Year) +
                                    req.value.Id;
                    type.Active = req.value.Active;
                    type.Name = req.value.Name;
                    type.Note = req.value.Note;
                    type.Time = req.value.Time;
                    type.Type = req.value.Type;
                    type.CreatedDate = Convert.ToDateTime(DateTime.Today.ToString());

                    _context.ContractType.Add(type);
                    _context.SaveChanges();

                }

                catch (Exception e)
                {
                }

                return HandleSuccess(res.Value = "SUCCESS CREATED DEPARTMENT!");
            }
            catch (ContractTypeAlreadyExistException e)
            {
                res.Status = ContractStatus.ContractTypeAlreadyExist;
                res.Value = e.Message;
            }

            return Ok(res);
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update([FromBody] Req<ContractTypeModelReq> req)
        {
            Res res = new Res();
            var type = _context.ContractType.FirstOrDefault(m => m.Id == req.value.Id);
            if (type == null)
                throw new ContractTypeNotFoundException();
            try
            {
                type.Active = req.value.Active;
                type.Name = req.value.Name;
                type.Note = req.value.Note;
                type.Time = req.value.Time;
                type.Type = req.value.Type;
                type.UpdatedDate = Convert.ToDateTime(DateTime.Today.ToString());

                _context.SaveChanges();

                return HandleSuccess(res);
            }

            catch (ContractTypeNotFoundException e)
            {
                res.Status = ContractStatus.ContractTypeNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult Delete([FromBody] Req<ContractTypeReq> req)
        {
            Res res = new Res();
            var type = _context.ContractType.FirstOrDefault(m => m.Id == req.value.Id);
            if (type == null)
                throw new ContractTypeNotFoundException();
            try
            {
                _context.Remove(type);

                _context.SaveChanges();

                return HandleSuccess(res);
            }

            catch (ContractTypeNotFoundException e)
            {
                res.Status = ContractStatus.ContractTypeNotFound;
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
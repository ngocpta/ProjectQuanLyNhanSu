using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
    [Route("api/contract")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly QuanLyNhanSuContext _context;

        public ContractController(QuanLyNhanSuContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("get-all-contracts")]
        public IActionResult GetAllContractTypes()
        {
            Res res = new Res();
            try
            {
                var contract = _context.Contracts;
                if (contract == null)
                    throw new ContractNotFoundException();
                var list = new List<ContractRes>();

                foreach (var c in contract)
                {
                    var t = new ContractRes();
                    try
                    {
                        t.Id = c.Id;
                        t.Active = c.Active;
                        t.ContractTypeId = c.ContractTypeId;
                        t.ContractTypeName = _context.ContractType.FirstOrDefault(x => x.Id == t.ContractTypeId) == null
                            ? string.Empty
                            : _context.ContractType.FirstOrDefault(x => x.Id == t.ContractTypeId).Name;
                        t.Name = c.Name;
                        t.Time = c.Time;
                        t.Note = c.Note;
                    }
                    catch (Exception e) { }
                    list.Add(t);
                }

                return HandleSuccess(list);
            }
            catch (ContractNotFoundException e)
            {
                res.Status = ContractStatus.ContractNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("get-contract")]
        public IActionResult GetContract([FromBody] Req<ContractReq> req)
        {
            Res res = new Res();
            try
            {
                var contract = _context.Contracts.FirstOrDefault(p => p.Id == req.value.Id);
                if (contract == null)
                    throw new ContractNotFoundException();
                var co = new ContractRes();
                try
                {
                    co.Id = contract.Id;
                    co.Active = contract.Active;
                    co.Name = contract.Name;
                    co.Time = contract.Time;
                    co.Note = contract.Note;
                    co.ContractTypeId = contract.ContractTypeId;
                    co.ContractTypeName= _context.ContractType.FirstOrDefault(x => x.Id == co.ContractTypeId) == null
                        ? string.Empty
                        : _context.ContractType.FirstOrDefault(x => x.Id == co.ContractTypeId).Name;
                }
                catch (Exception e) { }

                return HandleSuccess(co);
            }
            catch (ContractNotFoundException e)
            {
                res.Status = ContractStatus.ContractNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("get-contract-by-contracttype")]
        public IActionResult GetContractByContractType([FromBody] Req<ContractTypeReq> req)
        {
            Res res = new Res();
            try
            {
                var contracts = _context.Contracts.Where(p => p.ContractTypeId == req.value.Id);
                if (contracts == null)
                    throw new ContractNotFoundException();
                var list = new List<ContractRes>();
                foreach (var contract in contracts)
                {
                    var co = new ContractRes();
                    try
                    {
                        co.Id = contract.Id;
                        co.Active = contract.Active;
                        co.Name = contract.Name;
                        co.Time = contract.Time;
                        co.Note = contract.Note;
                        co.ContractTypeId = contract.ContractTypeId;
                        co.ContractTypeName = _context.ContractType.FirstOrDefault(x => x.Id == co.ContractTypeId) == null
                            ? string.Empty
                            : _context.ContractType.FirstOrDefault(x => x.Id == co.ContractTypeId).Name;

                        list.Add(co);
                    }
                    
                    catch (Exception e) { }
                }
                
                return HandleSuccess(list);
            }
            catch (ContractNotFoundException e)
            {
                res.Status = ContractStatus.ContractNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }
        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] Req<ContractModelReq> req)
        {
            Res res = new Res();

            try
            {
                Contracts contract = new Contracts();
                try
                {
                    var checkCode = _context.Contracts.Any(m => m.Id == req.value.Id);
                    if (checkCode)
                        throw new ContractAlreadyExistException();

                    contract.Id = "HD" + Convert.ToString(DateTime.Today.Day) +
                                    Convert.ToString(DateTime.Today.Month) + Convert.ToString(DateTime.Today.Year) +
                                    req.value.Id;
                    contract.Active = req.value.Active;
                    contract.Name = req.value.Name;
                    contract.Note = req.value.Note;
                    contract.Time = req.value.Time;

                    if (_context.ContractType.FirstOrDefault(x => x.Id == req.value.ContractTypeId)==null)
                        throw new ContractTypeNotFoundException();
                    if(_context.ContractType.FirstOrDefault(x=>x.Id==req.value.ContractTypeId).Active==false)
                        throw new ContractTypeDisableException();
                    contract.ContractTypeId = req.value.ContractTypeId;
                    
                    _context.Contracts.Add(contract);
                    _context.SaveChanges();

                }

                catch (Exception e) { }

                return HandleSuccess(res.Value = "SUCCESS CREATED!");
            }
            catch (ContractAlreadyExistException e)
            {
                res.Status = ContractStatus.ContractAlreadyExist;
                res.Value = e.Message;
            }

            return Ok(res);
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update([FromBody] Req<ContractModelReq> req)
        {
            Res res = new Res();
            var contract = _context.Contracts.FirstOrDefault(m => m.Id == req.value.Id);
            if (contract == null)
                throw new ContractNotFoundException();
            try
            {
                contract.Active = req.value.Active;
                contract.Name = req.value.Name;
                contract.Note = req.value.Note;
                contract.Time = req.value.Time;
                contract.ContractTypeId = req.value.ContractTypeId;

                _context.SaveChanges();

                return HandleSuccess(res);
            }

            catch (ContractNotFoundException e)
            {
                res.Status = ContractStatus.ContractNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult Delete([FromBody] Req<ContractReq> req)
        {
            Res res = new Res();
            var type = _context.Contracts.FirstOrDefault(m => m.Id == req.value.Id);
            if (type == null)
                throw new ContractNotFoundException();
            try
            {
                _context.Remove(type);

                _context.SaveChanges();

                return HandleSuccess(res);
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
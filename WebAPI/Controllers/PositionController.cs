using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ExceptionModel.PostionException;
using WebAPI.Model;
using WebAPI.Model.Constant;
using WebAPI.Model.Position;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly QuanLyNhanSuContext _context;

        public PositionController(QuanLyNhanSuContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("get-all-positions")]
        public IActionResult GetAllPositions()
        {
            Res res = new Res();
            try
            {
                var positions = _context.Position;
                if (positions == null)
                    throw new PositionNotFoundException();
                var list = new List<PositionRes>();

                foreach (var d in positions)
                {
                    var de = new PositionRes();
                    try
                    {
                        de.Id = d.Id;
                        de.Active = d.Active;
                        de.Name = d.Name;
                        de.Active = d.Active;
                    }
                    catch (Exception e) { }
                    list.Add(de);
                }

                return HandleSuccess(list);
            }
            catch (PositionNotFoundException e)
            {
                res.Status = PositionStatus.PositionNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("get-position")]
        public IActionResult GetPosition([FromBody] Req<PositionReq> req)
        {
            Res res = new Res();
            try
            {
                var position = _context.Position.FirstOrDefault(p => p.Id == req.value.Id);
                if (position == null)
                    throw new PositionNotFoundException();
                var de = new PositionRes();
                try
                {
                    de.Id = position.Id;
                    de.Name = position.Name;
                    de.Note = position.Note;
                    de.Active = position.Active;
                }
                catch (Exception e) { }

                return HandleSuccess(de);
            }
            catch (PositionNotFoundException e)
            {
                res.Status = PositionStatus.PositionNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] Req<PositionModelReq> req)
        {
            Res res = new Res();

            try
            {
                Position position = new Position();
                try
                {
                    var checkCode = _context.Position.Any(m => m.Id == req.value.Id);
                    if (checkCode)
                        throw new PositionAlreadyExistException();

                    position.Id = "P" + Convert.ToString(DateTime.Today.Day) +
                                    Convert.ToString(DateTime.Today.Month) + Convert.ToString(DateTime.Today.Year) +
                                    req.value.Id;
                    position.Active = req.value.Active;
                    position.Name = req.value.Name;
                    position.Note = req.value.Note;

                    _context.Position.Add(position);
                    _context.SaveChanges();

                }

                catch (Exception e)
                {
                }

                return HandleSuccess(res.Value = "SUCCESS CREATED !");
            }
            catch (PositionAlreadyExistException e)
            {
                res.Status = PositionStatus.PostiontAlreadyExist;
                res.Value = e.Message;
            }

            return Ok(res);
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update([FromBody] Req<PositionModelReq> req)
        {
            Res res = new Res();
            var position = _context.Position.FirstOrDefault(m => m.Id == req.value.Id);
            if (position == null)
                throw new PositionNotFoundException();
            try
            {
                position.Active = req.value.Active;
                position.Name = req.value.Name;
                position.Note = req.value.Note;

                _context.SaveChanges();

                return HandleSuccess(res.Value = "SUCCESS UPDATED !");
            }

            catch (PositionNotFoundException e)
            {
                res.Status = PositionStatus.PositionNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("delete")]
        public IActionResult Delete([FromBody] Req<PositionReq> req)
        {
            Res res = new Res();
            var position = _context.Position.FirstOrDefault(m => m.Id == req.value.Id);
            if (position == null)
                throw new PositionNotFoundException();
            try
            {
                _context.Remove(position);

                _context.SaveChanges();

                return HandleSuccess(res.Value = "SUCCESS DELETED !");
            }

            catch (PositionNotFoundException e)
            {
                res.Status = PositionStatus.PositionNotFound;
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
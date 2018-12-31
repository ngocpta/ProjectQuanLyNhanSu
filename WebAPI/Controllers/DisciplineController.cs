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
    [Route("api/discipline")]
    [ApiController]
    public class DisciplineController : ControllerBase
    {
        private readonly QuanLyNhanSuContext _context;

        public DisciplineController(QuanLyNhanSuContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("get-all-disciplines")]
        public IActionResult GetAllDisciplines()
        {
            Res res = new Res();
            try
            {
                var discipline = _context.Discipline;
                if (discipline == null)
                    throw new DisciplineNotFoundException();
                var list = new List<DisciplineRes>();

                foreach (var r in discipline)
                {
                    var re = new DisciplineRes();
                    try
                    {
                        re.Id = r.Id;
                        re.Amount = r.Amount;
                        re.Desciption = r.Desciption;
                        re.DicisionNo = r.DicisionNo;
                        re.EffectiveDate = r.EffectiveDate.ToString();
                        re.PercentDiscipline = r.PercentDiscipline;
                        re.RewardAndDisciplineMethodId = r.RewardAndDisciplineMethodId;
                        re.RewardAndDisciplineMethodName =
                            _context.RewardAndDisciplineMethod.FirstOrDefault(
                                x => x.Id == r.RewardAndDisciplineMethodId) == null
                                ? string.Empty
                                : _context.RewardAndDisciplineMethod
                                    .FirstOrDefault(x => x.Id == r.RewardAndDisciplineMethodId).Name;
                        re.SignBy = r.SignBy;
                        re.SignDate = r.SignDate.ToString();

                        list.Add(re);
                    }
                    catch (Exception e) { }
                }

                return HandleSuccess(list);
            }
            catch (RewardNotFoundException e)
            {
                res.Status = RewardAndDisciplineStatus.DisciplineNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("get-discipline")]
        public IActionResult GetDiscipline([FromBody] Req<DisciplineReq> req)
        {
            Res res = new Res();
            try
            {
                var r = _context.Discipline.FirstOrDefault(p => p.Id == req.value.Id);
                if (r == null)
                    throw new RewardNotFoundException();
                var re = new DisciplineRes();
                try
                {
                    re.Id = r.Id;
                    re.Amount = r.Amount;
                    re.Desciption = r.Desciption;
                    re.DicisionNo = r.DicisionNo;
                    re.EffectiveDate = r.EffectiveDate.ToString();
                    re.PercentDiscipline = r.PercentDiscipline;
                    re.RewardAndDisciplineMethodId = r.RewardAndDisciplineMethodId;
                    re.RewardAndDisciplineMethodName =
                        _context.RewardAndDisciplineMethod.FirstOrDefault(
                            x => x.Id == r.RewardAndDisciplineMethodId) == null
                            ? string.Empty
                            : _context.RewardAndDisciplineMethod
                                .FirstOrDefault(x => x.Id == r.RewardAndDisciplineMethodId).Name;
                    re.SignBy = r.SignBy;
                    re.SignDate = r.SignDate.ToString();
                }
                catch (Exception e) { }

                return HandleSuccess(re);
            }
            catch (DisciplineNotFoundException e)
            {
                res.Status = RewardAndDisciplineStatus.DisciplineNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("get-discipline-by-method")]
        public IActionResult GetRewardByMethod([FromBody] Req<MethodReq> req)
        {
            Res res = new Res();
            try
            {
                var disciplines = _context.Discipline.Where(p => p.RewardAndDisciplineMethodId == req.value.Id);
                if (disciplines == null)
                    throw new DisciplineNotFoundException();
                var list = new List<DisciplineRes>();
                foreach (var r in disciplines)
                {
                    var re = new DisciplineRes();
                    try
                    {
                        re.Id = r.Id;
                        re.Amount = r.Amount;
                        re.Desciption = r.Desciption;
                        re.DicisionNo = r.DicisionNo;
                        re.EffectiveDate = r.EffectiveDate.ToString();
                        re.PercentDiscipline = r.PercentDiscipline;
                        re.RewardAndDisciplineMethodId = r.RewardAndDisciplineMethodId;
                        re.RewardAndDisciplineMethodName =
                            _context.RewardAndDisciplineMethod.FirstOrDefault(
                                x => x.Id == r.RewardAndDisciplineMethodId) == null
                                ? string.Empty
                                : _context.RewardAndDisciplineMethod
                                    .FirstOrDefault(x => x.Id == r.RewardAndDisciplineMethodId).Name;
                        re.SignBy = r.SignBy;
                        re.SignDate = r.SignDate.ToString();

                        list.Add(re);
                    }
                    catch (Exception e) { }
                }


                return HandleSuccess(list);
            }
            catch (DisciplineNotFoundException e)
            {
                res.Status = RewardAndDisciplineStatus.DisciplineNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] Req<DisciplineModelReq> req)
        {
            Res res = new Res();

            try
            {
                Discipline discipline = new Discipline();
                try
                {
                    var checkCode = _context.Discipline.Any(m => m.Id == req.value.Id);
                    if (checkCode)
                        throw new DisciplinereadyExistException();

                    discipline.Id = "KL" + Convert.ToString(DateTime.Today.Day) +
                                    Convert.ToString(DateTime.Today.Month) + Convert.ToString(DateTime.Today.Year) +
                                    req.value.Id;
                    discipline.Amount = req.value.Amount;
                    discipline.Desciption = req.value.Desciption;
                    discipline.DicisionNo = req.value.DicisionNo;
                    discipline.EffectiveDate = Convert.ToDateTime(req.value.EffectiveDate);
                    discipline.PercentDiscipline = req.value.PercentDiscipline;
                    if (_context.RewardAndDisciplineMethod.FirstOrDefault(x => x.Id == req.value.RewardAndDisciplineMethodId) == null)
                        throw new MethodNotFoundException();
                    discipline.RewardAndDisciplineMethodId = req.value.RewardAndDisciplineMethodId;
                    discipline.SignBy = req.value.SignBy;
                    discipline.SignDate = Convert.ToDateTime(req.value.SignDate);
                    discipline.CreatedDate = DateTime.Today;

                    _context.Discipline.Add(discipline);
                    _context.SaveChanges();

                }

                catch (Exception e)
                {
                }

                return HandleSuccess(res.Value = "SUCCESS CREATED !");
            }
            catch (DisciplinereadyExistException e)
            {
                res.Status = RewardAndDisciplineStatus.DisciplineAlreadyExist;
                res.Value = e.Message;
            }
            catch (MethodNotFoundException e)
            {
                res.Status = RewardAndDisciplineStatus.RewardAndDisciplineMethodNotFound;
                res.Value = e.Message;
            }

            return Ok(res);
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update([FromBody] Req<DisciplineModelReq> req)
        {
            Res res = new Res();
            var discipline = _context.Discipline.FirstOrDefault(m => m.Id == req.value.Id);
            if (discipline == null)
                throw new RewardNotFoundException();
            try
            {
                discipline.Desciption = req.value.Desciption;
                discipline.DicisionNo = req.value.DicisionNo;
                discipline.EffectiveDate = Convert.ToDateTime(req.value.EffectiveDate);
                discipline.PercentDiscipline = req.value.PercentDiscipline;
                if (_context.RewardAndDisciplineMethod.FirstOrDefault(x => x.Id == req.value.RewardAndDisciplineMethodId) == null)
                    throw new MethodNotFoundException();
                discipline.RewardAndDisciplineMethodId = req.value.RewardAndDisciplineMethodId;
                discipline.Amount = req.value.Amount;
                discipline.Title = req.value.Title;
                discipline.SignBy = req.value.SignBy;
                discipline.SignDate = Convert.ToDateTime(req.value.SignDate);
                discipline.UpdatedDate = Convert.ToDateTime(DateTime.Today);

                _context.SaveChanges();

                return HandleSuccess(res);
            }

            catch (DisciplineNotFoundException e)
            {
                res.Status = RewardAndDisciplineStatus.DisciplineNotFound;
                res.Value = e.Message;
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
        public IActionResult Delete([FromBody] Req<DisciplineReq> req)
        {
            Res res = new Res();
            var discipline = _context.Discipline.FirstOrDefault(m => m.Id == req.value.Id);
            if (discipline == null)
                throw new RewardNotFoundException();
            try
            {
                _context.Remove(discipline);

                _context.SaveChanges();

                return HandleSuccess(res);
            }

            catch (RewardNotFoundException e)
            {
                res.Status = RewardAndDisciplineStatus.DisciplineNotFound;
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
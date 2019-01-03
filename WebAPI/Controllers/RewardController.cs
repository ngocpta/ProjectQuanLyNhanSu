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
    [Route("api/reward")]
    [ApiController]
    public class RewardController : ControllerBase
    {
        private readonly QuanLyNhanSuContext _context;

        public RewardController(QuanLyNhanSuContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("get-all-rewards")]
        public IActionResult GetAllRewards()
        {
            Res res = new Res();
            try
            {
                var reward = _context.Reward;
                if (reward == null)
                    throw new RewardNotFoundException();
                var list = new List<RewardRes>();

                foreach (var r in reward)
                {
                    var re = new RewardRes();
                    try
                    {
                        re.Id = r.Id;
                        re.Amount = r.Amount;
                        re.Desciption = r.Desciption;
                        re.DicisionNo = r.DicisionNo;
                        re.EffectiveDate = r.EffectiveDate.ToString();
                        re.PercentReward = r.PercentReward;
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
                res.Status = RewardAndDisciplineStatus.RewardNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("get-reward")]
        public IActionResult GetReward([FromBody] Req<RewardReq> req)
        {
            Res res = new Res();
            try
            {
                var r = _context.Reward.FirstOrDefault(p => p.Id == req.value.Id);
                if (r == null)
                    throw new RewardNotFoundException();
                var re = new RewardRes();
                try
                {
                    re.Id = r.Id;
                    re.Amount = r.Amount;
                    re.Desciption = r.Desciption;
                    re.DicisionNo = r.DicisionNo;
                    re.EffectiveDate = r.EffectiveDate.ToString();
                    re.PercentReward = r.PercentReward;
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
            catch (RewardNotFoundException e)
            {
                res.Status = RewardAndDisciplineStatus.RewardNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("get-reward-by-method")]
        public IActionResult GetRewardByMethod([FromBody] Req<MethodReq> req)
        {
            Res res = new Res();
            try
            {
                var reward = _context.Reward.Where(p => p.RewardAndDisciplineMethodId == req.value.Id);
                if (reward == null)
                    throw new RewardNotFoundException();
                var list = new List<RewardRes>();
                foreach (var r in reward)
                {
                    var re=new RewardRes();
                    try
                    {
                        re.Id = r.Id;
                        re.Amount = r.Amount;
                        re.Desciption = r.Desciption;
                        re.DicisionNo = r.DicisionNo;
                        re.EffectiveDate = r.EffectiveDate.ToString();
                        re.PercentReward = r.PercentReward;
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
                res.Status = RewardAndDisciplineStatus.RewardNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] Req<RewardModelReq> req)
        {
            Res res = new Res();

            try
            {
                Reward reward = new Reward();
                try
                {
                    var checkCode = _context.Reward.Any(m => m.Id == req.value.Id);
                    if (checkCode)
                        throw new RewardAlreadyExistException();

                    reward.Id = "KT" + Convert.ToString(DateTime.Today.Day) +
                                    Convert.ToString(DateTime.Today.Month) + Convert.ToString(DateTime.Today.Year) +
                                    req.value.Id;
                    reward.Amount = req.value.Amount;
                    reward.Desciption = req.value.Desciption;
                    reward.DicisionNo = req.value.DicisionNo;
                    
                    reward.EffectiveDate = DateTime.Today;
                    reward.PercentReward = req.value.PercentReward;
                    if (_context.RewardAndDisciplineMethod.FirstOrDefault(x => x.Id == req.value.RewardAndDisciplineMethodId) == null)
                        throw new MethodNotFoundException();
                    reward.RewardAndDisciplineMethodId = req.value.RewardAndDisciplineMethodId;
                    reward.SignBy = req.value.SignBy;
                    reward.SignDate = DateTime.Today;
                    reward.CreatedDate = DateTime.Today;

                    _context.Reward.Add(reward);
                    _context.SaveChanges();

                }

                catch (Exception e)
                {
                }

                return HandleSuccess(res.Value = "SUCCESS CREATED !");
            }
            catch (RewardAlreadyExistException e)
            {
                res.Status = RewardAndDisciplineStatus.RewardAlreadyExist;
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
        public IActionResult Update([FromBody] Req<RewardModelReq> req)
        {
            Res res = new Res();
            var reward = _context.Reward.FirstOrDefault(m => m.Id == req.value.Id);
            if (reward == null)
                throw new RewardNotFoundException();
            try
            {
                reward.Amount = req.value.Amount;
                reward.Desciption = req.value.Desciption;
                reward.DicisionNo = req.value.DicisionNo;

                reward.EffectiveDate = DateTime.Today;
                reward.PercentReward = req.value.PercentReward;
                if (_context.RewardAndDisciplineMethod.FirstOrDefault(x => x.Id == req.value.RewardAndDisciplineMethodId) == null)
                    throw new MethodNotFoundException();
                reward.RewardAndDisciplineMethodId = req.value.RewardAndDisciplineMethodId;
                reward.SignBy = req.value.SignBy;
                reward.SignDate = DateTime.Today;
                reward.CreatedDate = DateTime.Today;

                _context.SaveChanges();

                return HandleSuccess(res);
            }

            catch (RewardNotFoundException e)
            {
                res.Status = RewardAndDisciplineStatus.RewardNotFound;
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
        public IActionResult Delete([FromBody] Req<RewardReq> req)
        {
            Res res = new Res();
            var reward = _context.Reward.FirstOrDefault(m => m.Id == req.value.Id);
            if (reward == null)
                throw new RewardNotFoundException();
            try
            {
                _context.Remove(reward);

                _context.SaveChanges();

                return HandleSuccess(res);
            }

            catch (RewardNotFoundException e)
            {
                res.Status = RewardAndDisciplineStatus.RewardNotFound;
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
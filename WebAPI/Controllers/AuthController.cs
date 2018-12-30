using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ExceptionModel.MemberException;
using WebAPI.Model;
using WebAPI.Model.Constant;
using WebAPI.Model.Members;
using WebAPI.Security;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly QuanLyNhanSuContext _context;

        public AuthController(QuanLyNhanSuContext context)
        {
            _context = context;
        }

        [AllowAnonymous, Route("login")]
        public IActionResult Login([FromBody] Req<ProvinceRes> membeReq)
        {
            Res res = new Res();
            try
            {
                Member member = _context.Member.FirstOrDefault(m => m.Username == membeReq.value.Username);
                if (member == null) throw new MemberNotFoundException();
                if (!PasswordUtility.VerifyHashedPassword(member.Password, membeReq.value.Password))
                    throw new PasswordNotMatchException();
                member.LastLogin = Convert.ToDateTime(DateTime.Today.Date);
                _context.SaveChanges();
                
                
                return Ok(new Res
                {
                    Status = "SUCCESS",
                    Value = new
                    {
                        Token = TokenProvider.TokenGenerate(member),
                        User = new MemberLoginRes()
                        {
                            Username = member.Username,
                            Name = member.Fullname,
                            Role = member.Type.ToString()
                        }
                    }
                });
            }
            catch (MemberNotFoundException e)
            {
                res.Status = MemberStatus.MemberNotFound;
                res.Value = e.Message;
            }
            catch (PasswordNotMatchException e)
            {
                res.Status = MemberStatus.PasswordNotMatch;
                res.Value = e.Message;
            }
            return Ok(res);
        }
    }
}
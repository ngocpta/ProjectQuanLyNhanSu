using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Constant.ProfileConst;
using WebAPI.ExceptionModel.MemberException;
using WebAPI.Model;
using WebAPI.Model.Constant;
using WebAPI.Model.Members;
using WebAPI.Models;
using WebAPI.Security;

namespace WebAPI.Controllers
{
    [Route("api/member")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly QuanLyNhanSuContext _context;
        private string token = string.Empty;

        public MemberController(QuanLyNhanSuContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("get-all-members")]
        public IActionResult GetAllMember()
        {
            Res res=new Res();

            try
            {
                if (!Request.Headers.ContainsKey("Authorization"))
                    throw new MemberNotLoginException();
                if (!Request.Headers.TryGetValue("Authorization", out var authorization))
                    throw new MemberNotTokenException();
                token = authorization.FirstOrDefault().Substring("Bearer ".Length);
                var tokenIsValid = TokenProvider.TokenValidate(token, out var data);
                if (!tokenIsValid)
                    throw new MemberIncorrectTokenException();
                if (data.Role != MemberType.Admin.ToString())
                    throw new MemberUnauthorizeException();
                var members = _context.Member.Where(m => m.CreatedBy == data.Sub);
                if (members == null)
                    throw new MemberNotFoundException();
                var list = new List<MemberRes>();
                foreach (var m in members)
                {
                    var member = new MemberRes();
                    try
                    {
                        member.Username = m.Username;
                        member.Active = m.Active;
                        member.CreatedBy = m.CreatedBy;
                        member.CreatedDate = m.CreatedDate?.ToString("dd-MM-yyyy");
                        member.LastLogin = m.LastLogin?.ToString("dd-MM-yyyy");
                        member.Email = m.Email;
                        member.Fullname = m.Fullname;
                        member.Note = m.Note;
                        member.Phone = m.Phone;
                        member.Type = m.Type;
                        member.UpdatedBy = m.UpdatedBy;
                        member.UpdatedDate = m.UpdatedDate?.ToString("dd-MM-yyyy");
                        member.LastChangePass = m.LastChangePass?.ToString("dd-MM-yyyy");
                    }
                    catch (Exception e)
                    {
                    }

                    list.Add(member);
                }

                return HandleSuccess(list);
            }
            catch (MemberNotFoundException e)
            {
                res.Status = MemberStatus.MemberNotFound;
                res.Value = e.Message;
            }
            catch (MemberUnauthorizeException e)
            {
                res.Status = MemberStatus.MemberUnauthorize;
                res.Value = e.Message;
            }
            catch (MemberIncorrectTokenException e)
            {
                res.Status = MemberStatus.IncorrectToken;
                res.Value = e.Message;
            }
            catch (MemberNotTokenException e)
            {
                res.Status = MemberStatus.HaveNotToken;
                res.Value = e.Message;
            }
            catch (MemberNotLoginException e)
            {
                res.Status = MemberStatus.NotLogin;
                res.Value = e.Message;
            }

            return Ok(res);
        }

        [HttpPost]
        [Route("get-member-detail")]
        public IActionResult GetMemberDetail([FromBody] Req<MemberUsername> memReq)
        {
            Res res = new Res();

            try
            {
                if (!Request.Headers.ContainsKey("Authorization"))
                    throw new MemberNotLoginException();
                if (!Request.Headers.TryGetValue("Authorization", out var authorization))
                    throw new MemberNotTokenException();
                token = authorization.FirstOrDefault().Substring("Bearer ".Length);
                var tokenIsValid = TokenProvider.TokenValidate(token, out var data);
                if (!tokenIsValid)
                    throw new MemberIncorrectTokenException();
                if (data.Role == MemberType.Admin.ToString())
                {
                    var users = _context.Member.Where(m => m.CreatedBy == data.Sub);
                    var user = new MemberRes();
                    foreach (var m in users)
                    {
                        if (m.Username != memReq.value.Username)
                            throw new MemberNotFoundException();
                        try
                        {
                            user.Username = m.Username;
                            user.Active = m.Active;
                            user.CreatedBy = m.CreatedBy;
                            user.CreatedDate = m.CreatedDate?.ToString("dd-MM-yyyy");
                            user.LastLogin = m.LastLogin?.ToString("dd-MM-yyyy");
                            user.Email = m.Email;
                            user.Fullname = m.Fullname;
                            user.Note = m.Note;
                            user.Phone = m.Phone;
                            user.Type = m.Type;
                            user.UpdatedBy = m.UpdatedBy;
                            user.UpdatedDate = m.UpdatedDate?.ToString("dd-MM-yyyy");
                            user.LastChangePass = m.LastChangePass?.ToString("dd-MM-yyyy");
                        }
                        catch (Exception e)
                        {
                        }

                        return HandleSuccess(m);
                    }
                }
                else
                {
                    var member = _context.Member.FirstOrDefault(m => m.Username == memReq.value.Username);
                    if (member == null)
                        throw new MemberNotFoundException();
                    var memberReq = new MemberRes();
                    try
                    {
                        memberReq.Username = member.Username;
                        memberReq.Active = member.Active;
                        memberReq.CreatedBy = member.CreatedBy;
                        memberReq.CreatedDate = member.CreatedDate?.ToString("dd-MM-yyyy");
                        memberReq.LastLogin = member.LastLogin?.ToString("dd-MM-yyyy");
                        memberReq.Email = member.Email;
                        memberReq.Fullname = member.Fullname;
                        memberReq.Note = member.Note;
                        memberReq.Phone = member.Phone;
                        memberReq.Type = member.Type;
                        memberReq.UpdatedBy = member.UpdatedBy;
                        memberReq.UpdatedDate = member.UpdatedDate?.ToString("dd-MM-yyyy");
                        memberReq.LastChangePass = member.LastChangePass?.ToString("dd-MM-yyyy");
                    }
                    catch (Exception e){ }

                    return HandleSuccess(memberReq);
                }
            }
            catch (MemberIncorrectTokenException e)
            {
                res.Status = MemberStatus.IncorrectToken;
                res.Value = e.Message;
            }
            catch (MemberNotTokenException e)
            {
                res.Status = MemberStatus.HaveNotToken;
                res.Value = e.Message;
            }
            catch (MemberNotLoginException e)
            {
                res.Status = MemberStatus.NotLogin;
                res.Value = e.Message;
            }
            catch (MemberNotFoundException e)
            {
                res.Status = MemberStatus.MemberNotFound;
                res.Value = e.Message;
            }

            return Ok(res);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] Req<MemberReq> req)
        {
            Res res = new Res();
            try
            {
                if (!Request.Headers.ContainsKey("Authorization"))
                    throw new MemberNotLoginException();
                if (!Request.Headers.TryGetValue("Authorization", out var authorization))
                    throw new MemberNotTokenException();
                token = authorization.FirstOrDefault().Substring("Bearer ".Length);
                var tokenIsValid = TokenProvider.TokenValidate(token, out var data);
                if (!tokenIsValid)
                    throw new MemberIncorrectTokenException();
                if (data.Role != MemberType.Admin.ToString())
                    throw new MemberUnauthorizeException(); 

                Member member = new Member();

                try
                {
                    var checkUserName = _context.Member.Any(m => m.Username == req.value.Username);
                    if (checkUserName)
                        throw new MemberAlreadyExistException();
                    member.Username = req.value.Username;
                    if (req.value.Password != req.value.ConfirmPassword || !CheckPassword.TestPassword(req.value.Password))
                        throw new PasswordNotMatchException();
                    member.Password = PasswordUtility.HashPassword(req.value.Password);
                    member.Phone = req.value.Phone;
                    member.Type = req.value.Type;
                    member.Active = true;
                    member.Email = req.value.Email;
                    member.Fullname = req.value.Fullname;
                    member.Note = req.value.Note;
                    member.CreatedDate = Convert.ToDateTime(DateTime.Today.ToShortDateString());
                    member.CreatedBy = data.Sub;
                    member.FailLogin = 0;

                    _context.Member.Add(member);
                    _context.SaveChanges();
                }
                
                catch (Exception e)
                {
                }

                return HandleSuccess(res.Value="SUCCESS CREATED MEMBER!");
            }
            catch (MemberIncorrectTokenException e)
            {
                res.Status = MemberStatus.IncorrectToken;
                res.Value = e.Message;
            }
            catch (MemberNotTokenException e)
            {
                res.Status = MemberStatus.HaveNotToken;
                res.Value = e.Message;
            }
            catch (MemberNotLoginException e)
            {
                res.Status = MemberStatus.NotLogin;
                res.Value = e.Message;
            }
            catch (MemberUnauthorizeException e)
            {
                res.Status = MemberStatus.MemberNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update([FromBody] Req<UpdateMemberReq> req)
        {
            Res res = new Res();
            try
            {
                if (!Request.Headers.ContainsKey("Authorization"))
                    throw new MemberNotLoginException();
                if (!Request.Headers.TryGetValue("Authorization", out var authorization))
                    throw new MemberNotTokenException();
                token = authorization.FirstOrDefault().Substring("Bearer ".Length);
                var tokenIsValid = TokenProvider.TokenValidate(token, out var data);
                if (!tokenIsValid)
                    throw new MemberIncorrectTokenException();
                if (data.Role == MemberType.Admin.ToString())
                {
                    var member = _context.Member.FirstOrDefault(m => m.Username == req.value.Username);
                    if (member == null)
                        throw new MemberNotFoundException();
                    try
                    {
                        member.Fullname = req.value.Fullname;
                        member.Email = req.value.Email;
                        member.Note = req.value.Note;
                        member.Phone = req.value.PhoneNumber;
                        member.UpdatedBy = data.Sub;
                        member.UpdatedDate = Convert.ToDateTime(DateTime.Today.ToShortDateString());
                        member.Active = req.value.Active;

                        _context.SaveChanges();
                    }
                    catch (Exception e){ }

                    return HandleSuccess(res.Value="SUCCESS UPDATED MEMBER!");
                }
            }
            catch (MemberIncorrectTokenException e)
            {
                res.Status = MemberStatus.IncorrectToken;
                res.Value = e.Message;
            }
            catch (MemberNotTokenException e)
            {
                res.Status = MemberStatus.HaveNotToken;
                res.Value = e.Message;
            }
            catch (MemberNotLoginException e)
            {
                res.Status = MemberStatus.NotLogin;
                res.Value = e.Message;
            }
            catch (PasswordNotMatchException e)
            {
                res.Status = MemberStatus.PasswordNotMatch;
                res.Value = e.Message;
            }
            catch (MemberNotFoundException e)
            {
                res.Status = MemberStatus.MemberNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("deactive")]
        public IActionResult Deactive([FromBody] Req<MemberUsername> req)
        {
            Res res = new Res();
            try
            {
                if (!Request.Headers.ContainsKey("Authorization"))
                    throw new MemberNotLoginException();
                if (!Request.Headers.TryGetValue("Authorization", out var authorization))
                    throw new MemberNotTokenException();
                token = authorization.FirstOrDefault().Substring("Bearer ".Length);
                var tokenIsValid = TokenProvider.TokenValidate(token, out var data);
                if (!tokenIsValid)
                    throw new MemberIncorrectTokenException();
                if (data.Role == MemberType.Admin.ToString())
                {
                    var member = _context.Member.FirstOrDefault(m => m.Username == req.value.Username);
                    if (member == null)
                        throw new MemberNotFoundException();
                    member.Active = false;
                    
                    _context.SaveChanges();

                    return HandleSuccess(res.Value="SUCCESS DEACTIVED MEMBER!");
                }
            }
            catch (MemberIncorrectTokenException e)
            {
                res.Status = MemberStatus.IncorrectToken;
                res.Value = e.Message;
            }
            catch (MemberNotTokenException e)
            {
                res.Status = MemberStatus.HaveNotToken;
                res.Value = e.Message;
            }
            catch (MemberNotLoginException e)
            {
                res.Status = MemberStatus.NotLogin;
                res.Value = e.Message;
            }
            catch (MemberNotFoundException e)
            {
                res.Status = MemberStatus.MemberNotFound;
                res.Value = e.Message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("change-password")]
        public IActionResult ChangePassword([FromBody] Req<PasswordReq> req)
        {
            Res res = new Res();
            try
            {
                if (!Request.Headers.ContainsKey("Authorization"))
                    throw new MemberNotLoginException();
                if (!Request.Headers.TryGetValue("Authorization", out var authorization))
                    throw new MemberNotTokenException();
                token = authorization.FirstOrDefault().Substring("Bearer ".Length);
                var tokenIsValid = TokenProvider.TokenValidate(token, out var data);
                if (!tokenIsValid)
                    throw new MemberIncorrectTokenException();
                if (data.Sub!=req.value.Username)
                    throw new MemberDisabledException();
                var member = _context.Member.FirstOrDefault(m => m.Username == data.Sub);
                if (!PasswordUtility.VerifyHashedPassword(member.Password,req.value.Password))
                    throw new PasswordNotMatchException();
                if (!CheckPassword.TestPassword(req.value.NewPassword))
                    throw new PasswordWrongFormatException();
                member.Password = PasswordUtility.HashPassword(req.value.NewPassword);
                member.LastChangePass = Convert.ToDateTime(DateTime.Today.ToShortDateString());
                
                _context.SaveChanges();

                return HandleSuccess(res.Value = "SUCCESS CHANGED PASS");
            }
            catch (MemberIncorrectTokenException e)
            {
                res.Status = MemberStatus.IncorrectToken;
                res.Value = e.Message;
            }
            catch (MemberNotTokenException e)
            {
                res.Status = MemberStatus.HaveNotToken;
                res.Value = e.Message;
            }
            catch (MemberNotLoginException e)
            {
                res.Status = MemberStatus.NotLogin;
                res.Value = e.Message;
            }
            catch (PasswordNotMatchException e)
            {
                res.Status = MemberStatus.PasswordNotMatch;
                res.Value = e.Message;
            }
            catch (PasswordWrongFormatException e)
            {
                res.Status = MemberStatus.PasswordWrongFormat;
                res.Value = e.Message;
            }

            return Ok(res);
        }

        private IActionResult HandleSuccess(object data)
        {
            return Ok(new Res(data));
        }
        private IActionResult HandleError(string status, string err)
        {
            return Ok(new Res
            {
                Status = status,
                Value = err
            });
        }
    }
}
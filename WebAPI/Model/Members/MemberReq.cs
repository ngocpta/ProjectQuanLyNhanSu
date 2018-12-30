using System;

namespace WebAPI.Model.Members
{
  public class MemberReq
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Note { get; set; }
        public int? Type { get; set; }
    }
}
using System;

namespace WebAPI.Model.Members
{
  public class UpdateMemberReq
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Note { get; set; }
        public bool Active { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace WebAPI.Model.Members
{  
  public class MemberRes
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Note { get; set; }
        public string LastLogin { get; set; }
        public string LastChangePass { get; set; }
        public int? Type { get; set; }
        public bool? Active { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
    }
}
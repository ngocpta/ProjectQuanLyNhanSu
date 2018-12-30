using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Relatives
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public string Fullname { get; set; }
        public DateTime? Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public string Career { get; set; }
        public int? WardId { get; set; }
        public int? Relationship { get; set; }

        public Employee Employee { get; set; }
    }
}

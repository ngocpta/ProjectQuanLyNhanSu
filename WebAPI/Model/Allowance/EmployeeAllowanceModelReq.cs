using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Model.Allowance
{
    public class EmployeeAllowanceModelReq
    {
        public string Id { get; set; }

        public string EmployeeId { get; set; }

        public string AllowanceId { get; set; }

        public string SigningDate { get; set; }

        public string Note { get; set; }
    }
}

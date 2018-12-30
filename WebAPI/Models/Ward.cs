using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Ward
    {
        public Ward()
        {
            Employee = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? DistrictId { get; set; }

        public District District { get; set; }
        public ICollection<Employee> Employee { get; set; }
    }
}

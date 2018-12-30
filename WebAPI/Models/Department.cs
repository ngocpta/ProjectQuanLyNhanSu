using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public partial class Department
    {
        public Department()
        {
            Employee = new HashSet<Employee>();
        }
        
        public string Id { get;  set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }

        public ICollection<Employee> Employee { get; set; }
    }
}

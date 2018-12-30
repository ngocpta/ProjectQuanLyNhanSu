using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Specialize
    {
        public Specialize()
        {
            Employee = new HashSet<Employee>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }

        public ICollection<Employee> Employee { get; set; }
    }
}

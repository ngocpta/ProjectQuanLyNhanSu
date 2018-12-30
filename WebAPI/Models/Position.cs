using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Position
    {
        public Position()
        {
            Employee = new HashSet<Employee>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public bool? Active { get; set; }

        public ICollection<Employee> Employee { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class EmployeeDiscipline
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public string DisciplineId { get; set; }
        public DateTime? Date { get; set; }
        public string Note { get; set; }

        public Discipline Discipline { get; set; }
        public Employee Employee { get; set; }
    }
}

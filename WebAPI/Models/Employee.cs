using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeContract = new HashSet<EmployeeContract>();
            EmployeeDiscipline = new HashSet<EmployeeDiscipline>();
            EmployeeInsurrance = new HashSet<EmployeeInsurrance>();
            EmployeeReward = new HashSet<EmployeeReward>();
            EmployeeSalaryLevel = new HashSet<EmployeeSalaryLevel>();
            Relatives = new HashSet<Relatives>();
            TimeKeeping = new HashSet<TimeKeeping>();
            WorkProcess = new HashSet<WorkProcess>();
        }

        public string Id { get; set; }
        public string Fullname { get; set; }
        public DateTime? Birthday { get; set; }
        public int? Gender { get; set; }
        public string CurrentAddress { get; set; }
        public int? WardId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? EducationLevel { get; set; }
        public string SpecializeId { get; set; }
        public int? LanguageLevel { get; set; }
        public string DepartmentId { get; set; }
        public string PositionId { get; set; }
        public DateTime? PayrollDay { get; set; }
        public DateTime? DayInCompany { get; set; }
        public bool? Active { get; set; }

        public Department Department { get; set; }
        public Position Position { get; set; }
        public Specialize Specialize { get; set; }
        public Ward Ward { get; set; }
        public ICollection<EmployeeContract> EmployeeContract { get; set; }
        public ICollection<EmployeeDiscipline> EmployeeDiscipline { get; set; }
        public ICollection<EmployeeInsurrance> EmployeeInsurrance { get; set; }
        public ICollection<EmployeeReward> EmployeeReward { get; set; }
        public ICollection<EmployeeSalaryLevel> EmployeeSalaryLevel { get; set; }
        public ICollection<Relatives> Relatives { get; set; }
        public ICollection<TimeKeeping> TimeKeeping { get; set; }
        public ICollection<WorkProcess> WorkProcess { get; set; }
    }
}

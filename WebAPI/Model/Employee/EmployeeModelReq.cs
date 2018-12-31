using System.Collections.Generic;

namespace WebAPI.Model.Employee
{
    public class EmployeeModelReq
    {
        public string Id { get; set; }
        public string Fullname { get; set; }
        public string Birthday { get; set; }
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
        public string PayrollDay { get; set; }
        public string DayInCompany { get; set; }
        public bool? Active { get; set; }
    }
}
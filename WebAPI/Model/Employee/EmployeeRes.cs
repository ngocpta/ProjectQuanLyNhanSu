﻿using System.Collections.Generic;

namespace WebAPI.Model.Employee
{
    public class EmployeeRes
    {
        public string Id { get; set; }
        public string Fullname { get; set; }
        public string Birthday { get; set; }
        public int? Gender { get; set; }
        public string CurrentAddress { get; set; }
        public int? WardId { get; set; }
        public string WardName { get; set; }
        public string DistricName { get; set; }
        public string ProvinceName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? EducationLevel { get; set; }
        public string SpecializeId { get; set; }
        public string SpecializeName { get; set; }
        public int? LanguageLevel { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string PositionId { get; set; }
        public string PositionName { get; set; }
        public string PayrollDay { get; set; }
        public string DayInCompany { get; set; }
        public bool? Active { get; set; }

        public List<EmployeeRalativesRes> EmployeeRalatives { get; set; }
        public List<EmployeeWorkProcessRes> EmployeeWorkProcesses { get; set; }
    }
}
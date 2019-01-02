using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeAllowance = new HashSet<EmployeeAllowance>();
            EmployeeContract = new HashSet<EmployeeContract>();
            EmployeeDiscipline = new HashSet<EmployeeDiscipline>();
            EmployeeInsurrance = new HashSet<EmployeeInsurrance>();
            EmployeeReward = new HashSet<EmployeeReward>();
        }

        [Column("ID")]
        [StringLength(50)]
        public string Id { get; set; }
        [StringLength(100)]
        public string Fullname { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Birthday { get; set; }
        public int? Gender { get; set; }
        [StringLength(50)]
        public string CurrentAddress { get; set; }
        [Column("WardID")]
        public int? WardId { get; set; }
        [StringLength(50)]
        public string Phone { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        public int? EducationLevel { get; set; }
        [Column("SpecializeID")]
        [StringLength(50)]
        public string SpecializeId { get; set; }
        public int? LanguageLevel { get; set; }
        [Column("DepartmentID")]
        [StringLength(50)]
        public string DepartmentId { get; set; }
        [Column("PositionID")]
        [StringLength(50)]
        public string PositionId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PayrollDay { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DayInCompany { get; set; }
        public bool? Active { get; set; }

        [ForeignKey("DepartmentId")]
        [InverseProperty("Employee")]
        public virtual Department Department { get; set; }
        [ForeignKey("PositionId")]
        [InverseProperty("Employee")]
        public virtual Position Position { get; set; }
        [ForeignKey("SpecializeId")]
        [InverseProperty("Employee")]
        public virtual Specialize Specialize { get; set; }
        [ForeignKey("WardId")]
        [InverseProperty("Employee")]
        public virtual Ward Ward { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<EmployeeAllowance> EmployeeAllowance { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<EmployeeContract> EmployeeContract { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<EmployeeDiscipline> EmployeeDiscipline { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<EmployeeInsurrance> EmployeeInsurrance { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<EmployeeReward> EmployeeReward { get; set; }
    }
}

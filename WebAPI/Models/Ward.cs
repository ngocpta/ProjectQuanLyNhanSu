using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public partial class Ward
    {
        public Ward()
        {
            Employee = new HashSet<Employee>();
        }

        [Column("ID")]
        public int Id { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [Column("DistrictID")]
        public int? DistrictId { get; set; }

        [ForeignKey("DistrictId")]
        [InverseProperty("Ward")]
        public virtual District District { get; set; }
        [InverseProperty("Ward")]
        public virtual ICollection<Employee> Employee { get; set; }
    }
}

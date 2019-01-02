using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public partial class Specialize
    {
        public Specialize()
        {
            Employee = new HashSet<Employee>();
        }

        [Column("ID")]
        [StringLength(50)]
        public string Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        public bool? Active { get; set; }

        [InverseProperty("Specialize")]
        public virtual ICollection<Employee> Employee { get; set; }
    }
}

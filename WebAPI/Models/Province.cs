using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public partial class Province
    {
        public Province()
        {
            District = new HashSet<District>();
        }

        [Column("ID")]
        public int Id { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("Province")]
        public virtual ICollection<District> District { get; set; }
    }
}

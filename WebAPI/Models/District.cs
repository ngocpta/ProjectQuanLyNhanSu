using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public partial class District
    {
        public District()
        {
            Ward = new HashSet<Ward>();
        }

        [Column("ID")]
        public int Id { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [Column("ProvinceID")]
        public int? ProvinceId { get; set; }

        [ForeignKey("ProvinceId")]
        [InverseProperty("District")]
        public virtual Province Province { get; set; }
        [InverseProperty("District")]
        public virtual ICollection<Ward> Ward { get; set; }
    }
}

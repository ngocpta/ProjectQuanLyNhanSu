using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public partial class Member
    {
        [StringLength(50)]
        public string Username { get; set; }
        [StringLength(100)]
        public string Password { get; set; }
        [StringLength(500)]
        public string Fullname { get; set; }
        [StringLength(500)]
        public string Email { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        public string Note { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastLogin { get; set; }
        public int? FailLogin { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastChangePass { get; set; }
        public int? Type { get; set; }
        public bool? Active { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
    }
}

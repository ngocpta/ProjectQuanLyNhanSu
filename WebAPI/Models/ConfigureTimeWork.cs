using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public partial class ConfigureTimeWork
    {
        [Column("ID")]
        public int Id { get; set; }
        public TimeSpan TimeIn1 { get; set; }
        public TimeSpan TimeOut1 { get; set; }
        public TimeSpan TimeIn2 { get; set; }
        public TimeSpan TimeOut2 { get; set; }
        public TimeSpan TimeIn3 { get; set; }
        public TimeSpan TimeOut3 { get; set; }
    }
}

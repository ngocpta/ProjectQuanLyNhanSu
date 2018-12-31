using System;

namespace WebAPI.Models
{
    public partial class ConfigureTimeWork
    {
        public int Id { get; set; }
        public TimeSpan TimeIn1 { get; set; }
        public TimeSpan TimeIn2 { get; set; }
        public TimeSpan TimeIn3 { get; set; }
        public TimeSpan TimeOut1 { get; set; }
        public TimeSpan TimeOut2 { get; set; }
        public TimeSpan TimeOut3 { get; set; }
    }
}
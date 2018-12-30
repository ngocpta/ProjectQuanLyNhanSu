using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class District
    {
        public District()
        {
            Ward = new HashSet<Ward>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? ProvinceId { get; set; }

        public Province Province { get; set; }
        public ICollection<Ward> Ward { get; set; }
    }
}

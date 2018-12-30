using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Province
    {
        public Province()
        {
            District = new HashSet<District>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public ICollection<District> District { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Model.Position
{
    public class PositionModelReq
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public bool? Active { get; set; }
    }
}

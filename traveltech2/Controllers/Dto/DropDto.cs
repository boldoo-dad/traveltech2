using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using traveltech2.Models;

namespace traveltech2.Controllers.Dto
{
    public class DropDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public int? MenuID { get; set; }
    }
}

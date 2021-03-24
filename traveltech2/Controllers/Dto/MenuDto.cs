using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using traveltech2.Models;

namespace traveltech2.Controllers.Dto
{
    public class MenuDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public IList<Drop>? Drop { get; set; }
    }
}

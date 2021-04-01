using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using traveltech2.Models;

namespace traveltech2.Controllers.Dto
{
    public class MenusDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public int? HeadID { get; set; }
        public IEnumerable<MenuItems> MenuItems { get; set; }
    }
}

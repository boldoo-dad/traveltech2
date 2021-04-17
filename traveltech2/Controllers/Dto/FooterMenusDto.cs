using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using traveltech2.Models;

namespace traveltech2.Controllers.Dto
{
    public class FooterMenusDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? FooterID { get; set; }
        public IList<LinksDto> Links { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using traveltech2.Models;

namespace traveltech2.Controllers.Dto
{
    public class FooterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public IList<FooterMenus> FooterMenus { get; set; }
        public IList<FooterIcons> FooterIcons { get; set; }
    }
}

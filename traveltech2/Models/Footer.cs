using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace traveltech2.Models
{
    public class Footer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public IList<FooterMenus> FooterMenus { get; set; }
        public IList<FooterIcons> FooterIcons { get; set; }
    }
}

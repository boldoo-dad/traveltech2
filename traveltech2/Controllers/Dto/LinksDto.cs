using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using traveltech2.Models;

namespace traveltech2.Controllers.Dto
{
    public class LinksDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        [JsonIgnore]
        public IList<MenuItems> MenuItems { get; set; }
    }
}

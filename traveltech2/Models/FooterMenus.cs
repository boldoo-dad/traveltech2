using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace traveltech2.Models
{
    public class FooterMenus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? FooterID { get; set; }
        [JsonIgnore]
        public Footer Footer { get; set; }
        public IList<Links> Links { get; set; }
    }

}

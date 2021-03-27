using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace traveltech2.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }


        public int? HeadID { get; set; }
        [JsonIgnore]
        public Head Head { get; set; }
        public IEnumerable<Drop> Drop { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace traveltech2.Models
{
    public class Links
    {
     
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }


        [JsonIgnore]
        public IList<MenuItems> MenuItems { get; set; }
    }
}

﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace traveltech2.Models
{
    public class Drop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }


        public int? MenuID { get; set; }
        [JsonIgnore]
        public Menu Menu { get; set; }
    }
}

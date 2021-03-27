using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace traveltech2.Models
{
    public class App
    {
        public int Id { get; set; }
        public int? HeadID { get; set; }
        public Head Head { get; set; }
    }
}

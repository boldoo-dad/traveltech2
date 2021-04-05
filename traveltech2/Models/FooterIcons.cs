using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace traveltech2.Models
{
    public class FooterIcons
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        public string ImageSrc { get; set; }
        public int? FooterID { get; set; }
        [JsonIgnore]
        public Footer Footer { get; set; }

    }
}

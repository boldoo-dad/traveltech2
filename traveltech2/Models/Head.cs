


using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace traveltech2.Models
{
    public class Head 
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        public string ImageSrc { get; set; }

        public IEnumerable<Menus> Menus { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using traveltech2.Models;

namespace traveltech2.Controllers.Dto
{
    public class HeadDto
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        public string ImageSrc { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public IEnumerable<Menus> Menus { get; set; }
    }
}


using traveltech2.Models.HeadModels;

namespace traveltech2.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ToolID { get; set; }
        public Drop Tool { get; set; }
    }
}

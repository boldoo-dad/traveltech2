
using traveltech2.Models.HeadModels;

namespace traveltech2.Models
{
    public class Head
    {
        public int Id { get; set; }

        public int MenuID { get; set; }
        public Menu Menu { get; set; }
        public int MenuItemID { get; set; }
        public MenuItem MenuItem { get; set; }
        public int LogoID { get; set; }
        public Logo Logo { get; set; }
    }
}



using System.Collections.Generic;

namespace traveltech2.Models
{
    public class Head
    {
        public int Id { get; set; }

        public IEnumerable<Menu> Menu { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace traveltech2.Models
{
    public class Menu
    {
        //Menu()
        //{
        //    Drop = new HashSet<Drop>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }


        public IEnumerable<Drop> Drop { get; set; } = new HashSet<Drop>();
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace traveltech2.Models
{
    public class Logo
    {
        public int Id { get; set; }

        public string Name { get; set; }
        [NotMapped]
        public string Src { get; set; }
    }
}

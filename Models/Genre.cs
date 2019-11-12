﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaitynuProjektas.Models
{
    public class Genre
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
    }
}

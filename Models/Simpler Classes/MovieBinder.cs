using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaitynuProjektas.Models
{
    public class MovieBinder
    {
        public string title { get; set; }
        public DateTime releaseDate { get; set; }
        public int directorId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaitynuProjektas.Models
{
    public class SimpleUserScore
    {
        public int id { get; set; }
        public int movieId { get; set; }
        public int userId { get; set; }
        public float score { get; set; }
    }
}

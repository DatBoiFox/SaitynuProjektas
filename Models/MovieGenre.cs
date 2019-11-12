using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaitynuProjektas.Models
{
    public class MovieGenre
    {
        [Key]
        public int id { get; set; }
        public int movieId { get; set; }
        public int genreId { get; set; }


        public Movie movie { get; set; }
        public Genre genre { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaitynuProjektas.Models
{
    public class Movie
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public DateTime releaseDate { get; set; }
        [Required]
        public Director director { get; set; }
        public List<MovieGenre> movieGenres { get; set; }

        public List<UserScore> userScores { get; set; }
    }
}

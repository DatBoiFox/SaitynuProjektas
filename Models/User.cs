using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaitynuProjektas.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string surname { get; set; }
        [Required]
        public string nickName { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string uerLevel { get; set; }

        public List<FavoriteMovie> favoriteMovies { get; set; }
        public List<PlanToWatch> planToWatchMovies { get; set; }
        public List<UserScore> userScores { get; set; }

    }
}

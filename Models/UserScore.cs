using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaitynuProjektas.Models
{
    public class UserScore
    {
        [Key]
        public int id { get; set; }
        public int movieId { get; set; }
        public int userId { get; set; }

        public float score { get; set; }


        public Movie movie { get; set; }
        public User user { get; set; }
    }
}

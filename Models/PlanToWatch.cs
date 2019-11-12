using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaitynuProjektas.Models
{
    public class PlanToWatch
    {
        [Key]
        public int uselessId { get; set; }
        public int userId { get; set; }
        public int movieId { get; set; }

        public Movie movie { get; set; }
        public User user { get; set; }
    }
}

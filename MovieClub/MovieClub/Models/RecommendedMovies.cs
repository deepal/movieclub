using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Models
{
    public class RecommendedMovies
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Plot { get; set; }
        public double ImdbRatings { get; set; }
        public List<string> Catagories { get; set; }
    }
}
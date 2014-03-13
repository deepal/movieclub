using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Models
{
    public class FeaturedMovieDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double ImdbRatings { get; set; }
        public string PlotShort { get; set; }
        public double MovieClubRatings { get; set; }
        public string PosterURL { get; set; }
    }
}
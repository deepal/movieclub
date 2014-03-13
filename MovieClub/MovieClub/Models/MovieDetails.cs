using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Models
{
    public class MovieDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public string ReleaseDate { get; set; }
        public int Runtime { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Actors { get; set; }
        public string Writer { get; set; }
        public string PlotShort { get; set; }
        public string PlotFull { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Awards { get; set; }
        public string PosterURL { get; set; }
        public float ImdbRatings { get; set; }
        public long ImdbVotes { get; set; }
        public float MovieClubRatings { get; set; }
        public int MovieClubRentCount { get; set; }

        
    }
}
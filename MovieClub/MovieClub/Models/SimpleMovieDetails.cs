using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Models
{
    public class SimpleMovieDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string PosterURL { get; set; }
        public string Year { get; set; }
        public string ImdbId { get; set; }
        public float MovieClubRating { get; set; }
        public float ImdbRating { get; set; }
        public int MovieClubRentCount { get; set; }
        public int ViewsCount { get; set; }
    }
}
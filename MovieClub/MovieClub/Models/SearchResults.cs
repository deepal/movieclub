using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Models
{
    public class SearchResults
    {
        public List<Models.MovieResult> results { get; set; }
        public SearchResults()
        {
            results = new List<MovieResult>();
        }

        public void AddResult(MovieResult movie)
        {
            results.Add(movie);
        }

        
    }

    public class MovieResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public string Actors { get; set; }
        public string Genre { get; set; }
        public string PlotShort { get; set; }
        public float ImdbRatings { get; set; }
        public float MovieClubRatings { get; set; }
        public string PosterURL { get; set; }
    }
}
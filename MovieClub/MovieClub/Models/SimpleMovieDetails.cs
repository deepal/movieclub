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
    }
}
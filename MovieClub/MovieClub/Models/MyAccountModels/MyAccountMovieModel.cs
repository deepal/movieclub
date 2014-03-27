using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Models.MyAccountModels
{
    public class MyAccountMovieModel
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public string Categories { get; set; }
        public string Actors { get; set; }
        public string Year { get; set; }
        public float Imdb { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Models
{
    public class CategoryModel
    {
        public List<SimpleMovieDetails> MovieList { get; set; }
        public int? CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int? list { get; set; }
             
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TaggedMovieCount { get; set; }
    }
}
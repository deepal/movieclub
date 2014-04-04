using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Models.HomePageModels
{
    public class HomePageFeaturedsModel
    {
        public List<SimpleMovieDetails> RecommendedForYou { get; set; }
        public List<SimpleMovieDetails> NewlyAdded { get; set; }
        public List<List<SimpleMovieDetails>> TopCats { get; set; }

        public List<MovieClub.Models.FeaturedMovieDetails> Featureds { get; set; }
    }
}
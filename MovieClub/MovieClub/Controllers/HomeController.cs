using MovieClub.Models;
using MovieClub.Models.HomePageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieClub.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            HomePageFeaturedsModel homepagemodel = new HomePageFeaturedsModel();

            var featureds = db.DBFeatureds.ToList().Join(db.DBMovies,
                l => l.MovieId,
                r => r.Id,
                (l, r) => new
                {
                    Id = r.Id,
                    MovieName = r.Name,
                    PlotShort = r.PlotShort,
                    PosterURL = r.PosterURL,
                    MovieclubRatings = r.MovieClubRatings,
                    ImdbRatings = r.ImdbRatings
                });

            List<FeaturedMovieDetails> movies = new List<FeaturedMovieDetails>();

            foreach (var item in featureds)
            {
                movies.Add(new FeaturedMovieDetails() {
                    Id = item.Id,
                    Name = item.MovieName,
                    PlotShort = item.PlotShort,
                    PosterURL = item.PosterURL,
                    MovieClubRatings = item.MovieclubRatings,
                    ImdbRatings = item.ImdbRatings
                });
            }

            homepagemodel.Featureds = movies;

            return View(homepagemodel);
        }

    }
}

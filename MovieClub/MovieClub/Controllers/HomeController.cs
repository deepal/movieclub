using MovieClub.Models;
using MovieClub.Models.HomePageModels;
using MovieClub.Operations;
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

            List<List<SimpleMovieDetails>> topcats = new List<List<SimpleMovieDetails>>();

            List<string> topcatnames = new List<string>();
            var topcatids = HomePageOperations.GetTopCategories();

            if (topcatids.Count == 0)
            {
                return View(homepagemodel);
            }

            ViewBag.TopCatIds = topcatids;
            foreach (int i in topcatids)
            {
                string catname = db.DBCategories.Where(c => c.CategoryId == i).First().CategoryName;
                topcatnames.Add(catname);
                var mvs = db.DBMovies.Where(
                    mv => mv.Genre.Contains(catname)
                    ).ToList();
                mvs.Sort((x, y) => y.Views.CompareTo(x.Views));
                mvs = mvs.GetRange(0, Math.Min(5,mvs.Count));
                List<SimpleMovieDetails> templist = new List<SimpleMovieDetails>();
                foreach (var movie in mvs)
                {
                    templist.Add(new SimpleMovieDetails()
                    {
                        AddedDate = (DateTime)movie.AddedDate,
                        Category = movie.Genre,
                        Id = movie.Id,
                        ImdbId = movie.ImdbId,
                        ImdbRating = (float)movie.ImdbRatings,
                        MovieClubRating = (float)movie.MovieClubRatings,
                        MovieClubRentCount = movie.MovieClubRentCount,
                        Name = movie.Name,
                        PosterURL = movie.PosterURL,
                        ViewsCount = movie.Views,
                        Year = movie.Year
                    });
                }
                topcats.Add(templist);
            }
            homepagemodel.TopCats = topcats;
            ViewBag.TopCatNames = topcatnames;
            return View(homepagemodel);
        }

        [HttpGet]

        public ActionResult Error(int status)
        {

            switch ((int)status)
            {
                case 404:
                    ViewBag.Error = "Sorry! The page your are looking for is not here.";
                    break;
                case 403:
                    ViewBag.Error = "Sorry! You are not authorized to access this content.";
                    break;
                case 500:
                    ViewBag.Error = "Error occured in the server and cannot process your request right now!";
                    break;
                default:
                    ViewBag.Error = "Unknown Error occured!";
                    break;
            }

            return View();
        }
    }
}

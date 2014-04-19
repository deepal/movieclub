using MovieClub.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Text;
using MovieClub.WebServices;
using MovieClub.Operations;
using MovieClub.CustomAttributes;
using System.Text.RegularExpressions;

namespace MovieClub.Controllers
{
    
    public class ContentController : Controller
    {
        
        public const int LATEST = 1;
        public const int TOP_IMDB = 2;
        public const int TOP_MOVIECLUB = 3;
        public const int MOST_VIEWED = 4;
        public const int ALL = 5;
        

        // GET: /MovieClub/
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {            
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Item(int id)
        {
            return View();
        }
        
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Collection(int Id, int? Page, int? Method, int? list)
        {
            
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            List<SimpleMovieDetails> movielist = new List<SimpleMovieDetails>();

            if (list == null)
            {
                string catname = null;
                try
                {
                   catname = db.DBCategories.First(c => c.CategoryId == Id).CategoryName;
                }
                catch (Exception)
                {
                    return RedirectToAction("Error", "Home", new { status = "404" });
                    //return HttpNotFound();
                }
                var query = db.DBMovieToCategories.Where(ent => ent.CategoryId == Id).Join(db.DBMovies,
                r => r.MovieId,
                l => l.Id,
                (r, l) => new
                {
                    Id = l.Id,
                    MovieName = l.Name,
                    Year = l.Year,
                    Category = l.Genre,
                    PosterURL = l.PosterURL,
                    ImdbRating = l.ImdbRatings,
                    ViewsCount = l.Views,
                    MovieClubRating = l.MovieClubRatings,
                    MovieClubRentCount = l.MovieClubRentCount,
                    AddedDate = l.AddedDate
                });

                foreach (var movie in query)
                {
                    SimpleMovieDetails item =
                        new Models.SimpleMovieDetails()
                        {
                            Id = movie.Id,
                            Name = movie.MovieName,
                            Year = movie.Year,
                            Category = movie.Category,
                            PosterURL = movie.PosterURL,
                            ImdbRating = (float)movie.ImdbRating,
                            MovieClubRating = (float)movie.MovieClubRating,
                            MovieClubRentCount = movie.MovieClubRentCount,
                            ViewsCount = movie.ViewsCount,
                            AddedDate = (DateTime)movie.AddedDate
                        };
                    movielist.Add(item);
                }
                ViewBag.ListName = catname;
            }
            else
            {
                if (list > 5)
                {
                    return RedirectToAction("Error", "Home", new { status = "404" });
                }

                switch (list)
                {
                    case LATEST:
                        var querylatest = db.DBMovies.ToList();
                        querylatest.Sort((x, y) => ((DateTime)y.AddedDate).CompareTo((DateTime)x.AddedDate));
                        querylatest = querylatest.GetRange(0, Math.Min(30,querylatest.Count));
                        foreach (var movie in querylatest)
                        {
                            SimpleMovieDetails item =
                                new Models.SimpleMovieDetails()
                                {
                                    Id = movie.Id,
                                    Name = movie.Name,
                                    Year = movie.Year,
                                    Category = movie.Genre,
                                    PosterURL = movie.PosterURL,
                                    ImdbRating = (float)movie.ImdbRatings,
                                    ImdbId = movie.ImdbId,
                                    MovieClubRating = (float)movie.MovieClubRatings,
                                    MovieClubRentCount = movie.MovieClubRentCount,
                                    ViewsCount = movie.Views,
                                    AddedDate = (DateTime)movie.AddedDate
                                };
                            movielist.Add(item);
                        }
                        ViewBag.ListName = "Latest 30";
                        break;

                    case TOP_IMDB:
                        var queryImdb = db.DBMovies.ToList();
                        queryImdb.Sort((x, y) => (y.ImdbRatings).CompareTo(x.ImdbRatings));
                        foreach (var movie in queryImdb)
                        {
                            SimpleMovieDetails item =
                                new Models.SimpleMovieDetails()
                                {
                                    Id = movie.Id,
                                    Name = movie.Name,
                                    Year = movie.Year,
                                    Category = movie.Genre,
                                    PosterURL = movie.PosterURL,
                                    ImdbRating = (float)movie.ImdbRatings,
                                    ImdbId = movie.ImdbId,
                                    MovieClubRating = (float)movie.MovieClubRatings,
                                    MovieClubRentCount = movie.MovieClubRentCount,
                                    ViewsCount = movie.Views,
                                    AddedDate = (DateTime)movie.AddedDate
                                };
                            movielist.Add(item);
                        }
                        ViewBag.ListName = "Top IMDb Rated";
                        break;


                    case TOP_MOVIECLUB:
                        var querymovieclub = db.DBMovies.ToList();
                        querymovieclub.Sort((x, y) => (y.MovieClubRatings).CompareTo(x.MovieClubRatings));
                        foreach (var movie in querymovieclub)
                        {
                            SimpleMovieDetails item =
                                new Models.SimpleMovieDetails()
                                {
                                    Id = movie.Id,
                                    Name = movie.Name,
                                    Year = movie.Year,
                                    Category = movie.Genre,
                                    PosterURL = movie.PosterURL,
                                    ImdbRating = (float)movie.ImdbRatings,
                                    ImdbId = movie.ImdbId,
                                    MovieClubRating = (float)movie.MovieClubRatings,
                                    MovieClubRentCount = movie.MovieClubRentCount,
                                    ViewsCount = movie.Views,
                                    AddedDate = (DateTime)movie.AddedDate
                                };
                            movielist.Add(item);
                        }
                        ViewBag.ListName = "Top Pearson Rated";
                        break;


                    case MOST_VIEWED:
                        var queryview = db.DBMovies.ToList();
                        queryview.Sort((x, y) => (y.Views).CompareTo(x.Views));
                        foreach (var movie in queryview)
                        {
                            SimpleMovieDetails item =
                                new Models.SimpleMovieDetails()
                                {
                                    Id = movie.Id,
                                    Name = movie.Name,
                                    Year = movie.Year,
                                    Category = movie.Genre,
                                    PosterURL = movie.PosterURL,
                                    ImdbRating = (float)movie.ImdbRatings,
                                    ImdbId = movie.ImdbId,
                                    MovieClubRating = (float)movie.MovieClubRatings,
                                    MovieClubRentCount = movie.MovieClubRentCount,
                                    ViewsCount = movie.Views,
                                    AddedDate = (DateTime)movie.AddedDate
                                };
                            movielist.Add(item);
                        }
                        ViewBag.ListName = "Most Viewed";
                        break;

                    case ALL:
                        var queryall = db.DBMovies.ToList();
                        queryall.Sort((x, y) => ((DateTime)y.AddedDate).CompareTo((DateTime)x.AddedDate));
                        foreach (var movie in queryall)
                        {
                            SimpleMovieDetails item =
                                new Models.SimpleMovieDetails()
                                {
                                    Id = movie.Id,
                                    Name = movie.Name,
                                    Year = movie.Year,
                                    Category = movie.Genre,
                                    PosterURL = movie.PosterURL,
                                    ImdbRating = (float)movie.ImdbRatings,
                                    ImdbId = movie.ImdbId,
                                    MovieClubRating = (float)movie.MovieClubRatings,
                                    MovieClubRentCount = movie.MovieClubRentCount,
                                    ViewsCount = movie.Views,
                                    AddedDate = (DateTime)movie.AddedDate
                                };
                            movielist.Add(item);
                        }
                        ViewBag.ListName = "All Movies";
                        break;

                    default:
                        //show error
                        break;
                }
            }

            var movies = movielist;

            var pageC = (int)(Math.Ceiling(((double)(movies.Count)) / 15));

            if (Page == null || Page == 0)
            {
                Page = 1;
            }

            Models.CategoryModel catmodel;
            int remaining;
            if (Page <= pageC)
            {
                remaining = (movies.Count) - (15 * (((int)Page) - 1));

                var currentindex = (((int)Page) - 1) * 15;
                var currentpagemovies = movies.GetRange(currentindex, Math.Min(15, remaining));

                catmodel = new Models.CategoryModel()
                {
                    MovieList = currentpagemovies,
                    PageCount = pageC,
                };
            }
            else
            {
                return new EmptyResult();
            }

            if (Page > 0)
            {
                catmodel.CurrentPage = Page;
            }

            ViewBag.CollectionId = Id;
            catmodel.list = list;
            ViewBag.Remaining = remaining;
            if (Method == 0 || Method == null)
            {
                return View(catmodel);
            }
            else
            {
                return PartialView("_CollectionContentPartial", catmodel);
            }

        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult MovieDetails(int Id)
        { 
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            MovieDB.DBMovie dbmovieitem = null;
            try
            {
                dbmovieitem = db.DBMovies.First(mv => mv.Id == Id);
            }
            catch (Exception)
            {
                //return HttpNotFound();
                return RedirectToAction("Error", "Home", new { status = "404" });
            }

            dbmovieitem.Views += 1;
            db.SaveChanges();
            ViewBag.HasVoted = false;
            if (Request.IsAuthenticated)
            {
                var userid = UserOperations.GetCurrentUser().UserId;

                var dbreservations = db.DBReservations.Where(rs => rs.Issued == 0).ToList();

                if ((dbreservations.Count(r => r.MovieId == Id)) > 0)
                {
                    ViewBag.HasReservations = true;
                }
                else
                {
                    ViewBag.HasReservations = false;
                }
                if ((dbreservations.Count(r => r.MovieId == Id && r.UserId == userid) != 0) || (db.DBRents.Count(r => r.MovieId == Id && r.UserId == userid&&r.Returned==0) != 0))
                {
                    ViewBag.ReservationDisabled = true;
                }
                else
                {
                    ViewBag.ReservationDisabled = false;
                }

                var votes = db.DBRatings.Where(v => (v.UserId == userid) && (v.MovieId == Id));
                if (votes.Count() != 0)
                {
                    ViewBag.HasVoted = true;
                    ViewBag.UserRating = votes.First().Rating;
                }
            }
            return View(new MovieDetails() {
                Actors = dbmovieitem.Actors,
                Awards = dbmovieitem.Awards,
                Country = dbmovieitem.Country,
                Director = dbmovieitem.Director,
                Genre = dbmovieitem.Genre,
                Id = dbmovieitem.Id,
                ImdbId = dbmovieitem.ImdbId,
                ImdbRatings = (float)dbmovieitem.ImdbRatings,
                ImdbVotes = dbmovieitem.ImdbVotes,
                Language = dbmovieitem.Language,
                MovieClubRatings = (float)dbmovieitem.MovieClubRatings,
                MovieClubRentCount = dbmovieitem.MovieClubRentCount,
                Name = dbmovieitem.Name,
                PlotFull = dbmovieitem.PlotFull,
                PlotShort = dbmovieitem.PlotShort,
                PosterURL = dbmovieitem.PosterURL,
                ReleaseDate = dbmovieitem.ReleaseDate,
                Runtime = dbmovieitem.Runtime,
                Writer = dbmovieitem.Writer,
                Year = dbmovieitem.Year,
                Views = dbmovieitem.Views,
                AddedDate = (DateTime)dbmovieitem.AddedDate,
                ActorsList = stringToTagList(dbmovieitem.Actors,','),
                WritersList = stringToTagList(dbmovieitem.Writer,','),
                DirectorsList = stringToTagList(dbmovieitem.Director,','),
                TrailerURL = dbmovieitem.TrailerURL
            });
        }

        [AllowAnonymous]
        public ActionResult Sidebar()
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            var Categories = db.DBCategories.ToList<MovieDB.DBCategory>();
            Categories.Sort((c1, c2) => c1.CategoryName.CompareTo(c2.CategoryName));
            Models.SidebarCategoriesModel sidebardata = new Models.SidebarCategoriesModel();
            sidebardata.categorylist = new List<Category>();

            foreach (var category in Categories)
            {
                var taggedCount = db.DBMovieToCategories.Count(m => m.CategoryId == category.CategoryId);

                sidebardata.categorylist.Add(new Models.Category() { 
                    Id = category.CategoryId,
                    Name = category.CategoryName,
                    TaggedMovieCount = taggedCount
                });

            }

            return PartialView("_Sidebar", sidebardata);
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult Search(string q, string by)
        {
            if (by == null)
            {
                by = "name";
            }

            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            //var query = formdata["search-text"];
            System.Linq.IQueryable<MovieDB.DBMovie> resultsquery = null;
            switch (by)
            {
                case "name":
                    resultsquery = db.DBMovies.Where(m => (m.Name.ToLower()).Contains(q.ToLower()));
                    break;

                case "actor":
                    resultsquery = db.DBMovies.Where(m => m.Actors.Contains(q.ToLower()));
                    break;

                case "director":
                    resultsquery = db.DBMovies.Where(m => m.Director.Contains(q.ToLower()));
                    break;

                case "writer":
                    resultsquery = db.DBMovies.Where(m => m.Writer.Contains(q.ToLower()));
                    break;
            }
            var resultslist = new Models.SearchResults();

            foreach (var item in resultsquery)
            {
                resultslist.AddResult(new MovieResult(){ 
                    Id = item.Id,
                    Name = item.Name,
                    PlotShort = item.PlotShort,
                    PosterURL = item.PosterURL,
                    Genre = item.Genre,
                    MovieClubRatings = (float)item.MovieClubRatings,
                    ImdbRatings = (float)item.ImdbRatings,
                    Year = item.Year,
                    Actors = item.Actors
                });
                
            }
            ViewBag.SearchQuery = q;
            return View(resultslist);
        }

        public string StripHTML(string htmlString)
        {
            string pattern = @"<(.|\n)*?>";
            return Regex.Replace(htmlString, pattern, string.Empty);
        }

        public bool hasRated(int userid, int movieid)
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            int rated = db.DBRatings.Count(r => r.MovieId == movieid && r.UserId == userid);
            if (rated != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<string> stringToTagList(string inputstring,char delimiter)
        {
            List<string> tags = new List<string>();
            string[] items = inputstring.Split(delimiter);
            foreach (var item in items)
            {
                tags.Add(item.Trim());
            }
            return tags;
        }

        [HttpPost]
        [RequireMembership]  //remove this attribute after testing
        [ValidateAntiForgeryToken]
        public ActionResult RateMovie()
        {
            int mid = int.Parse(Request.Form["movieid"]);
            int uid = UserOperations.GetCurrentUser().UserId;
            float rating = float.Parse(Request.Form["rating"]);

            try{
                MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
                MovieDB.DBMovie dbmovie = db.DBMovies.First(m=>m.Id==mid);
                //var newrating = ((dbmovie.MovieClubRatings * (float)dbmovie.MovieClubVotes) + rating) / ((float)(dbmovie.MovieClubVotes+1));
                //
                if (!hasRated(uid, mid))
                {
                    db.DBRatings.Add(new MovieDB.DBRating()
                    {
                        MovieId = mid,
                        Rating = rating,
                        UserId = uid
                    });
                    db.SaveChanges();
                }
                else
                {
                    db.DBRatings.First(m => m.UserId == uid && m.MovieId == mid).Rating = rating;
                    db.SaveChanges();
                }

                var newrating = db.DBRatings.Where(m => m.MovieId == mid).Average(a => a.Rating);
                dbmovie.MovieClubRatings = (double)newrating;

                db.SaveChanges();

                return Json(new
                {
                    Rating = "" + newrating,
                    Result = "ok"
                });

            }
            catch(Exception){
                return Json(new
                {
                    Result = "error"
                });
            }
        }


        [HttpGet]
        [RequireMembership]
        public ActionResult RecommendMovie(int movieid)
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            var moviename = db.DBMovies.First(m => m.Id == movieid).Name;

            ViewBag.MovieName = moviename;
            ViewBag.MovieId = movieid;
            return View();
        }
    }
}

using MovieClub.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieClub.Controllers
{
    
    public class UserController : Controller
    {

        public ActionResult MyAccount()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddToFavorites()
        {

            var movieid = int.Parse(Request.Form["movieid"]);
            var userid = UserOperations.GetCurrentUser().UserId;
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            if (!hasFavorited(movieid, userid))
            {
                db.DBFavorites.Add(new MovieDB.DBFavorite()
                {
                    MovieID = movieid,
                    UserID = userid
                });
                db.SaveChanges();
                return Json(new
                {
                    result = "ok"
                });
            }
            else
            {
                return Json(new
                {
                    result = "error",
                    reason = "This movie is currently in your favorites!"
                });
            }
        }

        [HttpPost]
        public ActionResult AddToWatchList()
        {
            var movieid = int.Parse(Request.Form["movieid"]);
            var userid = UserOperations.GetCurrentUser().UserId;
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();

            if (!hasWatchlisted(movieid, userid))
            {
                db.DBWatchLists.Add(new MovieDB.DBWatchList()
                {
                    MovieId = movieid,
                    UserId = userid
                });
                db.SaveChanges();

                return Json(new
                {
                    result = "ok"
                });
            }
            else
            {
                return Json(new
                {
                    result = "error",
                    reason = "This movie is currently in your watchlist!"
                });
            }
        }

        [HttpPost]
        public ActionResult Recommend()
        {
            var movieid = int.Parse(Request.Form["movieid"]);
            var comment = Request.Form["comment"];
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();

            if (hasRecommended(movieid,HttpContext.User.Identity.Name))
            {
                return Json(new
                {
                    result = "error",
                    reason = "You have previously recommended this movie!"
                });
            }
            else
            {
                db.DBRecommendations.Add(new MovieDB.DBRecommendation()
                {
                    Username = HttpContext.User.Identity.Name,
                    MovieId = movieid,
                    Comment = comment,
                    Date = DateTime.Now
                });
                db.SaveChanges();

                return Json(new
                {
                    result = "ok"
                });
            }
        }

        //these are some helper methods------------------------------------------------------------------------------
        public bool hasRecommended(int movieid, string username)
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            if ((db.DBRecommendations.Count(r => r.MovieId == movieid && r.Username == username) != 0))
            {
                return true;
            }
            return false;
        }

        public bool hasFavorited(int movieid, int userid)
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            if((db.DBFavorites.Count(f=>f.MovieID==movieid&&f.UserID==userid)!=0)){
                return true;
            }
            return false;
        }

        public bool hasWatchlisted(int movieid, int userid)
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            if (db.DBWatchLists.Count(w => w.MovieId == movieid && w.UserId == userid) != 0)
            {
                return true;
            }
            return false;
        }
        //end of helper methods--------------------------------------------------------------------------------------

        [HttpPost]
        public ActionResult Reserve(int id)
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            var reservs = db.DBReservations.Count(r => r.MovieId == id);
            var moviename = db.DBMovies.First(m=>m.Id==id).Name;
            var uid = UserOperations.GetCurrentUser().UserId;

            //surround following block in try catch, and return result error in case of an error

            if ((db.DBReservations.Count(m => m.MovieId == id && m.UserId == uid)) == 0)
            {
                if ((db.DBRents.Count(m => m.MovieId == id && m.UserId == uid)) == 0)
                {
                    db.DBReservations.Add(new MovieDB.DBReservation()
                    {
                        MovieId = id,
                        UserId = UserOperations.GetCurrentUser().UserId,
                        Timestamp = DateTime.Now
                    });
                    db.SaveChanges();
                }
                else
                {
                    return Json(new
                    {
                        result = "error",
                        message = "You already own this!"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    result = "error",
                    message = "You have already reserved this movie!"
                });
            }

            if (reservs == 0)
            {
                var rents = db.DBRents.Where(re => re.MovieId == id);
                if (rents.Count() == 0)
                {
                    return Json(new
                    {
                        result = "ok",
                        message = "You have reserved \""+moviename+"\". Movie is available"
                    });
                }
                else
                {
                    return Json(new
                    {
                        result = "ok",
                        message = "You have reserved \"" + moviename + "\". Your position is 1"
                    });
                }
            }
            else
            {
                //there are more than 1 reservations
                return Json(new
                {
                    result = "ok",
                    message = "Your request has been placed \"" + moviename + "\". Your position is "+(reservs+1)
                });
            }

        }


        [HttpGet]
        public ActionResult Favorites()
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            var uid = UserOperations.GetCurrentUser().UserId;
            var favorites = db.DBFavorites.Where(f => f.UserID == uid).Join(db.DBMovies,
                r=>r.MovieID,
                l=>l.Id,
                (r, l) => new
                {
                    MovieId = l.Id,
                    MovieName = l.Name,
                    Actors = l.Actors,
                    Categories = l.Genre,
                    Year = l.Year,
                    ImdbRatings = l.ImdbRatings
                });

            List<Models.MyAccountModels.MyAccountMovieModel> favmovies = new List<Models.MyAccountModels.MyAccountMovieModel>();

            foreach (var item in favorites)
            {
                favmovies.Add(new Models.MyAccountModels.MyAccountMovieModel()
                {
                    MovieId = item.MovieId,
                    MovieName = item.MovieName,
                    Categories = item.Categories,
                    Actors = item.Actors,
                    Year = item.Year,
                    Imdb = (float)item.ImdbRatings
                });
            }
            //return Json(watchlist.ToList(), JsonRequestBehavior.AllowGet);
            return PartialView("_MovieListPartial", favmovies);
            //return Json(favorites.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Watchlist()
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            var uid = UserOperations.GetCurrentUser().UserId;
            var watchlist = db.DBWatchLists.Where(w => w.UserId == uid).Join(db.DBMovies,
                    l=>l.MovieId,
                    r=>r.Id,
                    (l, r) => new
                    {
                        MovieId = r.Id,
                        MovieName = r.Name,
                        Actors = r.Actors,
                        Categories = r.Genre,
                        Year = r.Year,
                        ImdbRatings = r.ImdbRatings
                    });
            List<Models.MyAccountModels.MyAccountMovieModel> watchlistmovies = new List<Models.MyAccountModels.MyAccountMovieModel>();

            foreach (var item in watchlist)
            {
                watchlistmovies.Add(new Models.MyAccountModels.MyAccountMovieModel() {
                    MovieId = item.MovieId,
                    MovieName = item.MovieName,
                    Categories = item.Categories,
                    Actors = item.Actors,
                    Year = item.Year,
                    Imdb = (float)item.ImdbRatings
                });
            }
            //return Json(watchlist.ToList(), JsonRequestBehavior.AllowGet);
            return PartialView("_MovieListPartial", watchlistmovies);
        }

        [HttpGet]
        public ActionResult Recommendations()
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            var recommendations = db.DBRecommendations.ToList().Join(db.DBMovies,
                    l => l.MovieId,
                    r => r.Id,
                    (l, r) => new
                    {
                        MovieId = r.Id,
                        MovieName = r.Name,
                        RecommendedBy = l.Username,
                        Comment = l.Comment,
                        Date = l.Date
                    });
            List<Models.MyAccountModels.RecommendationModel> recomList = new List<Models.MyAccountModels.RecommendationModel>();
            foreach (var recom in recommendations)
            {
                recomList.Add(new Models.MyAccountModels.RecommendationModel() {
                    MovieId = recom.MovieId,
                    MovieName = recom.MovieName,
                    RecommendedBy = recom.RecommendedBy,
                    Comment = recom.Comment,
                    Date = (DateTime)recom.Date
                });
            }
            return PartialView("_RecommendationsPartial", recomList);
           // return Json(recommendations.ToList(), JsonRequestBehavior.AllowGet);
        }

        /*public ActionResult Activity()
        {
            //this is not yet implemented
        }*/
    }
}

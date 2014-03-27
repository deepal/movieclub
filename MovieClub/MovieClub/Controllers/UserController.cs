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

        public ActionResult Reserve()
        {
            return Json(new
            {
                result = "ok"
            });
        }

        public ActionResult RequestQueue()
        {
            return Json(new
            {
                result = "ok"
            });
        }

        public ActionResult Favorites()
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            var favorites = db.DBFavorites.Where(f => f.UserID == UserOperations.GetCurrentUser().UserId).Join(db.DBMovies,
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

            return Json(favorites.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Watchlist()
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            var watchlist = db.DBWatchLists.Where(w => w.UserId == UserOperations.GetCurrentUser().UserId).Join(db.DBMovies,
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
            return Json(watchlist.ToList(), JsonRequestBehavior.AllowGet);
        }

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
            return Json(recommendations.ToList(), JsonRequestBehavior.AllowGet);
        }

        /*public ActionResult Activity()
        {
            //this is not yet implemented
        }*/
    }
}

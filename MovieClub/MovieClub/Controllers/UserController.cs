using MovieClub.CustomAttributes;
using MovieClub.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieClub.Controllers
{
    [RequireMembership]
    public class UserController : Controller
    {

        public ActionResult MyAccount()
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            var uid = UserOperations.GetCurrentUser().UserId;
            var messagecount = db.DBInboxMessages.Count(m=>m.UserId==uid&&m.Status==1);
            ViewBag.NewMessageCount = messagecount;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        [HttpGet]
        public ActionResult Suggest()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Suggest(string moviename, string info)
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();

            try
            {
                db.DBSuggestions.Add(new MovieDB.DBSuggestion()
                {
                    UserId = UserOperations.GetCurrentUser().UserId,
                    MovieName = moviename,
                    Description = info,
                    Timestamp = DateTime.Now
                });
                db.SaveChanges();

                return Json(new {
                    result = "ok",
                    message = "Your suggestion is queued!"
                });

            }
            catch (Exception)
            {
                return Json(new {
                    result = "error",
                    message = "Could not add your suggestion! Check your connection."
                });
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
        public ActionResult Reserve(int id)
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            var reservs = db.DBReservations.Count(r => r.MovieId == id&&r.Issued==0);
            var moviename = db.DBMovies.First(m=>m.Id==id).Name;
            var uid = UserOperations.GetCurrentUser().UserId;

            var dbreservations = db.DBReservations.Where(rs => rs.Issued == 0);

            //surround following block in try catch, and return result error in case of an error

            if ((dbreservations.Count(m => m.MovieId == id && m.UserId == uid)) == 0)
            {
                if ((db.DBRents.Count(m => m.MovieId == id && m.UserId == uid&&m.Returned==0)) == 0)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReviewMovie(string comment, int movieid)
        {
            var reviewEnabled = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["ReviewEnabled"]);
            var moderationEnabled = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["ModerateReviews"]);

            if(reviewEnabled){
                if (comment.Replace(" ",string.Empty).Length == 0)
                {
                    return Json(new
                    {
                        result = "empty"
                    });
                }

                MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();

                var userid = UserOperations.GetCurrentUser().UserId;

                try
                {
                    db.DBReviews.Add(new MovieDB.DBReview()
                    {
                        UserId = userid,
                        MovieId = movieid,
                        Comment = comment,
                        Timestamp = DateTime.Now,
                        Moderated = 0
                    });
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return Json(new
                    {
                        result = "error"
                    });
                }

                if (moderationEnabled)
                {
                    return Json(new
                    {
                        result = "pending"
                    });
                }
                else
                {
                    return Json(new
                    {
                        result = "ok"
                    });
                }
            }

            return Json(new
            {
                result = "error"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteReview(int rid)
        {
            try
            {
                MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
                var currentUserid = UserOperations.GetCurrentUser().UserId;

                var rv = db.DBReviews.Where(r => r.UserId == currentUserid && r.ReviewId == rid).FirstOrDefault();

                db.DBReviews.Remove(rv);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(new { 
                    result="error"
                });
            }

            return Json(new { 
                result="ok"
            });

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
            favorites.Reverse();
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
            //watchlist.Reverse();
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

            recommendations.Reverse();

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

        [HttpGet]
        public ActionResult Reservations()
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            var uid = UserOperations.GetCurrentUser().UserId;
            var reservations = db.DBReservations.Where(u=>u.UserId==uid&&u.Issued==0).Join(db.DBMovies,
                l=>l.MovieId,
                r=>r.Id,
                (l, r) => new
                {
                    MovieId = r.Id,
                    MovieName = r.Name,
                    ReservedDate = l.Timestamp
                }).ToList();
            List<Models.MyAccountModels.ReservationModel> reservlist = new List<Models.MyAccountModels.ReservationModel>();
            reservations.Sort((x, y) => ((DateTime)y.ReservedDate).CompareTo((DateTime)x.ReservedDate));
            foreach (var reserv in reservations)
            {
                reservlist.Add(new Models.MyAccountModels.ReservationModel() {
                    MovieId = reserv.MovieId,
                    MovieName = reserv.MovieName,
                    ReservedOn = reserv.ReservedDate
                });
            }
            return PartialView("_ReservationsPartial", reservlist);

        }

        [HttpGet]
        public ActionResult CurrentRents()
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            var uid = UserOperations.GetCurrentUser().UserId;
            var rents = db.DBRents.Where(r => r.UserId == uid && ((r.DueDate > DateTime.Now && r.Returned==0)||(r.Returned==0))).Join(db.DBMovies,
                l => l.MovieId,
                r => r.Id,
                (l, r) => new
                {
                    MovieId = r.Id,
                    MovieName = r.Name,
                    DateBorrowed = l.BorrowedDate,
                    DueDate = l.DueDate
                }).ToList();

            List<Models.MyAccountModels.CurrentRentModel> currentrents = new List<Models.MyAccountModels.CurrentRentModel>();
            rents.Sort((x, y) => ((DateTime)y.DateBorrowed).CompareTo((DateTime)x.DateBorrowed));
            foreach (var item in rents)
            {
                currentrents.Add(new Models.MyAccountModels.CurrentRentModel()
                {
                    MovieId = item.MovieId,
                    MovieName = item.MovieName,
                    DateBorrowed = (DateTime)item.DateBorrowed,
                    DueDate = (DateTime)item.DueDate
                });
            }
            return PartialView("_CurrentRentsPartial", currentrents);
        }

        [HttpGet]
        public ActionResult RentsHistory()
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            var uid = UserOperations.GetCurrentUser().UserId;
            var history = db.DBRents.Where(r => r.UserId == uid && ((r.DueDate < DateTime.Now && r.Returned==1)||(r.Returned==1))).Join(db.DBMovies,
                l => l.MovieId,
                r => r.Id,
                (l, r) => new
                {
                    MovieId = r.Id,
                    MovieName= r.Name,
                    DateBorrowed = l.BorrowedDate,
                    DateReturned = l.ReturnedDate
                }).ToList();

            List<Models.MyAccountModels.RentsHistoryModel> rentshistory = new List<Models.MyAccountModels.RentsHistoryModel>();

            history.Sort((x,y)=>((DateTime)y.DateReturned).CompareTo((DateTime)x.DateReturned));

            foreach (var item in history)
            {
                rentshistory.Add(new Models.MyAccountModels.RentsHistoryModel() {
                    MovieId = item.MovieId,
                    MovieName = item.MovieName,
                    DateBorrowed = (DateTime)item.DateBorrowed,
                    DateReturned = item.DateReturned
                });
            }

            return PartialView("_RentsHistoryPartial", rentshistory);
        }

        [HttpGet]
        public ActionResult Messages(bool? popup)
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            var uid = UserOperations.GetCurrentUser().UserId;

            var messages = db.DBInboxMessages.Where(m => m.UserId == uid).ToList().OrderByDescending(m=>m.Date);
            //messages.Sort((x, y) => ((DateTime)x.Date).CompareTo((DateTime)y.Date));
            List<Models.MyAccountModels.InboxMessageModel> messagebox = new List<Models.MyAccountModels.InboxMessageModel>();
            foreach (var message in messages)
            {
                messagebox.Add(new Models.MyAccountModels.InboxMessageModel()
                {
                    Date = (DateTime)message.Date,
                    Description = message.Message,
                    MessageId = message.MessageId,
                    Status = message.Status
                });
            }
            if (popup == null)
            {
                return PartialView("_InboxMessagesPartial", messagebox);
            }
            return View(messagebox);
        }

        public static int GetNewMessageCount()
        {
            var user = UserOperations.GetCurrentUser().UserId;
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();

            return db.DBInboxMessages.Count(m => m.UserId == user && m.Status == 1);
        }
        

        [HttpPost]
        public ActionResult MarkMessagesRead()
        {
            var uid = UserOperations.GetCurrentUser().UserId;
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            db.DBInboxMessages.Where(m => m.UserId == uid).ToList().ForEach(m => m.Status = 0);
            db.SaveChanges();

            return Json(new {
                result="ok",
                message="All messages marked as read!"
            });
        }

        /*public ActionResult Activity()
        {
            //this is not yet implemented
        }*/
    }
}

using MovieClub.CustomAttributes;
using MovieClub.Models;
using MovieClub.Operations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MovieClub.Controllers
{
    [RequireAdmin]
    public class AdminController : Controller
    {


        [HttpGet]
        public ActionResult CPanel()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Users()
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();

            var users = db.DBUsers.ToList();
            users.Sort((x, y) => (x.UserId).CompareTo(y.UserId));
            List<Models.AdminModels.UserModel> userlist = new List<Models.AdminModels.UserModel>();

            foreach (var user in users)
            {
                userlist.Add(new Models.AdminModels.UserModel()
                {
                    UserID = user.UserId,
                    Username = user.UserName,
                    Email = user.Email,
                    EmployeeId = user.EmpId
                });
            }

            return PartialView("_UsersAdminPartial", userlist);
        }

        [HttpGet]
        public ActionResult ManageMovies()
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();


            var dbmovies = from movie in db.DBMovies.ToList()
                           join reserv in db.DBReservations.ToList() on movie.Id equals reserv.MovieId into movreserv
                           from item in movreserv.DefaultIfEmpty()
                           select new
                           {
                               Selected = false,
                               MovieId = movie.Id,
                               MovieName = movie.Name,
                               Views = movie.Views,
                               Genre = movie.Genre,
                               AddedDate = movie.AddedDate,
                               Rents = (from rent in db.DBRents where rent.MovieId == movie.Id select rent).Count(),
                               AvailableToRent = !(db.DBRents.AsEnumerable().Any(row => movie.Id == row.MovieId)) || (db.DBRents.AsEnumerable().Any(row => movie.Id == row.MovieId && row.Returned == 1)),//(from ritem in db.DBRents where (ritem.MovieId == movie.Id && ritem.Returned == 1) select ritem).Count() + (from m in db.DBMovies where (db.DBRents.AsEnumerable().Any(row => m.Id == row.MovieId)) select m).Count(),
                               ReservationsCount = (from rsitem in db.DBReservations where rsitem.MovieId == movie.Id && rsitem.Issued == 0 select rsitem).Count(),
                               Featured = (db.DBFeatureds.AsEnumerable().Any(row => movie.Id == row.MovieId)),//(from fitem in db.DBFeatureds where fitem.MovieId == movie.Id select fitem).Count()
                               Favorites = (from fav in db.DBFavorites where fav.MovieID == movie.Id select fav).Count(),
                               Rating = (from mv in db.DBMovies where mv.Id == movie.Id select mv).First().MovieClubRatings
                           };

            List<Models.AdminModels.ManageMoviesModel> movies = new List<Models.AdminModels.ManageMoviesModel>();

            foreach (var item in dbmovies)
            {

                movies.Add(new Models.AdminModels.ManageMoviesModel()
                {
                    AddedDate = (DateTime)item.AddedDate,
                    AvailableToRent = item.AvailableToRent,
                    Featured = item.Featured,
                    Genre = item.Genre,
                    MovieId = item.MovieId,
                    MovieName = item.MovieName,
                    RentsCount = item.Rents,
                    ReservationsCount = item.ReservationsCount,
                    Selected = false,
                    ViewsCount = item.Views,
                    Favorites = item.Favorites,
                    PearsonRating = (float)item.Rating,
                    Updated = false
                });

            }


            return View(movies);
        }

        [HttpPost]
        public ActionResult ManageMovies(Models.AdminModels.ManageMoviesModel movieitem)
        {
            return Json(new
            {
                result = "ok"
            });
        }

        [HttpGet]
        public ActionResult AddMovie()
        {
            return View();
        }


        [HttpPost]
        [HandleError]
        [ValidateAntiForgeryToken]
        public ActionResult AddMovie(Models.MovieDetails movie, HttpPostedFileBase uploadFile)
        {

            if (ModelState.IsValid)
            {

                MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
                var movieexists = db.DBMovies.Count(m => m.ImdbId == movie.ImdbId);

                if (movieexists != 0)
                {
                    ModelState.AddModelError("", "\"" + movie.Name + "\" already exists!");
                    return View(movie);

                    /*
                    return Json(new
                    {
                        result = "error",
                        message = 
                    });*/

                }

                //upload trailer start
                if (uploadFile != null)
                {
                    if (uploadFile.ContentLength > 0)
                    {
                        if (Path.GetExtension(uploadFile.FileName) == ".mp4")
                        {
                            string filePath = Path.Combine(HttpContext.Server.MapPath("/Content/multimedia"), Path.GetFileName(movie.ImdbId + ".mp4"));
                            uploadFile.SaveAs(filePath);
                        }
                        else
                        {

                            ModelState.AddModelError("", "Could not upload trailer! Video file extension should be \".mp4\" ");
                            return View(movie);
                            /*
                             return Json(new {
                                result = "error",
                                message = "Could not upload trailer! Video file extension should be \".mp4\" "
                             });
                             
                             */
                        }
                    }
                }

                //if video was uploaded successfully, then save the movie poster
                SavePoster(movie.PosterURL, movie.ImdbId);
                //save movie to db
                if (AddMovieToDB(db, movie))
                {
                    /*
                    return Json(new {
                        result="ok",
                        message = "Movie \"" + movie.Name + "\" added successfully!"
                    });*/
                    ViewBag.SuccessMessage = "Movie \"" + movie.Name + "\" added successfully!";
                    return View(movie);
                }
                else
                {

                    ModelState.AddModelError("", "Could not add movie to the database! Check your connection.");
                    return View(movie);

                    /*
                    return Json(new {
                        result = "fail",
                        message = "Could not add movie to the database! Check your connection."
                    });*/
                }


            }

            ModelState.AddModelError("", "Error occured adding movie! Check whether required fields are filled.");
            return View(movie);

            /*
            return Json(new {
                result = "fail",
                message = "Error occured adding movie! Check whether required fields are filled."
            });*/
        }

        [HttpGet]
        public ActionResult ReservedMovies()
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            var reservations = db.DBReservations.Where(rs => rs.Issued == 0).ToList();
            var reservs = reservations.Join(db.DBMovies,
                    l => l.MovieId,
                    r => r.Id,
                    (l, r) => new
                    {
                        MovieId = l.MovieId,
                        MovieName = r.Name,
                        ReservationsCount = reservations.Count(rs => rs.MovieId == l.MovieId),
                    }
                ).GroupBy(mv => mv.MovieId).Select(grp => grp.First());

            List<Models.AdminModels.ReservedMovieModel> reservedmovies = new List<Models.AdminModels.ReservedMovieModel>();

            foreach (var movie in reservs)
            {
                var firstreservation = reservations.Where(m => m.MovieId == movie.MovieId).OrderByDescending(m => m.Timestamp).Last();
                reservedmovies.Add(new Models.AdminModels.ReservedMovieModel()
                {
                    MovieId = movie.MovieId,
                    MovieName = movie.MovieName,
                    ReservationsCount = reservations.Count(r => r.MovieId == movie.MovieId),
                    UserId = firstreservation.UserId,
                    Username = db.DBUsers.First(u => u.UserId == firstreservation.UserId).UserName,
                    DateReserved = firstreservation.Timestamp
                });
            }
            return PartialView("_ReservedMoviesPartial", reservedmovies);
        }

        [HttpPost]
        public ActionResult Issue(int movie, int u)
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            var ures = db.DBReservations.Where(ur => ur.UserId == u && ur.MovieId == movie);
            if (ures.Count() != 0)
            {

                var rents = db.DBRents.Where(rs => rs.MovieId == movie && rs.Returned == 0);

                if (rents.Count() == 0)
                {
                    ures.ToList().ForEach(m => m.Issued = 1);
                    db.DBRents.Add(new MovieDB.DBRent()
                    {
                        UserId = ures.First().UserId,
                        MovieId = movie,
                        DueDate = DateTime.Now.AddDays((double)2),
                        BorrowedDate = DateTime.Now
                    });

                    db.SaveChanges();

                    return Json(new
                    {
                        result = "ok",
                        message = "Movie issued to the user"
                    });
                }
                else
                {
                    return Json(new
                    {
                        result = "error",
                        message = "Movie is not yet available"
                    });
                }

            }
            else
            {
                return Json(new
                {
                    result = "fail",
                    message = "Invalid operation"
                });
            }
        }


        public bool AddMovieToDB(MovieDB.MovieClubDBE db, MovieDetails movie)
        {
            MovieDB.DBMovie dbmovie = new MovieDB.DBMovie();
            MovieDB.DBCategory dbcat = new MovieDB.DBCategory();
            dbmovie.Actors = movie.Actors;
            dbmovie.Awards = movie.Awards;
            dbmovie.Country = movie.Country;
            dbmovie.Director = movie.Director;
            dbmovie.Genre = movie.Genre;
            dbmovie.ImdbId = movie.ImdbId;
            dbmovie.ImdbRatings = movie.ImdbRatings;
            dbmovie.ImdbVotes = movie.ImdbVotes;
            dbmovie.Language = movie.Language;
            dbmovie.MovieClubRatings = movie.MovieClubRatings;
            dbmovie.MovieClubRentCount = movie.MovieClubRentCount;
            dbmovie.Name = movie.Name;
            dbmovie.PlotFull = movie.PlotFull;
            dbmovie.PlotShort = movie.PlotShort;
            dbmovie.PosterURL = System.Configuration.ConfigurationManager.AppSettings["PosterPath"] + "/" + movie.ImdbId + Path.GetExtension(movie.PosterURL);
            dbmovie.ReleaseDate = movie.ReleaseDate;
            dbmovie.Runtime = movie.Runtime;
            dbmovie.Writer = movie.Writer;
            dbmovie.Year = movie.Year;
            dbmovie.AddedDate = DateTime.Now;
            dbmovie.TrailerURL = System.Configuration.ConfigurationManager.AppSettings["VideoSavePath"] + "/" + movie.ImdbId + ".mp4";
            db.DBMovies.Add(dbmovie);
            db.SaveChanges();
            string[] categories = (movie.Genre).Split(',');

            var currentMovieId = db.DBMovies.Where(m => m.Name == movie.Name).First().Id;

            List<MovieDB.DBCategory> currentlist = db.DBCategories.ToList<MovieDB.DBCategory>();
            foreach (string cat in categories)
            {
                var tag = cat.Trim();
                tag.Replace(" ", string.Empty);
                if ((currentlist.FindAll(c => c.CategoryName == tag)).Count == 0)
                {
                    dbcat.CategoryName = tag;
                    db.DBCategories.Add(dbcat);
                    db.SaveChanges();
                }
                var currentcatId = db.DBCategories.Where(c => c.CategoryName == tag).First().CategoryId;

                MovieDB.DBMovieToCategory entry = new MovieDB.DBMovieToCategory();

                entry.MovieId = currentMovieId;
                entry.CategoryId = (int?)currentcatId;

                db.DBMovieToCategories.Add(entry);
                db.SaveChanges();
            }

            db.SaveChanges();

            return true;

        }

        [HttpGet]
        public ActionResult PendingReturns()
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            var pendings = db.DBRents.Where(r => (r.DueDate < DateTime.Now && r.Returned == 0) || (r.Returned == 0)).Join(db.DBMovies,
                    l => l.MovieId,
                    r => r.Id,
                    (r, l) => new
                    {
                        MovieId = l.Id,
                        MovieName = l.Name,
                        DateTaken = r.BorrowedDate,
                        DueDate = r.DueDate,
                        UserId = r.UserId
                    }
                ).Join(db.DBUsers,
                    l => l.UserId,
                    r => r.UserId,
                    (l, r) => new
                    {
                        MovieId = l.MovieId,
                        MovieName = l.MovieName,
                        DateTaken = l.DateTaken,
                        DueDate = l.DueDate,
                        UserId = l.UserId,
                        Username = r.UserName,
                        UserEmail = r.Email
                    }
                );
            List<Models.AdminModels.PendingReturnsModel> preturns = new List<Models.AdminModels.PendingReturnsModel>();

            foreach (var item in pendings)
            {
                preturns.Add(new Models.AdminModels.PendingReturnsModel()
                {
                    MovieId = item.MovieId,
                    MovieName = item.MovieName,
                    UserId = (int)item.UserId,
                    UserName = item.Username,
                    Email = item.UserEmail,
                    DueDate = (DateTime)item.DueDate,
                    DateTaken = (DateTime)item.DateTaken
                });
            }

            return PartialView("_PendingReturnsPartial", preturns);
        }

        [HttpPost]
        public ActionResult FeatureMovie(int movieid, bool featured)
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            if (featured)
            {
                var featuredcount = db.DBFeatureds.ToList().Count;
                if (featuredcount < 15)
                {
                    db.DBFeatureds.Add(new MovieDB.DBFeatured()
                    {
                        MovieId = movieid,
                        Date = DateTime.Now
                    });
                    db.SaveChanges();


                    if (featuredcount == 9)
                    {
                        return Json(new
                        {
                            result = "ok",
                            message = "Movie added to featured list",
                            full = "true"
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            result = "ok",
                            message = "Movie added to featured list"
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        result = "error",
                        message = "You can add upto 10 featured movies"
                    });
                }
            }
            else
            {
                if (db.DBFeatureds.Count(m => m.MovieId == movieid) != 0)
                {
                    db.DBFeatureds.Remove(db.DBFeatureds.First(m => m.MovieId == movieid));
                    db.SaveChanges();

                    return Json(new
                    {
                        result = "ok",
                        message = "Movie removed from featured list"
                    });
                }

                return Json(new
                {
                    result = "none"
                });

            }

        }

        [HttpPost]
        public ActionResult MarkAsReturned(int movieid, int u)
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            db.DBRents.Where(r => r.UserId == u && r.MovieId == movieid).ToList().ForEach(m => m.Returned = 1);
            var rents = db.DBRents.Where(r => r.UserId == u && r.MovieId == movieid).ToList();
            //now I need update the respective row in rents and update the returned date 
            rents.OrderByDescending(m => m.BorrowedDate).First().ReturnedDate = DateTime.Now;

            DateTime borroweddate = (DateTime)rents.OrderBy(m=>m.BorrowedDate).First().BorrowedDate;

            var fine = MovieIssuesOperations.CalculatedFine(borroweddate, DateTime.Now);
            var charge = MovieIssuesOperations.CalculateCharge(borroweddate,DateTime.Now);

            db.DBPaymentsDues.Add(new MovieDB.DBPaymentsDue() {
                Charge=charge,
                Fine=fine,
                MovieId=movieid,
                UserId=u,
                Paid=0,
            });

            db.SaveChanges();

            return Json(new
            {
                result = "ok",
                message = "Movie marked as returned!"
            });
        }

        [HttpGet]
        public ActionResult SendMessage(int userid)
        {
            ViewBag.To = userid;
            return View();
        }

        [HttpPost]
        public ActionResult SendMessage()
        {
            var to = int.Parse((string)Request.Form["To"]);
            var comment = Request.Form["Comment"];

            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();

            db.DBInboxMessages.Add(new MovieDB.DBInboxMessage()
            {
                Date = DateTime.Now,
                Message = comment,
                Status = 1,
                UserId = to
            });

            db.SaveChanges();

            return Json(new
            {
                result = "ok",
                message = "Message sent!"
            });
        }

        public void SavePoster(string Url, string ImdbId)
        {
            string LocalPath = HttpContext.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["PosterDownloadPath"]) + "/" + ImdbId + Path.GetExtension(Url);
            WebClient client = new WebClient();
            client.DownloadFile(Url, LocalPath);
            return;
        }
    }



}

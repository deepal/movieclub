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
                bool admin = false;

                if (db.DBAdmins.Count(ad => ad.UserId == user.UserId) > 0)
                {
                    admin = true;
                }

                userlist.Add(new Models.AdminModels.UserModel()
                {
                    UserID = user.UserId,
                    Username = user.UserName,
                    Email = user.Email,
                    EmployeeId = user.EmpId,
                    IsAdmin = admin
                });
            }

            return PartialView("_UsersAdminPartial", userlist);
        }

        [HttpGet]
        public ActionResult Movies()
        {
            return View();
        }



        [HttpGet]
        public ActionResult ManageMovies(string q)
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            List<MovieDB.DBMovie> mlist;

            if (q != null && q != "")
            {
                mlist = db.DBMovies.Where(m => m.Name.Contains(q.ToLower())).ToList();
            }
            else
            {
                mlist = db.DBMovies.ToList();
            }

            var dbmovies = from movie in mlist
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
                    MovieClubRating = (float)item.Rating,
                    Updated = false
                });

            }


            return PartialView("_ManageMoviesPartial",movies.GroupBy(m=>m.MovieId).Select(grp=>grp.First()).ToList());
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
                        if ((Path.GetExtension(uploadFile.FileName)).ToLower() == ".mp4")
                        {
                            string filePath = Path.Combine(HttpContext.Server.MapPath("/Content/multimedia"), Path.GetFileName(movie.ImdbId + ".mp4"));
                            uploadFile.SaveAs(filePath);
                            movie.TrailerURL = System.Configuration.ConfigurationManager.AppSettings["VideoSavePath"] + "/" + movie.ImdbId + ".mp4";
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
                movie.PosterURL = SavePoster(movie.PosterURL, movie.ImdbId);
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
            //reservations.Sort((x, y) => y.Timestamp.CompareTo(x.Timestamp));
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
        [ValidateAntiForgeryToken]
        public ActionResult Issue(int movie, int u, bool? unreserved)
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();

            if (unreserved != null)
            {
                if ((bool)unreserved)
                {

                    var isreserved = db.DBMovies.Where(m => db.DBReservations.Any(mv => mv.MovieId == movie && mv.Issued==0)).Count();
                    var isrented = db.DBRents.Where(mv=>db.DBRents.Any(mr=>mr.MovieId==movie && mr.Returned==0)).Count();
                    if (isreserved > 0)
                    {
                        return Json(new {
                            result="error",
                            message = "Movie has reservations! Cannot be issued."
                        });
                    }
                    else if(isrented > 0)
                    {
                        return Json(new
                        {
                            result = "error",
                            message = "Movie is not available yet!"
                        });
                    }
                    else
                    {
                        db.DBRents.Add(new MovieDB.DBRent()
                        {
                            MovieId = movie,
                            Returned = 0,
                            UserId = u,
                            BorrowedDate = DateTime.Now,
                            DueDate = DateTime.Now.AddDays(double.Parse(System.Configuration.ConfigurationManager.AppSettings["DefaultRentDuration"]))
                        });
                        db.SaveChanges();

                        return Json(new
                        {
                            result = "ok",
                            message = "Movie issued!"
                        });
                    }

                    /*
                    var unresmovies = db.DBMovies.Where(m => !db.DBReservations.Any(mv => mv.MovieId == movie)).Select(m => new SimpleMovieDetails() {
                        Id = m.Id,
                        AddedDate = (DateTime)m.AddedDate,
                        Category = m.Genre,
                        ImdbId = m.ImdbId,
                        ImdbRating = (float)m.ImdbRatings,
                        MovieClubRating = (float)m.MovieClubRatings,
                        MovieClubRentCount = m.MovieClubRentCount,
                        Name = m.Name,
                        PosterURL = m.PosterURL,
                        ViewsCount = m.Views,
                        Year = m.Year
                    });*/
                }
            }

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
            dbmovie.PosterURL = movie.PosterURL; //System.Configuration.ConfigurationManager.AppSettings["PosterPath"] + "/" + movie.ImdbId + Path.GetExtension(movie.PosterURL);
            dbmovie.ReleaseDate = movie.ReleaseDate;
            dbmovie.Runtime = movie.Runtime;
            dbmovie.Writer = movie.Writer;
            dbmovie.Year = movie.Year;
            dbmovie.AddedDate = DateTime.Now;
            dbmovie.TrailerURL = movie.TrailerURL;
            db.DBMovies.Add(dbmovie);
            db.SaveChanges();
            string[] categories = (movie.Genre).Split(',');

            var currentMovieId = db.DBMovies.Where(m => m.Name == movie.Name).First().Id;

            List<MovieDB.DBCategory> currentlist = db.DBCategories.ToList<MovieDB.DBCategory>();
            foreach (string cat in categories)
            {
                MovieDB.DBCategory dbcat = new MovieDB.DBCategory();
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
                ).ToList();
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
        [ValidateAntiForgeryToken]
        public ActionResult FeatureMovie(int movieid, bool featured)
        {
            var maxfeats = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxFeaturedsCount"]);
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            if (featured)
            {
                var featuredcount = db.DBFeatureds.ToList().Count;
                if (featuredcount < maxfeats )
                {
                    db.DBFeatureds.Add(new MovieDB.DBFeatured()
                    {
                        MovieId = movieid,
                        Date = DateTime.Now
                    });
                    db.SaveChanges();

                    return Json(new
                    {
                        result = "ok",
                        message = "Movie added to featured list"
                    });
                }
                else
                {
                    return Json(new
                    {
                        result = "error",
                        message = "You can add upto "+maxfeats+" featured movies"
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
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMovie(int movieid)
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();

            try
            {
                var movie = db.DBMovies.Where(m => m.Id == movieid).SingleOrDefault();

                db.DBMovies.Remove(movie);

                db.SaveChanges();

                return Json(new
                {
                    result = "ok",
                    message = "Movie Deleted"
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    result = "error",
                    message = "Error occured. Check your connection."
                });
            }
        }

        public string SavePoster(string Url, string ImdbId)
        {
            string RelativePath = System.Configuration.ConfigurationManager.AppSettings["PosterDownloadPath"] + "/" + ImdbId + Path.GetExtension(Url);
            string LocalPath = HttpContext.Server.MapPath(RelativePath);
            WebClient client = new WebClient();
            try
            {
                client.DownloadFile(Url, LocalPath);
            }
            catch (Exception)
            {
                return "/Content/images/poster-na.jpg" ;
            }
            return RelativePath.Replace("~",string.Empty);
        }

        [HttpGet]
        public ActionResult GetPaymentDues()
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();

            var pays = db.DBPaymentsDues.Where(p=>p.Paid==0).ToList();
            var paybyuser = pays.GroupBy(p => p.UserId).Select(pu => new
            {
                UserId = pu.Key,
                TotalFine = pu.Sum(m => m.Fine),
                TotalCharge = pu.Sum(m => m.Charge)
            }).Join(db.DBUsers,
                l=>l.UserId,
                r=>r.UserId,
                (l, r) => new
                {
                    UserId = l.UserId,
                    Username = r.UserName,
                    UserEmail = r.Email,
                    TotalFine = l.TotalFine,
                    TotalCharge = l.TotalCharge,
                    TotalPayment = l.TotalCharge+l.TotalFine
                }
            );

            List<Models.AdminModels.PendingPaymentsModel> ppay = new List<Models.AdminModels.PendingPaymentsModel>();

            foreach (var item in paybyuser)
            {
                ppay.Add(new Models.AdminModels.PendingPaymentsModel() {
                    UserId = item.UserId,
                    Username = item.Username,
                    UserEmail = item.UserEmail,
                    TotalCharge = (float)item.TotalCharge,
                    TotalFines = (float)item.TotalFine,
                    TotalPaymentDue = (float)item.TotalPayment
                });
            }

            return PartialView("_PendingPaymentsPartial",ppay);
        }

        [HttpGet]
        public ActionResult DetailedPayment(int userid)
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            var userpayment = db.DBPaymentsDues.Where(up => up.UserId == userid && up.Paid==0).Join(db.DBMovies,
                l=>l.MovieId,
                r=>r.Id,
                (l, r) => new
                {
                    MovieId = l.MovieId,
                    MovieName = r.Name,
                    Fine = l.Fine,
                    Charge = l.Charge
                }
                );

            List<Models.AdminModels.DetailedPaymentModel> paylist = new List<Models.AdminModels.DetailedPaymentModel>();
            double fineTotal = 0;
            double chargeTotal = 0;
            foreach (var item in userpayment)
            {
                paylist.Add(new Models.AdminModels.DetailedPaymentModel() {
                    Charge = (float)item.Charge,
                    Fine = (float)item.Fine,
                    MovieId = item.MovieId,
                    MovieName = item.MovieName
                });
                fineTotal += item.Fine;
                chargeTotal += item.Charge;
            }
            ViewBag.PaymentUser = userid;
            ViewBag.FineTotal = fineTotal;
            ViewBag.ChargeTotal = chargeTotal;

            return View(paylist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PerformTransaction(int userid, float payment){
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            db.DBPaymentsDues.Where(p => p.UserId == userid && p.Paid==0).ToList().ForEach(m => m.Paid = 1);;

            db.DBPaymentHistories.Add(new MovieDB.DBPaymentHistory() {
                PayAmount = payment,
                Timestamp = DateTime.Now,
                UserId = userid
            });

            db.SaveChanges();

            return Json(new
            {
                result = "ok",
                message = "Payment complete!"
            });
        }

        [HttpGet]
        public ActionResult IssueUnreserved()
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();

            var movies = db.DBMovies.ToList();
            movies.Sort((x, y) => x.Name.CompareTo(y.Name));

            movies = movies.Where(m => !db.DBReservations.Any(r => r.MovieId == m.Id && r.Issued==0)).ToList();
            movies = movies.Where(m => !db.DBRents.Any(rn => rn.MovieId == m.Id && rn.Returned == 0)).ToList();

            var users = db.DBUsers.ToList();
            users.Sort((x, y) => x.UserName.CompareTo(y.UserName));

            List<SimpleMovieDetails> mv = new List<SimpleMovieDetails>();
            List<UserDetails> usr = new List<UserDetails>();

            foreach (var movie in movies)
            {
                mv.Add(new SimpleMovieDetails() {
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

            foreach (var user in users)
            {
                usr.Add(new UserDetails() {
                    UserName = user.UserName,
                    UserId = user.UserId,
                    IsAdmin = false,
                    PhotoURL = user.PhotoURL
                });
            }

            return View(new Models.AdminModels.IssueUnreservedModel() {
                users = usr,
                movies = mv
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MakeAdmin(int u)
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            var currentuserid = Operations.UserOperations.GetCurrentUser().UserId;
            var dbadmins = db.DBAdmins.ToList();

            if (u != currentuserid)
            {
                if (dbadmins.Count(a => a.UserId == u) > 0)
                {
                    return Json(new
                    {
                        result = "error",
                        message = "User already an admin!"
                    });
                }
                else
                {
                    db.DBAdmins.Add(new MovieDB.DBAdmin()
                    {
                        UserId = u
                    });
                    db.SaveChanges();
                    return Json(new
                    {
                        result = "ok",
                        message = "User promoted as an admin"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    result = "error",
                    message = "You are not authorized to perform this operation!"
                });
            }

            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveAdmin(int u)
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            var currentuserid = Operations.UserOperations.GetCurrentUser().UserId;
            var dbadmins = db.DBAdmins.Where(a => a.UserId == u);

            if (u != currentuserid)
            {
                if (dbadmins.ToList().Count > 0)
                {
                    db.DBAdmins.Remove(dbadmins.SingleOrDefault());
                    db.SaveChanges();

                    return Json(new
                    {
                        result = "ok",
                        message = "User removed from admins list"
                    });
                }
                else
                {
                    return Json(new
                    {
                        result = "error",
                        message = "User is not an admin"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    result = "error",
                    message = "User cannot remove yourself from admins!"
                });
            }

            

        }

        [HttpGet]
        public ActionResult GetPaymentHistory()
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();

            var payments = db.DBPaymentHistories.Where(p=>p.PayAmount!=0).Join(db.DBUsers,
                l=>l.UserId,
                r=>r.UserId,
                (l, r) => new
                {
                    UserId = l.UserId,
                    Username = r.UserName,
                    PayAmount = l.PayAmount,
                    Timestamp = l.Timestamp
                });

            List<Models.AdminModels.PaymentHistoryModel> historylist = new List<Models.AdminModels.PaymentHistoryModel>();

            foreach (var item in payments)
            {
                historylist.Add(new Models.AdminModels.PaymentHistoryModel() {
                    UserId = item.UserId,
                    Username = item.Username,
                    PayAmount = (float)item.PayAmount,
                    Timestamp = item.Timestamp
                });
            }
            historylist.Sort((x, y) => ((DateTime)y.Timestamp).CompareTo((DateTime)x.Timestamp));
            return PartialView("_PaymentHistoryPartial",historylist);

        }

        //[HttpGet]
        //public ActionResult GetRentsHistory()
        //{
            
        //}
    }



}

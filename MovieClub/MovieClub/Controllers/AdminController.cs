using MovieClub.CustomAttributes;
using MovieClub.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MovieClub.Controllers
{
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
                userlist.Add(new Models.AdminModels.UserModel() {
                    UserID = user.UserId,
                    Username = user.UserName,
                    Email = user.Email,
                    EmployeeId = user.EmpId
                });
            }

            return PartialView("_UsersAdminPartial",userlist);
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

                    return Json(new
                    {
                        result = "error",
                        message = "\""+movie.Name+"\" already exists!"
                    });
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
                            return Json(new {
                                result = "error",
                                message = "Could not upload trailer! Video file extension should be \".mp4\" "
                            });
                        }
                    }
                }

                //if video was uploaded successfully, then save the movie poster
                SavePoster(movie.PosterURL, movie.ImdbId);
                //save movie to db
                if (AddMovieToDB(db, movie))
                {
                    return Json(new {
                        result="ok",
                        message = "Movie \"" + movie.Name + "\" added successfully!"
                    });
                }
                else
                {
                    return Json(new {
                        result = "fail",
                        message = "Could not add movie to the database! Check your connection."
                    });
                }


            }
            return Json(new {
                result = "fail",
                message = "Error occured adding movie! Check whether required fields are filled."
            });
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
            dbmovie.PosterURL = System.Configuration.ConfigurationManager.AppSettings["PosterPath"]+"/"+movie.ImdbId+Path.GetExtension(movie.PosterURL);
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
            var pendings = db.DBRents.Where(r=>(r.DueDate<DateTime.Now && r.Returned==0)||(r.Returned==0)).Join(db.DBMovies,
                    l=>l.MovieId,
                    r=>r.Id,
                    (r,l)=>new{
                        MovieId = l.Id,
                        MovieName = l.Name,
                        DateTaken = r.BorrowedDate,
                        DueDate = r.DueDate,
                        UserId = r.UserId
                    }
                ).Join(db.DBUsers,
                    l=>l.UserId,
                    r=>r.UserId,
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
                preturns.Add(new Models.AdminModels.PendingReturnsModel(){
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

        [HttpGet]
        public ActionResult MarkAsReturned(int movieid, int u)
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            db.DBRents.Where(r => r.UserId == u && r.MovieId == movieid).ToList().ForEach(m=>m.Returned=1);
            return Json(new
            {
                result="ok"
            });
        }

        [HttpGet]
        public ActionResult SendMessage(int userid)
        {
            return Json(new
            {
                result = "ok"
            });
        } 

        public void SavePoster(string Url, string ImdbId)
        {
            string LocalPath = HttpContext.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["PosterDownloadPath"]) + "/" + ImdbId + Path.GetExtension(Url);
            WebClient client = new WebClient();
            client.DownloadFile(Url,LocalPath);
            return;
        }
    }

    

}

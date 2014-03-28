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
                        }
                    }
                }

                //if video was uploaded successfully, then save the movie poster
                SavePoster(movie.PosterURL, movie.ImdbId);
                //save movie to db
                MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
                if (AddMovieToDB(db, movie))
                {
                    ViewBag.SuccessMessage = "Movie \"" + movie.Name + "\" added successfully!";
                    ModelState.Clear();
                }
                else
                {
                    ModelState.AddModelError("", "Could not add movie to the database! Check your connection.");
                }


                return View();

            }
            ModelState.AddModelError("", "Error occured adding movie! Check whether required fields are filled.");
            return View(movie);
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


        public void SavePoster(string Url, string ImdbId)
        {
            string LocalPath = HttpContext.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["PosterDownloadPath"]) + "/" + ImdbId + Path.GetExtension(Url);
            WebClient client = new WebClient();
            client.DownloadFile(Url,LocalPath);
            return;
        }
    }

    

}

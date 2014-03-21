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
    [Authorize]
    public class AdminController : Controller
    {

        
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
                /*
                try
                {
                    var db = new MovieDataContext();
                    db.Movies.Add(movie);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
                
                 */

                //upload trailer start
                if (uploadFile.ContentLength > 0)
                {
                    if (Path.GetExtension(uploadFile.FileName) == ".mp4")
                    {
                        string filePath = Path.Combine(HttpContext.Server.MapPath("/Content/multimedia"), Path.GetFileName(movie.ImdbId+".mp4"));
                        uploadFile.SaveAs(filePath);

                        //if video was uploaded successfully, then save the movie poster
                        SavePoster(movie.PosterURL, movie.ImdbId);

                        ViewBag.SuccessMessage = "Movie \""+movie.Name+"\" added successfully!";
                        ModelState.Clear();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Could not upload trailer! Video file extension should be \".mp4\" ");
                        return View(movie);
                    }
                }
                //upload trailer end

                return View(new Models.MovieDetails());

            }
            ModelState.AddModelError("", "Error occured adding movie! Check whether required fields are filled.");
            return View(movie);
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

using MovieClub.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        [ValidateAntiForgeryToken]
        public ActionResult AddMovie(Models.MovieDetails movie)
        {
            if (ModelState.IsValid)
            {
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
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Error adding movie!");
            return View(movie);
        }

        [HandleError]
        [HttpGet]
        public ActionResult Upload()
        {
            return PartialView("_Trailer");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Upload(HttpPostedFileBase uploadFile)
        {
            if (uploadFile.ContentLength > 0)
            {
                string filePath = Path.Combine(HttpContext.Server.MapPath("/Content/multimedia"), Path.GetFileName(uploadFile.FileName));
                uploadFile.SaveAs(filePath);
            }
            return View();
        }

    }
}

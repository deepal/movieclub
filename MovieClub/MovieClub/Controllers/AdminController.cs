using MovieClub.Models;
using System;
using System.Collections.Generic;
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
    }
}

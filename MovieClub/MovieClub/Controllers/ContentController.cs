using MovieClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieClub.Controllers
{
    public class ContentController : Controller
    {
        //
        // GET: /MovieClub/

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Item(int id)
        {
            var db = new MovieDataContext();
            var movie = db.Movies.Find(3);
            var movies = db.Movies.ToArray();
            int len = movies.Length;
            return View(movie);
        }

        public ActionResult Catagory(string catagory)
        {
            return View();
        }

        public ActionResult MovieDetails(int movieid)
        {
            return View();
        }

        
    }
}

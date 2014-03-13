using MovieClub.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Text;
using MovieClub.WebServices;

namespace MovieClub.Controllers
{
    public class ContentController : Controller
    {
        //
        // GET: /MovieClub/

        private ImdbMovie moviedata;

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

        [HttpGet]
        public ActionResult MovieDetails(string moviename)
        {
            ViewBag.Name = moviename;
            ImdbMovie imdbmoviedata = JsonConvert.DeserializeObject<ImdbMovie>(ImdbData.GetData(moviename));
            
            //the logic should be to get imdb data and insert them into a MovieDetails model with every required fields converted
            //the MovieDetails model should be passed to this' view. view should also strongly typed into this model.
            return View(imdbmoviedata);
        }

        
    }
}

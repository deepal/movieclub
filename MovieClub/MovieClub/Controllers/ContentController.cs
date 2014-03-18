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
    [Authorize]
    public class ContentController : Controller
    {
        //
        // GET: /MovieClub/

        private ImdbMovie moviedata;

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Item(int id)
        {
            var db = new MovieDataContext();
            var movie = db.Movies.Find(3);
            var movies = db.Movies.ToArray();
            int len = movies.Length;
            return View(movie);
        }

        public List<SimpleMovieDetails> GetMovieList()
        {
            var movies = new List<SimpleMovieDetails>()
            {
                new Models.SimpleMovieDetails(){
                    Id = 1,
                    Name = "The Pursuit of Happiness",
                    Year = "2006",
                    Category = "Biography, Drama",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTQ5NjQ0NDI3NF5BMl5BanBnXkFtZTcwNDI0MjEzMw@@._V1_SX300.jpg",
                },
                new Models.SimpleMovieDetails(){
                    Id = 1,
                    Name = "The Pursuit of Happiness",
                    Year = "2006",
                    Category = "Biography, Drama",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTQ5NjQ0NDI3NF5BMl5BanBnXkFtZTcwNDI0MjEzMw@@._V1_SX300.jpg",
                },
                new Models.SimpleMovieDetails(){
                    Id = 1,
                    Name = "The Pursuit of Happiness",
                    Year = "2006",
                    Category = "Biography, Drama",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTQ5NjQ0NDI3NF5BMl5BanBnXkFtZTcwNDI0MjEzMw@@._V1_SX300.jpg",
                },
                new Models.SimpleMovieDetails(){
                    Id = 1,
                    Name = "The Pursuit of Happiness",
                    Year = "2006",
                    Category = "Biography, Drama",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTQ5NjQ0NDI3NF5BMl5BanBnXkFtZTcwNDI0MjEzMw@@._V1_SX300.jpg",
                },
                new Models.SimpleMovieDetails(){
                    Id = 1,
                    Name = "The Pursuit of Happiness",
                    Year = "2006",
                    Category = "Biography, Drama",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTQ5NjQ0NDI3NF5BMl5BanBnXkFtZTcwNDI0MjEzMw@@._V1_SX300.jpg",
                },
                new Models.SimpleMovieDetails(){
                    Id = 1,
                    Name = "The Pursuit of Happiness",
                    Year = "2006",
                    Category = "Biography, Drama",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTQ5NjQ0NDI3NF5BMl5BanBnXkFtZTcwNDI0MjEzMw@@._V1_SX300.jpg",
                },
                new Models.SimpleMovieDetails(){
                    Id = 1,
                    Name = "The Pursuit of Happiness",
                    Year = "2006",
                    Category = "Biography, Drama",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTQ5NjQ0NDI3NF5BMl5BanBnXkFtZTcwNDI0MjEzMw@@._V1_SX300.jpg",
                },
                new Models.SimpleMovieDetails(){
                    Id = 1,
                    Name = "The Pursuit of Happiness",
                    Year = "2006",
                    Category = "Biography, Drama",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTQ5NjQ0NDI3NF5BMl5BanBnXkFtZTcwNDI0MjEzMw@@._V1_SX300.jpg",
                },
                new Models.SimpleMovieDetails(){
                    Id = 1,
                    Name = "The Pursuit of Happiness",
                    Year = "2006",
                    Category = "Biography, Drama",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTQ5NjQ0NDI3NF5BMl5BanBnXkFtZTcwNDI0MjEzMw@@._V1_SX300.jpg",
                },
                new Models.SimpleMovieDetails(){
                    Id = 1,
                    Name = "The Pursuit of Happiness",
                    Year = "2006",
                    Category = "Biography, Drama",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTQ5NjQ0NDI3NF5BMl5BanBnXkFtZTcwNDI0MjEzMw@@._V1_SX300.jpg",
                },
                new Models.SimpleMovieDetails(){
                    Id = 1,
                    Name = "The Pursuit of Happiness",
                    Year = "2006",
                    Category = "Biography, Drama",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTQ5NjQ0NDI3NF5BMl5BanBnXkFtZTcwNDI0MjEzMw@@._V1_SX300.jpg",
                },
                new Models.SimpleMovieDetails(){
                    Id = 1,
                    Name = "The Pursuit of Happiness",
                    Year = "2006",
                    Category = "Biography, Drama",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTQ5NjQ0NDI3NF5BMl5BanBnXkFtZTcwNDI0MjEzMw@@._V1_SX300.jpg",
                },
                new Models.SimpleMovieDetails(){
                    Id = 1,
                    Name = "The Pursuit of Happiness",
                    Year = "2006",
                    Category = "Biography, Drama",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTQ5NjQ0NDI3NF5BMl5BanBnXkFtZTcwNDI0MjEzMw@@._V1_SX300.jpg",
                },
                new Models.SimpleMovieDetails(){
                    Id = 1,
                    Name = "The Pursuit of Happiness",
                    Year = "2006",
                    Category = "Biography, Drama",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTQ5NjQ0NDI3NF5BMl5BanBnXkFtZTcwNDI0MjEzMw@@._V1_SX300.jpg",
                },
                new Models.SimpleMovieDetails(){
                    Id = 1,
                    Name = "The Pursuit of Happiness",
                    Year = "2006",
                    Category = "Biography, Drama",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTQ5NjQ0NDI3NF5BMl5BanBnXkFtZTcwNDI0MjEzMw@@._V1_SX300.jpg",
                },
                new Models.SimpleMovieDetails(){
                    Id = 1,
                    Name = "The Pursuit of Happiness",
                    Year = "2006",
                    Category = "Biography, Drama",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTQ5NjQ0NDI3NF5BMl5BanBnXkFtZTcwNDI0MjEzMw@@._V1_SX300.jpg",
                },
                new Models.SimpleMovieDetails(){
                    Id = 1,
                    Name = "The Pursuit of Happiness",
                    Year = "2006",
                    Category = "Biography, Drama",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTQ5NjQ0NDI3NF5BMl5BanBnXkFtZTcwNDI0MjEzMw@@._V1_SX300.jpg",
                },
                new Models.SimpleMovieDetails(){
                    Id = 1,
                    Name = "The Pursuit of Happiness",
                    Year = "2006",
                    Category = "Biography, Drama",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTQ5NjQ0NDI3NF5BMl5BanBnXkFtZTcwNDI0MjEzMw@@._V1_SX300.jpg",
                },
                new Models.SimpleMovieDetails(){
                    Id = 1,
                    Name = "The Pursuit of Happiness",
                    Year = "2006",
                    Category = "Biography, Drama",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTQ5NjQ0NDI3NF5BMl5BanBnXkFtZTcwNDI0MjEzMw@@._V1_SX300.jpg",
                },
                new Models.SimpleMovieDetails(){
                    Id = 1,
                    Name = "The Pursuit of Happiness",
                    Year = "2006",
                    Category = "Biography, Drama",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTQ5NjQ0NDI3NF5BMl5BanBnXkFtZTcwNDI0MjEzMw@@._V1_SX300.jpg",
                },
                new Models.SimpleMovieDetails(){
                    Id = 1,
                    Name = "The Pursuit of Happiness",
                    Year = "2006",
                    Category = "Biography, Drama",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTQ5NjQ0NDI3NF5BMl5BanBnXkFtZTcwNDI0MjEzMw@@._V1_SX300.jpg",
                },
                new Models.SimpleMovieDetails(){
                    Id = 1,
                    Name = "The Pursuit of Happiness",
                    Year = "2006",
                    Category = "Biography, Drama",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTQ5NjQ0NDI3NF5BMl5BanBnXkFtZTcwNDI0MjEzMw@@._V1_SX300.jpg",
                },
                new Models.SimpleMovieDetails(){
                    Id = 1,
                    Name = "The Pursuit of Happiness",
                    Year = "2006",
                    Category = "Biography, Drama",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTQ5NjQ0NDI3NF5BMl5BanBnXkFtZTcwNDI0MjEzMw@@._V1_SX300.jpg",
                },
                new Models.SimpleMovieDetails(){
                    Id = 1,
                    Name = "The Pursuit of Happiness",
                    Year = "2006",
                    Category = "Biography, Drama",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTQ5NjQ0NDI3NF5BMl5BanBnXkFtZTcwNDI0MjEzMw@@._V1_SX300.jpg",
                },
                new Models.SimpleMovieDetails(){
                    Id = 1,
                    Name = "The Pursuit of Happiness",
                    Year = "2006",
                    Category = "Biography, Drama",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTQ5NjQ0NDI3NF5BMl5BanBnXkFtZTcwNDI0MjEzMw@@._V1_SX300.jpg",
                },
            };

            return movies;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Collection(int Id, int? Page, int? Method)
        {

            var movies = GetMovieList();
            var pageC = (int)(Math.Ceiling(((double)(movies.Count)) / 15));

            if (Page == null || Page == 0)
            {
                Page = 1;
            }

            Models.CategoryModel catmodel;

            if (Page <= pageC)
            {
                var remaining = (movies.Count) - (15 * (((int)Page) - 1));

                catmodel = new Models.CategoryModel()
                {
                    MovieList = movies.GetRange((int)Page, Math.Min(15, remaining)),
                    PageCount = pageC,
                };
            }
            else
            {
                return new EmptyResult();
            }

            if (Page > 0)
            {
                catmodel.CurrentPage = Page;
            }

            ViewBag.CollectionId = Id;
            if (Method == 0 || Method == null)
            {
                return View(catmodel);
            }
            else
            {
                return PartialView("_CollectionContentPartial", catmodel);
            }

        }

        

        [HttpGet]
        [AllowAnonymous]
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

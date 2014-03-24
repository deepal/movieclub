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
            return View();
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
            
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            var query = db.DBMovieToCategory.Where(ent => ent.CategoryId == Id).Join(db.DBMovies,
                r => r.MovieId,
                l => l.Id,
                (r, l) => new
                {
                    Id = l.Id,
                    MovieName = l.Name,
                    Year = l.Year,
                    Category = l.Genre,
                    PosterURL = l.PosterURL,
                });

            List<SimpleMovieDetails> movielist = new List<SimpleMovieDetails>();

            foreach (var movie in query)
            {
                SimpleMovieDetails item =
                    new Models.SimpleMovieDetails()
                    {
                        Id = movie.Id,
                        Name = movie.MovieName,
                        Year = movie.Year,
                        Category = movie.Category,
                        PosterURL = movie.PosterURL,
                    };
                movielist.Add(item);
            }

            var movies = movielist;

            var pageC = (int)(Math.Ceiling(((double)(movies.Count)) / 15));

            if (Page == null || Page == 0)
            {
                Page = 1;
            }

            Models.CategoryModel catmodel;
            int remaining;
            if (Page <= pageC)
            {
                remaining = (movies.Count) - (15 * (((int)Page) - 1));

                var currentindex = (((int)Page) - 1) * 15;
                var currentpagemovies = movies.GetRange(currentindex, Math.Min(15, remaining));

                catmodel = new Models.CategoryModel()
                {
                    MovieList = currentpagemovies,
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
            ViewBag.Remaining = remaining;
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
        public ActionResult MovieDetails(int Id)
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            MovieDB.DBMovie dbmovieitem = db.DBMovies.First(mv => mv.Id == Id);

            return View(new MovieDetails() {
                Actors = dbmovieitem.Actors,
                Awards = dbmovieitem.Awards,
                Country = dbmovieitem.Country,
                Director = dbmovieitem.Director,
                Genre = dbmovieitem.Genre,
                Id = dbmovieitem.Id,
                ImdbId = dbmovieitem.ImdbId,
                ImdbRatings = (float)dbmovieitem.ImdbRatings,
                ImdbVotes = dbmovieitem.ImdbVotes,
                Language = dbmovieitem.Language,
                MovieClubRatings = (float)dbmovieitem.MovieClubRatings,
                MovieClubRentCount = dbmovieitem.MovieClubRentCount,
                Name = dbmovieitem.Name,
                PlotFull = dbmovieitem.PlotFull,
                PlotShort = dbmovieitem.PlotShort,
                PosterURL = dbmovieitem.PosterURL,
                ReleaseDate = dbmovieitem.ReleaseDate,
                Runtime = dbmovieitem.Runtime,
                Writer = dbmovieitem.Writer,
                Year = dbmovieitem.Year
            });
        }

        [AllowAnonymous]
        public ActionResult Sidebar()
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            var Categories = db.DBCategories.ToList<MovieDB.DBCategory>();
            Categories.Sort((c1, c2) => c1.CategoryName.CompareTo(c2.CategoryName));
            Models.SidebarCategoriesModel sidebardata = new Models.SidebarCategoriesModel();
            sidebardata.categorylist = new List<Category>();

            foreach (var category in Categories)
            {
                var taggedCount = db.DBMovieToCategory.Count(m => m.CategoryId == category.CategoryId);

                sidebardata.categorylist.Add(new Models.Category() { 
                    Id = category.CategoryId,
                    Name = category.CategoryName,
                    TaggedMovieCount = taggedCount
                });

            }

            return PartialView("_Sidebar", sidebardata);
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult Search(string q)
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            //var query = formdata["search-text"];
            var resultsquery = db.DBMovies.Where(m => ((m.Name).ToLower()).Contains(q.ToLower()));
            var resultslist = new Models.SearchResults();

            foreach (var item in resultsquery)
            {
                resultslist.AddResult(new MovieResult(){ 
                    Id = item.Id,
                    Name = item.Name,
                    PlotShort = item.PlotShort,
                    PosterURL = item.PosterURL,
                    Genre = item.Genre,
                    MovieClubRatings = (float)item.MovieClubRatings,
                    ImdbRatings = (float)item.ImdbRatings,
                    Year = item.Year,
                    Actors = item.Actors
                });
                
            }
            ViewBag.SearchQuery = q;
            return View(resultslist);
        }

        
    }
}

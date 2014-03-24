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

        public const int LATEST = 1;
        public const int TOP_IMDB = 2;
        public const int TOP_MOVIECLUB = 3;
        public const int MOST_VIEWED = 4;

        // GET: /MovieClub/
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
        public ActionResult Collection(int Id, int? Page, int? Method, int? list)
        {
            
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            List<SimpleMovieDetails> movielist = new List<SimpleMovieDetails>();

            if (list == null)
            {
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
                    ImdbRating = l.ImdbRatings,
                    ViewsCount = l.Views,
                    MovieClubRating = l.MovieClubRatings,
                    MovieClubRentCount = l.MovieClubRentCount
                });

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
                            ImdbRating = (float)movie.ImdbRating,
                            MovieClubRating = (float)movie.MovieClubRating,
                            MovieClubRentCount = movie.MovieClubRentCount,
                            ViewsCount = movie.ViewsCount
                        };
                    movielist.Add(item);
                }
            }
            else
            {
                switch (list)
                {
                    case LATEST:
                        var querylatest = db.DBMovies.ToList();
                        querylatest.Sort((x, y) => ((DateTime)y.AddedDate).CompareTo((DateTime)x.AddedDate));
                        foreach (var movie in querylatest)
                        {
                            SimpleMovieDetails item =
                                new Models.SimpleMovieDetails()
                                {
                                    Id = movie.Id,
                                    Name = movie.Name,
                                    Year = movie.Year,
                                    Category = movie.Genre,
                                    PosterURL = movie.PosterURL,
                                    ImdbRating = (float)movie.ImdbRatings,
                                    ImdbId = movie.ImdbId,
                                    MovieClubRating = (float)movie.MovieClubRatings,
                                    MovieClubRentCount = movie.MovieClubRentCount,
                                    ViewsCount = movie.Views
                                };
                            movielist.Add(item);
                        }
                        break;

                    case TOP_IMDB:
                        var queryImdb = db.DBMovies.ToList();
                        queryImdb.Sort((x, y) => (y.ImdbRatings).CompareTo(x.ImdbRatings));
                        foreach (var movie in queryImdb)
                        {
                            SimpleMovieDetails item =
                                new Models.SimpleMovieDetails()
                                {
                                    Id = movie.Id,
                                    Name = movie.Name,
                                    Year = movie.Year,
                                    Category = movie.Genre,
                                    PosterURL = movie.PosterURL,
                                    ImdbRating = (float)movie.ImdbRatings,
                                    ImdbId = movie.ImdbId,
                                    MovieClubRating = (float)movie.MovieClubRatings,
                                    MovieClubRentCount = movie.MovieClubRentCount,
                                    ViewsCount = movie.Views
                                };
                            movielist.Add(item);
                        }
                        break;


                    case TOP_MOVIECLUB:
                        var querymovieclub = db.DBMovies.ToList();
                        querymovieclub.Sort((x, y) => (y.MovieClubRatings).CompareTo(x.MovieClubRatings));
                        foreach (var movie in querymovieclub)
                        {
                            SimpleMovieDetails item =
                                new Models.SimpleMovieDetails()
                                {
                                    Id = movie.Id,
                                    Name = movie.Name,
                                    Year = movie.Year,
                                    Category = movie.Genre,
                                    PosterURL = movie.PosterURL,
                                    ImdbRating = (float)movie.ImdbRatings,
                                    ImdbId = movie.ImdbId,
                                    MovieClubRating = (float)movie.MovieClubRatings,
                                    MovieClubRentCount = movie.MovieClubRentCount,
                                    ViewsCount = movie.Views
                                };
                            movielist.Add(item);
                        }
                        break;


                    case MOST_VIEWED:
                        var queryview = db.DBMovies.ToList();
                        queryview.Sort((x, y) => (y.Views).CompareTo(x.Views));
                        foreach (var movie in queryview)
                        {
                            SimpleMovieDetails item =
                                new Models.SimpleMovieDetails()
                                {
                                    Id = movie.Id,
                                    Name = movie.Name,
                                    Year = movie.Year,
                                    Category = movie.Genre,
                                    PosterURL = movie.PosterURL,
                                    ImdbRating = (float)movie.ImdbRatings,
                                    ImdbId = movie.ImdbId,
                                    MovieClubRating = (float)movie.MovieClubRatings,
                                    MovieClubRentCount = movie.MovieClubRentCount,
                                    ViewsCount = movie.Views
                                };
                            movielist.Add(item);
                        }
                        break;

                    default:
                        //show error
                        break;
                }
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
            catmodel.list = list;
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

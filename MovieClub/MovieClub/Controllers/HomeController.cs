using MovieClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieClub.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var movies = new List<FeaturedMovieDetails>()
            {
                new Models.FeaturedMovieDetails(){
                    Id = 1,
                    Name = "Prison Break",
                    PlotShort = "Structural Engineer Michael Scofield turns himself in to the Fox River Penitentiary in order to break out his brother Lincoln Burrows, who is on death row for the murder of the Vice President's brother. But Lincoln was set up by some of the Company (an agency formed by corrupt government officials) guys, headed by General Jonathan Krantz. Michael breaks out from Fox River with his brother Lincoln and other convicts.",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTg3NTkwNzAxOF5BMl5BanBnXkFtZTcwMjM1NjI5MQ@@._V1_SX300.jpg",
                    ImdbRatings = 8.6,
                    MovieClubRatings = 9.1,
                },
                new Models.FeaturedMovieDetails(){
                    Id = 2,
                    Name = "Up",
                    PlotShort = "A young Carl Fredrickson meets a young adventure-spirited girl named Ellie. They both dream of going to a lost land in South America. 70 years later, Ellie has died. Carl remembers the promise he made to her. Then, when he inadvertently hits a construction worker, he is forced to go to a retirement home. But before they can take him, he and his house fly away. However, he has a stowaway aboard: an 8-year-old boy named Russell, who's trying to get an Assisting the Elderly badge. Together, they embark on an adventure, where they encounter talking dogs, an evil villain and a rare bird named Kevin.",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTk3NDE2NzI4NF5BMl5BanBnXkFtZTgwNzE1MzEyMTE@._V1_SX300.jpg",
                    ImdbRatings = 8.3,
                    MovieClubRatings = 8.1,
                },
                new Models.FeaturedMovieDetails(){
                    Id = 2,
                    Name = "Up",
                    PlotShort = "A young Carl Fredrickson meets a young adventure-spirited girl named Ellie. They both dream of going to a lost land in South America. 70 years later, Ellie has died. Carl remembers the promise he made to her. Then, when he inadvertently hits a construction worker, he is forced to go to a retirement home. But before they can take him, he and his house fly away. However, he has a stowaway aboard: an 8-year-old boy named Russell, who's trying to get an Assisting the Elderly badge. Together, they embark on an adventure, where they encounter talking dogs, an evil villain and a rare bird named Kevin.",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTk3NDE2NzI4NF5BMl5BanBnXkFtZTgwNzE1MzEyMTE@._V1_SX300.jpg",
                    ImdbRatings = 8.3,
                    MovieClubRatings = 8.1,
                },
                new Models.FeaturedMovieDetails(){
                    Id = 2,
                    Name = "Up",
                    PlotShort = "A young Carl Fredrickson meets a young adventure-spirited girl named Ellie. They both dream of going to a lost land in South America. 70 years later, Ellie has died. Carl remembers the promise he made to her. Then, when he inadvertently hits a construction worker, he is forced to go to a retirement home. But before they can take him, he and his house fly away. However, he has a stowaway aboard: an 8-year-old boy named Russell, who's trying to get an Assisting the Elderly badge. Together, they embark on an adventure, where they encounter talking dogs, an evil villain and a rare bird named Kevin.",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTk3NDE2NzI4NF5BMl5BanBnXkFtZTgwNzE1MzEyMTE@._V1_SX300.jpg",
                    ImdbRatings = 8.3,
                    MovieClubRatings = 8.1,
                },
                new Models.FeaturedMovieDetails(){
                    Id = 2,
                    Name = "Up",
                    PlotShort = "A young Carl Fredrickson meets a young adventure-spirited girl named Ellie. They both dream of going to a lost land in South America. 70 years later, Ellie has died. Carl remembers the promise he made to her. Then, when he inadvertently hits a construction worker, he is forced to go to a retirement home. But before they can take him, he and his house fly away. However, he has a stowaway aboard: an 8-year-old boy named Russell, who's trying to get an Assisting the Elderly badge. Together, they embark on an adventure, where they encounter talking dogs, an evil villain and a rare bird named Kevin.",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTk3NDE2NzI4NF5BMl5BanBnXkFtZTgwNzE1MzEyMTE@._V1_SX300.jpg",
                    ImdbRatings = 8.3,
                    MovieClubRatings = 8.1,
                },
                new Models.FeaturedMovieDetails(){
                    Id = 2,
                    Name = "Up",
                    PlotShort = "A young Carl Fredrickson meets a young adventure-spirited girl named Ellie. They both dream of going to a lost land in South America. 70 years later, Ellie has died. Carl remembers the promise he made to her. Then, when he inadvertently hits a construction worker, he is forced to go to a retirement home. But before they can take him, he and his house fly away. However, he has a stowaway aboard: an 8-year-old boy named Russell, who's trying to get an Assisting the Elderly badge. Together, they embark on an adventure, where they encounter talking dogs, an evil villain and a rare bird named Kevin.",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTk3NDE2NzI4NF5BMl5BanBnXkFtZTgwNzE1MzEyMTE@._V1_SX300.jpg",
                    ImdbRatings = 8.3,
                    MovieClubRatings = 8.1,
                },
                new Models.FeaturedMovieDetails(){
                    Id = 2,
                    Name = "Up",
                    PlotShort = "A young Carl Fredrickson meets a young adventure-spirited girl named Ellie. They both dream of going to a lost land in South America. 70 years later, Ellie has died. Carl remembers the promise he made to her. Then, when he inadvertently hits a construction worker, he is forced to go to a retirement home. But before they can take him, he and his house fly away. However, he has a stowaway aboard: an 8-year-old boy named Russell, who's trying to get an Assisting the Elderly badge. Together, they embark on an adventure, where they encounter talking dogs, an evil villain and a rare bird named Kevin.",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTk3NDE2NzI4NF5BMl5BanBnXkFtZTgwNzE1MzEyMTE@._V1_SX300.jpg",
                    ImdbRatings = 8.3,
                    MovieClubRatings = 8.1,
                },
                new Models.FeaturedMovieDetails(){
                    Id = 1,
                    Name = "Pacific Rim",
                    PlotShort = "When monstrous creatures, known as Kaiju, started rising from the sea, a war began that would take millions of lives and consume humanity's resources for years on end. To combat the giant Kaiju, a special type of weapon was devised: massive robots, called Jaegers, which are controlled simultaneously by two pilots whose minds are locked in a neural bridge. But even the Jaegers are proving nearly defenseless in the face of the relentless Kaiju. On the verge of defeat, the forces defending mankind have no choice but to turn to two unlikely heroes - a washed up former pilot (Charlie Hunnam) and an untested trainee (Rinko Kikuchi) - who are teamed to drive a legendary but seemingly obsolete Jaeger from the past. Together, they stand as mankind's last hope against the mounting apocalypse.",
                    PosterURL = "http://ia.media-imdb.com/images/M/MV5BMTY3MTI5NjQ4Nl5BMl5BanBnXkFtZTcwOTU1OTU0OQ@@._V1_SX300.jpg",
                    ImdbRatings = 7.1,
                    MovieClubRatings = 6.8,
                },
            };
            return View(movies);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}

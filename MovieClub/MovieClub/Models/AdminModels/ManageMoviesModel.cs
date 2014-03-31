using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Models.AdminModels
{
    public class ManageMoviesModel
    {
        public bool Selected { get; set; }
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public string Genre { get; set; }
        public bool Featured { get; set; }
        public int ReservationsCount { get; set; }
        public int RentsCount { get; set; }
        public int ViewsCount { get; set; }
        public bool AvailableToRent { get; set; }
        public DateTime AddedDate { get; set; }
        public int Favorites { get; set; }
        public float PearsonRating { get; set; }
        public bool Updated { get; set; }
    }
}
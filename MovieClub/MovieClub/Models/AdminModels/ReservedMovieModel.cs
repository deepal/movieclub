using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Models.AdminModels
{
    public class ReservedMovieModel
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public int ReservationsCount { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public DateTime DateReserved { get; set; }
    }
}
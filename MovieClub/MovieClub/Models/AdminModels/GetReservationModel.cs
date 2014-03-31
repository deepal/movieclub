using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Models.AdminModels
{
    public class GetReservationModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Models
{
    public class ReviewModel
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int MovieId { get; set; }
        public string Comment { get; set; }
        public int Moderated { get; set; }
        public DateTime Timestamp { get; set; }
        public bool DeleteEnabled { get; set; }

    }
}
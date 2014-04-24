using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Models.AdminModels
{
    public class PendingReviewsModel
    {
        public int ReviewId{ get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public string Comment { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
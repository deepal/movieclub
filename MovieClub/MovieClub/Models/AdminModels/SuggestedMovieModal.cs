using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Models.AdminModels
{
    public class SuggestedMovieModal
    {
        public int SuggestionId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string MovieName { get; set; }
        public string Info { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Models.MyAccountModels
{
    public class RecommendationModel
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public string RecommendedBy { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
    }
}
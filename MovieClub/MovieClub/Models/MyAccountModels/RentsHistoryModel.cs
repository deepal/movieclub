using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Models.MyAccountModels
{
    public class RentsHistoryModel
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public DateTime DateBorrowed { get; set; }
        public DateTime? DateReturned { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Models.MyAccountModels
{
    public class CurrentRentModel
    {
        public int MovieId { get; set; }
        public DateTime DateBorrowed { get; set; }
        public DateTime DueDate { get; set; }
        public string MovieName { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Models.MyAccountModels
{
    public class ReservationModel
    {
        public int MovieId { get; set; }
        public DateTime ReservedOn { get; set; }
        public string MovieName { get; set; }
    }
}
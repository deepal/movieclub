using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Models.AdminModels
{
    public class DetailedPaymentModel
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public float Charge { get; set; }
        public float Fine { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Models.AdminModels
{
    public class PaymentHistoryModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public float PayAmount { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
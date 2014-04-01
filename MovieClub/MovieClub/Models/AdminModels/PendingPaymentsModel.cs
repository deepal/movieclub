using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Models.AdminModels
{
    public class PendingPaymentsModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string UserEmail { get; set; }
        public float TotalFines { get; set; }
        public float TotalCharge { get; set; }
        public float TotalPaymentDue { get; set; }
    }
}
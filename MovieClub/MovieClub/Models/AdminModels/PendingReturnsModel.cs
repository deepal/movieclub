using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Models.AdminModels
{
    public class PendingReturnsModel
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime DateTaken { get; set; }
        public DateTime DueDate { get; set; }
    }
}
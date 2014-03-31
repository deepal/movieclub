using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Models.AdminModels
{
    public class UserReservedModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public DateTime ReservedOn { get; set; }
    }
}
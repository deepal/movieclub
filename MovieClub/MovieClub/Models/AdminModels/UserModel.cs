using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Models.AdminModels
{
    public class UserModel
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public int EmployeeId { get; set; }
        public string Email { get; set; }
    }
}
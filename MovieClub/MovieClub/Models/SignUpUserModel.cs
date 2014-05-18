using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieClub.Models
{
    public class SignUpUserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RetypePassword { get; set; }
        public int EmployeeId { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
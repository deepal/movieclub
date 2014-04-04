using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Models.AdminModels
{
    public class IssueUnreservedModel
    {
        public List<SimpleMovieDetails> movies { get; set; }
        public List<UserDetails> users { get; set; }
    }
}
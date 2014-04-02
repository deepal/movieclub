using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Models.MyAccountModels
{
    public class InboxMessageModel
    {
        public int MessageId { get; set; }
        public int Status { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
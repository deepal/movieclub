using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Models
{
    public class SidebarCategoriesModel
    {
        public List<Category> categorylist { get; set; }
        public List<MovieCollection> collectionslist { get; set; }
    }

}
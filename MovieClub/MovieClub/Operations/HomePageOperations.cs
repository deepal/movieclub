using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Operations
{
    public static class HomePageOperations
    {
        public static List<int> GetTopCategories()
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            var topviewedmovies = db.DBMovies.ToList();
            topviewedmovies.Sort((x, y) => x.Views.CompareTo(y.Views));

            var topmovies = topviewedmovies.GetRange(0, Math.Min(topviewedmovies.Count, 50));

            List<string> toptags = new List<string>();

            foreach (var item in topmovies)
            {
                toptags.AddRange(stringToTagList(item.Genre,','));
            }
            var grp = toptags.GroupBy(t => t).Select(grps => new {
                Tag = grps.Key,
                Count = grps.Count()
            }).ToList();
            grp.Sort((x,y)=>y.Count.CompareTo(x.Count));
            grp = grp.GetRange(0, 3);

            List<int> catlist = new List<int>();

            foreach (var item in grp)
            {
                catlist.Add(db.DBCategories.Where(c => c.CategoryName.Contains(item.Tag)).First().CategoryId);
            }

            return catlist;
        }

        public static List<string> stringToTagList(string inputstring, char delimiter)
        {
            List<string> tags = new List<string>();
            string[] items = inputstring.Split(delimiter);
            foreach (var item in items)
            {
                tags.Add(item.Trim());
            }
            return tags;
        }
    }
}
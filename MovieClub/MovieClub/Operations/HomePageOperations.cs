using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Operations
{
    public class HomePageOperations
    {
        public List<int> GetUserFavoriteCategories(int userid)
        {
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            var userfavren = db.DBRents.Where(rn => rn.UserId == userid);
            var userfavres = db.DBReservations.Where(rs => rs.UserId == userid);
            var userfavfav = db.DBFavorites.Where(fv => fv.UserID == userid);

            return null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Operations
{
    public class AccountOperations
    {
        public static bool checkUsernameAvailability(string username){
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            if(db.DBUsers.Count(u=>u.UserName==username)==0){
                return true;
            }
            return false;
        }

        public static bool checkEmailAvailability(string email){
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            if(db.DBUsers.Count(u=>u.Email==email)==0){
                return true;
            }
            return false;
        }

        public static bool checkEmployeeIDAvailability(int empID){
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            if(db.DBUsers.Count(u=>u.EmpId==empID)==0){
                return true;
            }
            return false;
        }

    }
}
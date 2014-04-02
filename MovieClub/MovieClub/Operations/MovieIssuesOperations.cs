using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClub.Operations
{
    public static class MovieIssuesOperations
    {
        public static DateTime GetReturnDate(DateTime issuedDate)
        {
            return issuedDate.AddDays(double.Parse(System.Configuration.ConfigurationManager.AppSettings["DefaultRentDuration"]));
        }

        public static double CalculatedFine(DateTime issuedDate, DateTime returnedDate)
        {
            //change login accordingly
            TimeSpan duration = returnedDate-issuedDate;
            double dayscount = Math.Floor(duration.TotalDays);
            double defaultduration = double.Parse(System.Configuration.ConfigurationManager.AppSettings["DefaultRentDuration"]);
            if(dayscount>defaultduration){
                double fine = (dayscount-defaultduration)*(double.Parse(System.Configuration.ConfigurationManager.AppSettings["DefaultFinePerDay"]));
                return fine;
            }
            else{
                return (double)0;
            }

        }

        public static double CalculateCharge(DateTime issuedDate, DateTime returnedDate)
        {
            double defaultcharge = double.Parse(System.Configuration.ConfigurationManager.AppSettings["DefaultCharge"]);        
            TimeSpan duration = returnedDate-issuedDate;
            double dayscount = Math.Floor(duration.TotalDays);

            return defaultcharge * dayscount;
        }

        public static void UpdateUserCharge(int userid, int movieid, double charge, double fine)
        {
            if (charge > (double)0 || fine > (double)0) 
            {
                MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();

                db.DBPaymentsDues.Add(new MovieDB.DBPaymentsDue()
                {
                    UserId = userid,
                    MovieId = movieid,
                    Charge = charge,
                    Fine = fine,
                    Paid = 0
                });
                db.SaveChanges();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace MovieClub.WebServices
{
    public class ImdbData
    {
        public static string GetData(string moviename)
        {
            string url = "http://www.omdbapi.com/?t=" + HttpUtility.UrlEncode(moviename) + "&r=json&plot=full";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responsestream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responsestream, Encoding.UTF8);
            string jsondata = reader.ReadToEnd();
            return jsondata;
        }
    }
}
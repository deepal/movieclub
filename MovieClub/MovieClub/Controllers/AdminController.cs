﻿using MovieClub.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieClub.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {

        [HttpGet]
        public ActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        [HandleError]
        [ValidateAntiForgeryToken]
        public ActionResult AddMovie(Models.MovieDetails movie, HttpPostedFileBase uploadFile)
        {
            if (ModelState.IsValid)
            {
                /*
                try
                {
                    var db = new MovieDataContext();
                    db.Movies.Add(movie);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
                
                 */

                //upload trailer start
                if (uploadFile.ContentLength > 0)
                {
                    if (Path.GetExtension(uploadFile.FileName) == ".mp4")
                    {
                        string filePath = Path.Combine(HttpContext.Server.MapPath("/Content/multimedia"), Path.GetFileName(movie.ImdbId+".mp4"));
                        uploadFile.SaveAs(filePath);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Trailer should be a MP4 type Video !");
                        return View(movie);
                    }
                }
                //upload trailer end

                return View(new Models.MovieDetails());

            }
            ModelState.AddModelError("", "Error adding movie!");
            return View(movie);
        }

        

    }
}

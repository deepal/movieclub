using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using MovieClub.Models;
using MovieClub.Operations;
using MovieClub.CustomAttributes;

namespace MovieClub.Controllers
{

    
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public string GetSolUserName()
        {
            return "robinhood";
            //return HttpContext.User.Identity.Name;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            if ((string)Session["LoggedIn"] != "true")
            {
                if (UserOperations.IsAuthenticated(GetSolUserName()))
                {
                    System.Web.Security.FormsAuthentication.SetAuthCookie(GetSolUserName(), false);
                    if (UserOperations.IsAdmin())
                    {
                        Session["Admin"] = "true";
                    }
                    else
                    {
                        Session["Admin"] = "false";
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }


        //
        // POST: /Account/LogOff
        [HttpPost]
        [RequireMembership]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session["LoggedIn"] = "false";
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register


        [AllowAnonymous]
        public ActionResult Register()
        {
            var soluname = GetSolUserName();
            ViewBag.SolUserName = soluname;
            MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
            if (db.DBUsers.Where(u => u.UserName == soluname ).Count() != 0)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();

                if (db.DBUsers.Where(u => u.Email.ToLower() == model.Email.ToLower()).Count() != 0)
                {
                    ModelState.AddModelError("", "An account already exists with given email. Check your email address.");
                    return View(model);
                }

                if (db.DBUsers.Where(u => u.EmpId == model.EmployeeId).Count() != 0)
                {
                    ModelState.AddModelError("","Employee ID you entered is currently assigned to an account!");
                    return View();
                }


                db.DBUsers.Add(new MovieDB.DBUser()
                {
                    UserName = GetSolUserName(),//System.Web.HttpContext.Current.User.Identity.Name,
                    EmpId = model.EmployeeId,
                    Email = model.Email,
                    PhotoURL = "http://localhost/none.jpg",
                    AccountCreatedDate = DateTime.Now
                });
                db.SaveChanges();
                return RedirectToAction("Login", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/Disassociate

        
        /*
         #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        #endregion 
         
         */
    }
}

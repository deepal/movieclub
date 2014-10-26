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
using System.Text;
using System.Security.Cryptography;

namespace MovieClub.Controllers
{

    
    public class AccountController : Controller
    {
        
        [AllowAnonymous]
        public string GetSolUserName()
        {
            return "deepal";
            //return HttpContext.User.Identity.Name;
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            if (!Request.IsAuthenticated)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult SignUp(Models.SignUpUserModel newuser)
        {
            if (!Request.IsAuthenticated)
            {
                MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
                
                if (ModelState.IsValid)
                {

                    if (newuser.Password != newuser.RetypePassword)
                    {
                        ModelState.AddModelError("", "Passwords should match!");
                        return View();
                    }

                    if (!Operations.AccountOperations.checkUsernameAvailability(newuser.UserName))
                    {
                        ModelState.AddModelError("", "Username not available!");
                        return View();
                    }

                    if (!Operations.AccountOperations.checkEmailAvailability(newuser.Email))
                    {
                        ModelState.AddModelError("", "Email not available!");
                        return View();
                    }

                    if (!Operations.AccountOperations.checkEmployeeIDAvailability(newuser.EmployeeId))
                    {
                        ModelState.AddModelError("", "Employee ID not available!");
                        return View();
                    }

                    string salt = DateTime.Now.ToString("yyyymmddHHmmssffff");
                    byte[] pass = Encoding.ASCII.GetBytes(newuser.Password + salt);
                    byte[] hash;
                    string hashString;

                    using (MD5 md5 = MD5.Create())
                    {
                        hash = md5.ComputeHash(pass);
                        StringBuilder sBuilder = new StringBuilder();

                        for (int i = 0; i < hash.Length; i++)
                        {
                            sBuilder.Append(hash[i].ToString("x2"));
                        }

                        hashString = sBuilder.ToString();
                    }

                    db.DBUsers.Add(new MovieDB.DBUser()
                    {
                        UserName = newuser.UserName,
                        Email = newuser.Email,
                        EmpId = newuser.EmployeeId,
                        AccountCreatedDate = DateTime.Now,
                        Password = hashString,
                        pSalt = salt
                    });

                    db.SaveChanges();
                }

                ViewBag.Message = "Account created successfully! Login to continue.";

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            if (!Request.IsAuthenticated)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Models.LoginModel login)
        {
            if (!Request.IsAuthenticated)
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("","Could not login!");
                    return View();
                }

                MovieDB.MovieClubDBE db = new MovieDB.MovieClubDBE();
                var user = db.DBUsers.Where(us => us.UserName == login.Username).ToList();

                if (user.Count() == 0)
                {
                    ModelState.AddModelError("","Username or Password incorrect!");
                    return View();
                }
                else
                {
                    if (checkLogin(user, login.Username, login.Password))
                    {
                        System.Web.Security.FormsAuthentication.SetAuthCookie(login.Username, login.RememberMe);
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

            return RedirectToAction("Index", "Home");
        }

        public bool checkLogin(List<MovieDB.DBUser> allUsers, string username, string password)
        {
            var salt = allUsers.First(u => u.UserName == username).pSalt;
            var pass = allUsers.First(u => u.UserName == username).Password;

            byte[] loginPass = Encoding.ASCII.GetBytes(password + salt);
            byte[] hash;
            string hashString;

            using (MD5 md5 = MD5.Create())
            {
                hash = md5.ComputeHash(loginPass);
                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < hash.Length; i++)
                {
                    sBuilder.Append(hash[i].ToString("x2"));
                }

                hashString = sBuilder.ToString();
            }

            if (hashString == pass)
            {
                return true;
            }

            return false;
        }


        [HttpGet]
        [RequireMembership]
        public ActionResult LogOff()
        {
            if (Request.IsAuthenticated)
            {
                Session["LoggedIn"] = "false";
                System.Web.Security.FormsAuthentication.SignOut();
            }

            return RedirectToAction("Index", "Home");
        }

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

    }
}

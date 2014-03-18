using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieClub.Controllers
{
    [Authorize]
    public class UserController : Controller
    {

        public ActionResult MyAccount()
        {

            return View();
        }
    }
}

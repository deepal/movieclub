using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieClub.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(string query)
        {
            return View();
        }

    }
}

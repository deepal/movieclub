using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieClub.Operations;
using System.Web.Routing;

namespace MovieClub.CustomAttributes
{
    public class RequireMembershipAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string loggedIn = (string)filterContext.HttpContext.Session["LoggedIn"];
            if (loggedIn != "true")
            {
                filterContext.Result = new RedirectToRouteResult("Registration", new RouteValueDictionary(new { action="Register", controller="Account" }));
            }
        }
    }
}
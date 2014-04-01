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
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //string loggedIn = (string)filterContext.HttpContext.Session["LoggedIn"];
            //string loggedIn = HttpContext.Current.User.Identity.Name;
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {                
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary { 
                        { "controller", "Account" }, 
                        { "action", "Register" } }
                    );
            }
        }
    }
}
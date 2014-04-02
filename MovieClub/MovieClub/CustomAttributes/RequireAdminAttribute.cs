using MovieClub.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieClub.CustomAttributes
{
    public class RequireAdminAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!UserOperations.IsAdmin())
            {
                filterContext.Result = new Http403Result();
            }
        }
    }

    internal class Http403Result : ActionResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            // Set the response code to 403.
            context.HttpContext.Response.StatusCode = 403;
        }
    }

}
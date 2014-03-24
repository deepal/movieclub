using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MovieClub
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "MovieDetails",
                url: "{controller}/MovieDetails/{*Id}",
                defaults: new { controller = "Home", action = "MovieDetails" }
            );

            routes.MapRoute(
                name: "Search",
                url: "{controller}/Search/",
                defaults: new { controller = "Content", action = "Search" }
            );

            routes.MapRoute(
                name: "Collection",
                url: "{controller}/Collection/{Id}",
                defaults: new { controller = "Content", action = "Collection"}
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
﻿using System.Web.Mvc;
using System.Web.Routing;

namespace StrategyCorps.CodeSample.WebApi
{
    /// <summary>
    /// RouteConfig
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// RegisterRoutes
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

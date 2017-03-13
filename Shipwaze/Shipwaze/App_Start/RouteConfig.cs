using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

public class RouteConfig {
    public static void RegisterRoutes(RouteCollection routes) {
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

        routes.MapRoute(
            "Home",
            "{action}",
            new { controller = "Home", action = "Index" },
            new { IsRootAction = new IsRootActionConstraint() }  // Route Constraint
        );

        routes.MapRoute(
            name: "Generic",
            url: "{controller}/{action}/{id}",
            defaults: new { id = UrlParameter.Optional }
        );

    }
}


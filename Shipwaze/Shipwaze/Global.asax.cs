using System;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

public class MvcApplication : System.Web.HttpApplication {

    protected void Application_Start() {

        AreaRegistration.RegisterAllAreas();

        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        BundleConfig.RegisterBundles(BundleTable.Bundles);

        BuildManager.GetReferencedAssemblies();

        ViewEngines.Engines.Clear();
        ViewEngines.Engines.Add(new CustomViewEngine());
    }

    /// <summary>
    /// Provide a custom path/locations for views
    /// </summary>
    public class CustomViewEngine : RazorViewEngine {

        public CustomViewEngine() {
            string[] locations = new string[] {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };

            this.ViewLocationFormats = locations;
            this.PartialViewLocationFormats = locations;
            this.MasterLocationFormats = locations;


            string[] areas = new string[] {
                "~/Areas/{2}/Views/{0}.cshtml"
            };

            this.AreaPartialViewLocationFormats = areas;
            this.AreaViewLocationFormats = areas;
            this.AreaMasterLocationFormats = areas;
        }

    }

    protected void Application_Error(object sender, EventArgs e) {

    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using Newtonsoft.Json;

public static class RequireJsHelpers {

    public static MvcHtmlString InitPageMainModule(this HtmlHelper helper) {

        if (!IsAmdEnabled())
        {
           return null;
        }

        var require = new StringBuilder();
        var scriptsPath = "~/scripts/";
        var absolutePath = VirtualPathUtility.ToAbsolute(scriptsPath);

        //Page module Options
        require.AppendLine("<script>");
        require.AppendLine("var require = {");
        require.AppendLine("config: {");
        require.AppendFormat("'{0}':", GetPageModuleID());

        //Get parameters from the controller
        dynamic options = GetModuleOptions();

        require.AppendFormat("{0}", JsonConvert.SerializeObject(options[GetPageModuleID()]));
        require.AppendLine("}");
        require.AppendLine("};");
        require.AppendLine("</script>");

        //Initialize requirejs and the page module
        require.AppendLine("<script src='https://cdnjs.cloudflare.com/ajax/libs/require.js/2.1.18/require.min.js'></script>");
        require.AppendLine("<script>");
        require.AppendFormat("    require([\"{0}main.js?v={1}\"]," + Environment.NewLine, absolutePath, helper.AssemblyVersion());
        require.AppendLine("        function() {");
        require.AppendFormat("            require([\"{0}\", \"domReady!\"]);" + Environment.NewLine, GetPageModuleID());
        require.AppendLine("        });");
        require.AppendLine("</script>");

        return new MvcHtmlString(require.ToString());
    }

    private static string GetPageModuleID() {
        var httpContext = new HttpContextWrapper(System.Web.HttpContext.Current);
        var routeData = System.Web.Routing.RouteTable.Routes.GetRouteData(httpContext);

        var controller = routeData.Values["controller"];
        var action = routeData.Values["action"];

        if (routeData.Values["area"] != null) {
            return string.Format("{0}/{1}", controller, action);
        }

        return string.Format("../{0}/{1}", controller, action).ToLower();
    }

    public static void AddModuleOption(string key, string value) {
        var moduleID = GetPageModuleID();

        AddModuleOption(moduleID, key, value);
    }

    public static void AddModuleOption(string moduleID, string key, string value) {

        dynamic options = GetModuleOptions();

        if (!options.ContainsKey(moduleID)) {
            options.Add(moduleID, new Dictionary<string, string>());
        }

        if (!options[moduleID].ContainsKey(key)) {
            options[moduleID].Add(key, value);
        }
        else {
            options[moduleID][key] = value;
        }
    }

    public static Dictionary<string, Dictionary<string, string>> GetModuleOptions() {

        var moduleID = GetPageModuleID();

        dynamic result = HttpContext.Current.Items["AmdModuleOptions"];

        if (result == null) {
            result = new Dictionary<string, Dictionary<string, string>>();

            var options = new Dictionary<string, string>();
            options.Add("baseUrl", HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + ResolveUrl("~/"));

            result.Add(moduleID, options);

            HttpContext.Current.Items["AmdModuleOptions"] = result;
        }

        return result;
    }

    //Converts Url to usable format
    public static string ResolveUrl(string url) {
        Control resolver = new Control();

        return resolver.ResolveUrl(url);
    }

    public static void EnableAmd()
    {
        HttpContext.Current.Items["AmdEnabled"] = true;
    }

    public static bool IsAmdEnabled()
    {
        return HttpContext.Current.Items["AmdEnabled"] != null;
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

public static class VersionHelper {

    private static string _assemblyVersion = string.Empty;    

    public static string AssemblyVersion(this HtmlHelper helper) {
        if (string.IsNullOrWhiteSpace(_assemblyVersion)) {
           _assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        return _assemblyVersion;
    }

    public static string GetWebFormViewName(this IView view) {
        if (view is WebFormView) {
            string viewUrl = ((WebFormView)view).ViewPath;
            string viewFileName = viewUrl.Substring(viewUrl.LastIndexOf('/'));
            string viewFileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(viewFileName);
            return (viewFileNameWithoutExtension);
        }
        else {
            throw (new InvalidOperationException("This view is not a WebFormView"));
        }
    }
}


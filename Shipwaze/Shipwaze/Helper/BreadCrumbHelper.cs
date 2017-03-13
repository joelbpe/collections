using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

public static class BreadCrumbHelper {

    public static MvcHtmlString BreadCrumbs(this HtmlHelper html) {
        var breadCrumbs = html.ViewContext.ViewBag.Crumbs as List<BreadCrumb>;

        if (breadCrumbs == null) {
            breadCrumbs = new List<BreadCrumb>();
            breadCrumbs.Add(new BreadCrumb { Title = "<i class='fa fa-home'></i>&nbsp;home", Url = "/home/index" });
        }

        var crumbs = new StringBuilder();

        for (int i = 0; i < breadCrumbs.Count; i++) {
            var crumb = breadCrumbs[i];

            if (string.IsNullOrEmpty(crumb.Url)) {
                crumbs.Append(" " + crumb.Title);
            }
            else {
                crumbs.AppendFormat("<a href='{0}'>{1}</a>", crumb.Url, crumb.Title);
            }

            if (i < breadCrumbs.Count - 1) {
                crumbs.Append("&nbsp;<i class='fa fa-angle-right'></i>&nbsp;");
            }
        }

        return MvcHtmlString.Create(crumbs.ToString());
    }

}
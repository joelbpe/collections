using System;
using System.Collections.Generic;
using System.Web.Mvc;

public class HomeController : Controller {
    public HomeController() {
        RequireJsHelpers.EnableAmd();
    }

    public ActionResult Index() {
        ViewBag.Crumbs = new List<BreadCrumb>();
        ViewBag.Crumbs.Add(new BreadCrumb { Title = "<i class='fa fa-home'></i>&nbsp;home" });

        ViewBag.ContentTitle = "Dashboard";

        try {
            object i = "0AS";

            var x = (int)i;
        } catch (Exception ex) {
        }

        return View();
    }

    public ActionResult Contact() {
        return View();
    }

    public ActionResult About() {
        return View();
    }

    public ActionResult Help() {
        return View();
    }

}

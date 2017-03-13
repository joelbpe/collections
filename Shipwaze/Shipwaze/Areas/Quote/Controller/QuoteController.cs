using System.Web.Mvc;

namespace Areas.Quote {

    public class QuoteController : Controller {

        public QuoteController()
        {
            RequireJsHelpers.EnableAmd();
        }

        public ActionResult Quote() {
            ViewBag.ContentTitle = "Quote";

            return View();
        }

    }
}
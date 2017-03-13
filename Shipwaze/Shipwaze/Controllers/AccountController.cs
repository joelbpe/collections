using System.Web.Mvc;

[Authorize]
public class AccountController : Controller {
    [AllowAnonymous]
    public ActionResult Login(string returnUrl) {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public ActionResult Login(LoginModel model, string returnUrl) {
        return View(model);
    }

    [AllowAnonymous]
    public ActionResult Register() {
        return View();
    }

    [AllowAnonymous]
    public ActionResult ForgotPassword() {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public ActionResult Register(RegisterModel model) {
        return View(model);
    }
}
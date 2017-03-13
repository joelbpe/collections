using System.Web.Mvc;

namespace Areas.Quote
{
    public class QuoteAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Quote"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("", "quote", new { area = "quote", controller = "quote", action = "quote" });
        }
    }
}

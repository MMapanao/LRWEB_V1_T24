using System.Web.Mvc;

namespace LRWEB_V1_CLIENT_SIDE_T24.Areas.LRForms
{
    public class EpayFormAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LRForms";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "EpayForm_default",
                "LRForms/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
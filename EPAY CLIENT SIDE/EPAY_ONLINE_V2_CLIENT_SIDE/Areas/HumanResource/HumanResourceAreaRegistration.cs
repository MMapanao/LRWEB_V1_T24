using System.Web.Mvc;

namespace LRWEB_V1_CLIENT_SIDE_T24.Areas.HumanResource
{
    public class HumanResourceAreaRegistration : AreaRegistration 
    {

        public override string AreaName 
        {
            get 
            {
                return "HumanResource";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "HumanResource_default",
                "HumanResource/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
using System.Web;
using System.Web.Mvc;

namespace LRWEB_V1_ADMIN_T24
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

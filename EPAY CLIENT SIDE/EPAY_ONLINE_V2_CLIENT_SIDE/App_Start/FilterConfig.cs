using System.Web;
using System.Web.Mvc;

namespace LRWEB_V1_CLIENT_SIDE_T24
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

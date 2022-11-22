using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LRWEB_V1_CLIENT_SIDE_T24.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Redirect(LRWEB_V1_CLIENT_SIDE_T24.Areas.HumanResource.GlobalVariableClass.FolderName +"/HumanResource/Login/Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return Redirect(LRWEB_V1_CLIENT_SIDE_T24.Areas.HumanResource.GlobalVariableClass.FolderName + "/HumanResource/Login/Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return Redirect(LRWEB_V1_CLIENT_SIDE_T24.Areas.HumanResource.GlobalVariableClass.FolderName + "/HumanResource/Login/Index");
        }
    }
}
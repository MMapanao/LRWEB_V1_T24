using LRWEB_V1_ADMIN_T24.Areas.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LRWEB_V1_ADMIN_T24.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return Redirect(GlobalVariableClass.FolderName+ "/Admin/EnrollClients/Index?content=EnrollClient");
        }
    }
}
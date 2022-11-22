using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using System.Web.Mvc;
using LRWEB_V1_ADMIN_T24.Areas.Admin.Models;

namespace LRWEB_V1_ADMIN_T24.Areas.Admin.Controllers
{
    public class ClientAccessController : Controller
    {
        DateTime dateTime = DateTime.UtcNow.Date;
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMBANKconnection"].ToString());

        // GET: Admin/ClientAccess
        public ActionResult Index()
        {
            if (Session["ticket"] == null || Session["cred"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                string ticket = Session["ticket"].ToString(), cred = Session["cred"].ToString();
                if (GlobalVariableClass.Authenticated(ticket, cred) == false)
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            var obj = conn.Query<_CLIENT_LISTGC>(@"SELECT cl.groupCode, cl.clientName as CompanyName FROM LRWEB_V1_CLIENTS as cl 
            WHERE status='VERIFIED'", new {

            }).ToList();
            List<_CLIENT_LISTGC> result = new List<_CLIENT_LISTGC>();
            if(obj != null)
            {
                foreach(var data in obj)
                {
                    _CLIENT_LISTGC epp = new _CLIENT_LISTGC();
                    epp.groupCode = data.groupCode;
                    epp.CompanyName = data.CompanyName;
                    result.Add(epp);
                }
            }
            return View(result);
        }

        [HttpPost]
        public JsonResult GetDataFromAccess(string GroupCode)
        {


            var obj_hrclient = conn.Query<_EPAY_V2_CLIENT_ACCESS_ID>(@"
            SELECT * FROM LRWEB_V1_CLIENT_ACCESS_ID WHERE groupCode=@GroupCode", new
            {
                GroupCode = GroupCode
            }).ToList();
            List<_EPAY_V2_CLIENT_ACCESS_ID> result_hr = new List<_EPAY_V2_CLIENT_ACCESS_ID>();
            if (obj_hrclient != null)
            {
                foreach (var hr in obj_hrclient)
                {
                    _EPAY_V2_CLIENT_ACCESS_ID epay = new _EPAY_V2_CLIENT_ACCESS_ID();
                    epay.ID = hr.ID;
                    epay.clientName = hr.clientName;
                    epay.lastName = hr.lastName;
                    epay.firstName = hr.firstName;
                    epay.middleName = hr.middleName;
                    epay.contactNumber = hr.contactNumber;
                    epay.groupCode = hr.groupCode;
                    epay.email = hr.email;
                    //epay.GroupCode = hr.GroupCode;
                    //epay.changepass = hr.changepass;
                    epay.sent_status = hr.sent_status;
                    result_hr.Add(epay);
                }
            }

            return Json(result_hr, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ResendCredentials(string thisid)
        {
            //string done = "Reset Credentials";
            //conn.Execute("INSERT INTO AUDIT_TRAIL(userName, action) values('" + unm + "', '" + done + "')");
            var obj = conn.Query<string>(@"Select (firstName + lastName) FROM 
            LRWEB_V1_CLIENT_ACCESS_ID where ID = @intRecord", new {
                intRecord = thisid
            }).Single();
            conn.Execute(@"UPDATE LRWEB_V1_CLIENT_ACCESS_ID SET newPassword='YES',sent_status='0',attempt='0',status='ACTIVE',password=@password
             WHERE ID=@intRecord", new {
                intRecord =thisid,
                password = GlobalVariableClass.EncryptBCrypt((obj).Replace(" ", "").Replace(" ", "").Replace(" ", "").Replace(" ", "")).ToString()
            });

            return Json("", JsonRequestBehavior.AllowGet);
        }


        public JsonResult DeleteClientID(string thisid ,string username)
        {
            conn.Execute(@"DELETE LRWEB_V1_CLIENT_ACCESS_ID  where email=@irc", new {
                irc = thisid
            });
            string done = "Deleted HR " + thisid;
            logit(done);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public void logit(string done)
        {
            var arrcred = GlobalVariableClass.Base64Decode(GlobalVariableClass.Base64Decode(Session["cred"].ToString())).Split(',');
            var unm = arrcred[5].ToString();
            conn.Execute("INSERT INTO AUDIT_TRAIL(userName, action) values('" + unm + "', '" + done + "')");
        }
    }
}
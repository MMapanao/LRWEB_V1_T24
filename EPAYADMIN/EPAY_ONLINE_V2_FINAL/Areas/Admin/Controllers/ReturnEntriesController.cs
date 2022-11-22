using Dapper;
using LRWEB_V1_ADMIN_T24.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LRWEB_V1_ADMIN_T24.Areas.Admin.Controllers
{
    public class ReturnEntriesController : Controller
    {
        DateTime dateTime = DateTime.UtcNow.Date;
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMBANKconnection"].ToString());
        // GET: Admin/ReturnEntries
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
            var obj = conn.Query<_EPAY_V2_CLIENTS>(@" SELECT TOP 300 cl.*, 
  (Select (cred.firstname +' ' +cred.lastname) FROM LRWEB_V1_ADMIN_CRED AS cred where cred.IntRecord = cl.admin_id) AS encode_by
             FROM LRWEB_V1_CLIENTS as cl WHERE status='RETURNED' ORDER BY ID DESC").ToList();
            List<_EPAY_V2_CLIENTS> result_asd = new List<_EPAY_V2_CLIENTS>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    _EPAY_V2_CLIENTS model = new _EPAY_V2_CLIENTS();
                    model.ID = row.ID;
                    model.accountNumber = row.accountNumber;
                    model.groupCode = row.groupCode;
                    model.clientName = row.clientName;
                    model.customerID = row.customerID;
                    model.status = row.status;
                    model.admin_id = row.admin_id;
                    if (row.encode_by != null)
                    {
                        model.encode_by = GlobalVariableClass.IFNullEmptyString(row.encode_by.ToUpper());
                    }
                    else
                    {
                        model.encode_by = "Deleted";
                    }
                    
                    model.remarks = row.remarks;
                    result_asd.Add(model);
                }
            }
            return View(result_asd);
        }

        [HttpPost]
        public JsonResult SubmitClient(string model_state)
        {
            var mstate = model_state.Split(',');
            for (int x = 0; x < mstate.Length; x++)
            {
                conn.Execute(@"UPDATE LRWEB_V1_CLIENTS SET Status=@status 
                    WHERE groupCode=@gcode", new
                {
                    status = "PENDING",
                    gcode = mstate[x]
                });

                var arrcred = GlobalVariableClass.Base64Decode(GlobalVariableClass.Base64Decode(Session["cred"].ToString())).Split(',');
                var unm = arrcred[5].ToString();
                string done = "Resubmitted Group Code " + mstate[x];
                conn.Execute("INSERT INTO AUDIT_TRAIL(userName, action) values('" + unm + "', '" + done + "')");
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

    }
}
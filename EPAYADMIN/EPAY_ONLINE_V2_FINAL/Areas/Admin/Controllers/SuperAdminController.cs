using Dapper;
using LRWEB_V1_ADMIN_T24.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LRWEB_V1_ADMIN_T24.Areas.Admin.Controllers
{
    public class SuperAdminController : Controller
    {
        DateTime dateTime = DateTime.UtcNow.Date;
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMBANKconnection"].ToString());
        // GET: Admin/SuperAdmin
        public ActionResult Index()
        {
            if (Session["ticket"] == null || Session["cred"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                string ticket = Session["ticket"].ToString(), cred = Session["cred"].ToString();
                if (GlobalVariableClass.AuthenticatedSuperAdmin(ticket, cred) == false)
                {
                    return RedirectToAction("Index", "Login");
                }
            }

            //if (Session["ticket"] == null || Session["cred"] == null)
            //{
            //    return RedirectToAction("Index", "Login");
            //}
            //else
            //{
            //    if (GlobalVariableClass.AuthenticatedSuperAdmin(ticket, cred) == false)
            //    {
            //        return RedirectToAction("Index", "Login");
            //    }
            //}
            var obj_accounts = conn.Query<_SUPER_ADMIN>(Queries.QueryModel_SuperAdmin.GetAllAdmin()).ToList();
            List<_SUPER_ADMIN> result_cred = new List<_SUPER_ADMIN>();
            if (obj_accounts != null)
            {
                foreach (var row in obj_accounts)
                {
                    _SUPER_ADMIN dt = new _SUPER_ADMIN();
                    dt.IntRecord = row.IntRecord;
                    dt.firstname = row.firstname;
                    dt.lastname = row.lastname;
                    dt.middlename = row.middlename;
                    dt.contactnumber = row.contactnumber;
                    dt.email = row.email;
                    dt.accesslevel = row.accesslevel;
                    dt.password = row.password;
                    dt.status = row.status;
                    dt.sent_status = row.sent_status;
                    result_cred.Add(dt);
                }
            }
            return View(result_cred);

        }
        public ActionResult NewAdmin()
        {
            if (Session["ticket"] == null || Session["cred"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                string ticket = Session["ticket"].ToString(), cred = Session["cred"].ToString();
                if (GlobalVariableClass.AuthenticatedSuperAdmin(ticket, cred) == false)
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            return View();
        }

        public ActionResult AuditTrail()
        {
            if (Session["ticket"] == null || Session["cred"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                string ticket = Session["ticket"].ToString(), cred = Session["cred"].ToString();
                if (GlobalVariableClass.AuthenticatedSuperAdmin(ticket, cred) == false)
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            var obj_accounts = conn.Query<_AUDIT_TRAIL>("SELECT TOP (1000) ID,userName, action, dateCreated FROM AUDIT_TRAIL ORDER BY ID DESC").ToList();
            List<_AUDIT_TRAIL> result_cred = new List<_AUDIT_TRAIL>();
            if (obj_accounts != null)
            {
                foreach (var row in obj_accounts)
                {
                    _AUDIT_TRAIL dt = new _AUDIT_TRAIL();
                    dt.ID = row.ID;
                    dt.userName = row.userName;
                    dt.action = row.action;
                    dt.dateCreated = row.dateCreated;
                    result_cred.Add(dt);
                }
            }
            return View(result_cred);
        }

        [HttpPost]
        public JsonResult SubmitNewAdmin(_SUPER_ADMIN model_state)
        {
            var arr_cred = new string[20];
            string ticket = Session["ticket"].ToString(), cred = Session["cred"].ToString();
            arr_cred = GlobalVariableClass.Base64Decode(GlobalVariableClass.Base64Decode(cred)).Split(',');
            var unm = arr_cred[5].ToString();

            if (!ModelState.IsValid)
            {
                var checkExistingEmail = conn.Query<int>(@"SELECT INTRECORD FROM LRWEB_V1_ADMIN_CRED WHERE email=@email", new
                {
                    email = model_state.email
                }).Count();
                return Json(ValidateNewAdmin(checkExistingEmail), JsonRequestBehavior.AllowGet);
            }else
            {
                var checkExistingEmail = conn.Query<int>(@"SELECT INTRECORD FROM LRWEB_V1_ADMIN_CRED WHERE email=@email", new {
                    email  = model_state.email
                }).Count();
                if(checkExistingEmail != 0)
                {
                    // PUT RESPONSE HERE ON VALIDATION OF EXISITING EMAIL
                    return Json(ValidateNewAdmin(checkExistingEmail), JsonRequestBehavior.AllowGet);
                }
                conn.Execute(@"INSERT INTO LRWEB_V1_ADMIN_CRED 
                (firstname,lastname,middlename,contactnumber,email,accesslevel,password) VALUES 
                (@firstname,@lastname,@middlename,@contactnumber,@email,@accesslevel,@password)", new
                {
                    firstname = model_state.firstname,
                    lastname = model_state.lastname,
                    middlename = model_state.middlename,
                    contactnumber = model_state.contactnumber,
                    email = model_state.email,
                    accesslevel = model_state.accesslevel,
                    password = GeneratePassword()
                });
                string done = "Added new user " + model_state.email;
                conn.Execute("INSERT INTO AUDIT_TRAIL(userName, action) values('" + unm + "', '" + done + "')");
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }

        public string[,] ValidateNewAdmin(int emailexist)
        {

            var response_array = new string[2, 9];
            int asdx = 0;
            foreach (var modelnames in ModelState)
            {
                // var mm = modelnames.Key;
                response_array[0, asdx] = modelnames.Key;
                asdx++;
            }
            int y = 0;
            foreach (var modelStateVal in ViewData.ModelState.Values)
            {
                foreach (var error in modelStateVal.Errors)
                { 
                        response_array[1, y] = error.ErrorMessage;
                }
                if (y == 5 && emailexist != 0)
                {
                    response_array[1, 5] = "Email is already Exist";
                }
                y += 1;
            }

            return response_array;
        }

        public string GeneratePassword()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        [HttpPost]
        public JsonResult checkAuth()
        {
            var arr_cred = new string[20];
            string token = Session["ticket"].ToString(), cred = Session["cred"].ToString();
            arr_cred = GlobalVariableClass.Base64Decode(GlobalVariableClass.Base64Decode(cred)).Split(',');
            var id = arr_cred[0].ToString();
            var obj = conn.Query<int>("SELECT * FROM LRWEB_V1_AUTH WHERE EmployeeEmail='"+ id +"' and token='"+ token +"' and logout=1").Count();
            if (obj != 0)
            {
                return Json("TRUE", JsonRequestBehavior.AllowGet);
            } else
            {
                return Json("FALSE", JsonRequestBehavior.AllowGet);
            }
        }
        
        [HttpPost]
        public JsonResult LOGOUT()
        {
            var arr_cred = new string[20];
            string token = Session["ticket"].ToString(), cred = Session["cred"].ToString();
            arr_cred = GlobalVariableClass.Base64Decode(GlobalVariableClass.Base64Decode(cred)).Split(',');
            var id = arr_cred[0].ToString();
            var obj = conn.Execute("UPDATE LRWEB_V1_AUTH SET logout=1 WHERE EmployeeEmail='" + id + "' and token='" + token + "'");
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteSecAdmin(string admin_id,string username)
        {
            conn.Execute(@"DELETE LRWEB_V1_ADMIN_CRED 
            where IntRecord=@intr",new {
                intr = admin_id
            });
            var arr_cred = new string[20];
            string ticket = Session["ticket"].ToString(), cred = Session["cred"].ToString();
            arr_cred = GlobalVariableClass.Base64Decode(GlobalVariableClass.Base64Decode(cred)).Split(',');
            var unm = arr_cred[5].ToString();
            string done = "Deleted user " + username + ".";
            conn.Execute("INSERT INTO AUDIT_TRAIL(userName, action) values('" + unm + "', '" + done + "')");

            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DisableSecAdmin(string admin_id, string username)
        {
            conn.Execute(@"UPDATE LRWEB_V1_ADMIN_CRED SET status='DISABLED' 
            where IntRecord=@intr",new {
                intr = admin_id
            });
            var arr_cred = new string[20];
            string ticket = Session["ticket"].ToString(), cred = Session["cred"].ToString();
            arr_cred = GlobalVariableClass.Base64Decode(GlobalVariableClass.Base64Decode(cred)).Split(',');
            var unm = arr_cred[5].ToString();
            string done = "Disabled user " + username + ".";
            conn.Execute("INSERT INTO AUDIT_TRAIL(userName, action) values('" + unm + "', '" + done + "')");
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ResendSecAdmin(string admin_id, string username)
        {
            conn.Execute(@"UPDATE LRWEB_V1_ADMIN_CRED SET sent_status='0', password=@pword 
            where IntRecord=@intr", new
            {
                intr = admin_id,
                pword = GeneratePassword()
            });
            var arr_cred = new string[20];
            string ticket = Session["ticket"].ToString(), cred = Session["cred"].ToString();
            arr_cred = GlobalVariableClass.Base64Decode(GlobalVariableClass.Base64Decode(cred)).Split(',');
            var unm = arr_cred[5].ToString();
            string done = "Resend credentials for user " + username + ".";
            conn.Execute("INSERT INTO AUDIT_TRAIL(userName, action) values('" + unm + "', '" + done + "')");
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ActiveSecAdmin(string admin_id, string username)
        {
            conn.Execute(@"UPDATE LRWEB_V1_ADMIN_CRED SET status='ACTIVE' 
            where IntRecord=@intr", new
            {
                intr = admin_id
            });
            var arr_cred = new string[20];
            string ticket = Session["ticket"].ToString(), cred = Session["cred"].ToString();
            arr_cred = GlobalVariableClass.Base64Decode(GlobalVariableClass.Base64Decode(cred)).Split(',');
            var unm = arr_cred[5].ToString();
            string done = "Reactivate user " + username + ".";
            conn.Execute("INSERT INTO AUDIT_TRAIL(userName, action) values('" + unm + "', '" + done + "')");
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditDetails()
        {
            if(Request.QueryString["identity"] == null || Request.QueryString["identity"].ToString() == "")
            {
                return View(GlobalVariableClass.FolderName + "/Admin/SuperAdmin/Index?content=admin");
            }
            var obj_accounts = conn.Query<_SUPER_ADMIN>(Queries.QueryModel_SuperAdmin.GetAllAdmin() +
                " AND IntRecord=@intrecord",new {
                    intrecord = Request.QueryString["identity"]
                }).Single();
            return View(obj_accounts);
        }

        [HttpPost]
        public JsonResult UPDATE_CRED_Admin(_SUPER_ADMIN model_state)
        {
            ModelState.Remove("firstname");
            ModelState.Remove("lastname");
            if (!ModelState.IsValid)
            {
                var response_array = new string[2, 9];
                int asdx = 0;
                foreach (var modelnames in ModelState)
                {
                    // var mm = modelnames.Key;
                    response_array[0, asdx] = modelnames.Key;
                    asdx++;
                }
                int y = 0;
                foreach (var modelStateVal in ViewData.ModelState.Values)
                {
                    foreach (var error in modelStateVal.Errors)
                    {
                        //var errorMessage = error.ErrorMessage;
                        //var exception = error.Exception;
                        response_array[1, y] = error.ErrorMessage;
                    }
                    y += 1;
                }
                return Json(response_array);
            }
            else
            {


                conn.Execute(@"UPDATE LRWEB_V1_ADMIN_CRED 
                SET contactnumber=@contactnumber,email=@email,accesslevel=@accesslevel 
                 WHERE IntRecord=@intrecord", new
                {
                    
                    contactnumber = model_state.contactnumber,
                    email = model_state.email,
                    accesslevel = model_state.accesslevel,
                    intrecord = model_state.IntRecord
                });
                var arr_cred = new string[20];
                string ticket = Session["ticket"].ToString(), cred = Session["cred"].ToString();
                arr_cred = GlobalVariableClass.Base64Decode(GlobalVariableClass.Base64Decode(cred)).Split(',');
                var unm = arr_cred[5].ToString();
                string done = "Update user " + model_state.email + ".";
                conn.Execute("INSERT INTO AUDIT_TRAIL(userName, action) values('" + unm + "', '" + done + "')");
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult UPDATE_PASSWORD(string newpass, string oldpass)
        {
            var arr_cred = new string[20];
            string ticket = Session["ticket"].ToString(), cred = Session["cred"].ToString();
            arr_cred = GlobalVariableClass.Base64Decode(GlobalVariableClass.Base64Decode(cred)).Split(',');
            var unm = arr_cred[5].ToString();


            var obj = conn.Query<int>(@"SELECT INTRECORD FROM LRWEB_V1_ADMIN_CRED WHERE 
                IntRecord=@IntRecord and password=@password", new
            {
                IntRecord = arr_cred[0].ToString(),
                password = oldpass
            }).Count();
            if (obj == 0)
            {
                return Json("Old password is incorrect", JsonRequestBehavior.AllowGet);
            }

            var data = conn.Query<_CHECKPASSWORD>(@"SELECT TOP 5 pastPassword 
                FROM PASSWORD_HISTORY  
                WHERE userName =@email 
                ORDER BY dateUpdated DESC", new
            {
                email = unm
            }).ToList();
            for (int i = 0; i < data.Count; i++)
            {
                if (newpass == data[i].pastPassword)
                {
                    return Json("Invalid Password", JsonRequestBehavior.AllowGet);
                }
            }

            string done = "Password Change";
            conn.Execute("INSERT INTO AUDIT_TRAIL(userName, action) values('" + unm + "', '" + done + "')");
            conn.Execute(@"UPDATE LRWEB_V1_ADMIN_CRED SET password=@password WHERE INTRECORD=@intrecord", new
            {
                password = newpass,
                intrecord = arr_cred[0].ToString()
            });
            conn.Execute("INSERT INTO PASSWORD_HISTORY(userName, pastPassword) values('" + unm + "', '" + newpass + "')");

            return Json("Password Success", JsonRequestBehavior.AllowGet);
        }

        public class _CHANGE_PASSWORD
        {
            public string txtoldpassword { get; set; }
            public string txtnewpassword { get; set; }
            public string txtconfirm { get; set; }
        }
    }
}
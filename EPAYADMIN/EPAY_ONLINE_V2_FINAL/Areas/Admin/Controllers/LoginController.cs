using Dapper;
using LRWEB_V1_ADMIN_T24.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.Mvc;

namespace LRWEB_V1_ADMIN_T24.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMBANKconnection"].ToString());
        // GET: Admin/Login
        public ActionResult Index()
        {


            if (Session["ticket"] == null || Session["cred"] == null)
            {

            }
            else
            {
                Session["cred"] = null;
                Session["ticket"] = null;
            }
            
            return View();
        }


        [HttpPost]
        public JsonResult awkfawidjawkdvnkl(string unm, string ups)
        {
            
            var obj = conn.Query<_Login>(@"Select * From LRWEB_v1_ADMIN_CRED  WHERE 
            email = @email and password = @pass and status='ACTIVE'", new
            {
                email = unm,
                pass = ups
            }).ToList();
            string cred = "";
            int x = 0;
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    cred = row.IntRecord + "," + row.lastname + "," + row.firstname + "," + row.middlename + "," +
                        row.contactnumber + "," + row.email + "," + row.accesslevel;
                    x++;

                }
            }
            if (x != 0)
            {
                
                //CREATING TOKEn
                string[] credentials = cred.Split(',');
                TimeSpan time1 = TimeSpan.FromHours(1);
                TimeSpan ts = DateTime.Now.TimeOfDay;
                var ts2 = ts.Add(time1);
                DateTime dt = new DateTime();
                dt = DateTime.Now;
                dt = dt.AddHours(15);
                string expiry = dt.ToString("yyyyMMddHHmmss");
                string randchar = GlobalVariableClass.RandomString(64);
                string timesstring = DateTime.Now.ToString("yyyyMMddHHmmss");
                conn.Execute("UPDATE LRWEB_v1_AUTH SET logout=1 WHERE EmployeeEmail='" + credentials[0] + "'");
                conn.Execute("Insert into LRWEB_v1_AUTH (GroupCode,EmployeeEmail,token,timestamp,timeexpiration,logout) values ('" +
                    credentials[6] + "','" + credentials[0] + "','" + randchar + "','" + timesstring + "','" + expiry +
                    "','0')");
                Session["ticket"] = randchar;
                string text = GlobalVariableClass.Base64Encode(GlobalVariableClass.Base64Encode(cred)); // 2x encrypt the information
                Session["cred"] = text;
                string faketokens = GlobalVariableClass.RandomString(72);
                Session["tokens"] = faketokens;
                HttpCookie myCookie = new HttpCookie("tokens");
                myCookie.Value = faketokens;
                myCookie.Expires = DateTime.Now.AddHours(15);
                Response.Cookies.Add(myCookie);

                if (credentials[6] == "ADMIN")
                {
                    string done = "Successfully Login as Admin";
                    conn.Execute("INSERT INTO AUDIT_TRAIL(userName, action) values('" + unm + "', '" + done + "')");
                    return Json(new { accesslevel = "ADMIN" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string done = "Successfully Login as On-Boarding User";
                    conn.Execute("INSERT INTO AUDIT_TRAIL(userName, action) values('" + unm + "', '" + done + "')");
                    return Json(new { accesslevel = "OBT" }, JsonRequestBehavior.AllowGet);
                }

                
            }
            return Json(new { accesslevel = "OBT" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult checklogin(string unm, string ups)
        {
            var data = conn.Query<_Login>(@"SELECT *, 
                (select DATEDIFF(DAY,passwordUpdated,getdate())) as daysgone 
                FROM LRWEB_V1_ADMIN_CRED  WHERE email =@email", new
            {
                email = unm
            }).ToList();

            if (data.Count != 0)
            {
                var ID = data[0].IntRecord;
                if (data[0].status == "DISABLED")
                {
                    return Json(new { message = "disabled" }, JsonRequestBehavior.AllowGet);
                }
                else if (data[0].password != ups && data[0].IntRecord != "1000000")
                {

                    var attempt = data[0].attempt + 1;
                    if (attempt == 3)
                    {
                        string done = "Exceeded number of login attempt.";
                        conn.Execute("UPDATE LRWEB_v1_ADMIN_CRED SET status = 'DISABLED', attempt ='" + attempt + "' WHERE IntRecord = " + ID);
                        conn.Execute("INSERT INTO AUDIT_TRAIL(userName, action) values('" + unm + "', '" + done + "')");
                        return Json(new { message = "exceedAttempt" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        conn.Execute("UPDATE LRWEB_v1_ADMIN_CRED SET attempt ='" + attempt + "' WHERE IntRecord = " + ID);
                        return Json(new { attempt = attempt, message = "passwordFailed" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (data[0].password == ups)
                {
                    conn.Execute("UPDATE LRWEB_v1_ADMIN_CRED SET attempt ='0' WHERE IntRecord = " + ID);
                    if (data[0].newPassword == "YES" && data[0].IntRecord != "1000000")
                    {
                        return Json(new { message = "newPassword" }, JsonRequestBehavior.AllowGet);
                    }
                    else if (data[0].daysgone > 180 && data[0].IntRecord != "1000000")
                    {
                        return Json(new { message = "changePassword" }, JsonRequestBehavior.AllowGet);
                    } else
                    {
                        var obj = conn.QueryFirstOrDefault<string>("SELECT IIF((SELECT TOP 1 logout FROM LRWEB_v1_AUTH WHERE EmployeeEmail ='"+ ID +"' ORDER BY IntRecord DESC) = 1 ,'confirm','noLogout') as checker"
                            ).ToString();
                        if (obj == "confirm") {
                            return Json(new { message = "confirm" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { message = "noLogout" }, JsonRequestBehavior.AllowGet);
                        }
                        
                    }
                } else
                {
                    return Json(new { message = "wrongPassword" }, JsonRequestBehavior.AllowGet);
                }

            }


            return Json(new { message = "noAccount" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult changepass (string unm, string newpass)
        {
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
                    return Json(new { message = "invalidPassword" }, JsonRequestBehavior.AllowGet);
                }
            }
            string done = "Password Change";
            conn.Execute("INSERT INTO AUDIT_TRAIL(userName, action) values('" + unm + "', '" + done + "')");
            conn.Execute("UPDATE LRWEB_v1_ADMIN_CRED SET OTPCode='', OTPDateTime='', newPassword = 'NO', password ='" + newpass + "', passwordUpdated = getdate(), dateUpdated = getdate(), status='ACTIVE', attempt='0' WHERE email = '" + unm + "'");
            conn.Execute("INSERT INTO PASSWORD_HISTORY(userName, pastPassword) values('" + unm + "', '" + newpass + "')");
            return Json(new { message = "passwordChanged" , unm = unm, ups = newpass }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult checkemail(string unm)
        {
            var data = conn.Query<_Login>(@"SELECT *, 
                (select DATEDIFF(DAY,passwordUpdated,getdate())) as daysgone 
                FROM LRWEB_v1_ADMIN_CRED  WHERE email =@email", new
            {
                email = unm
            }).ToList();
            if (data.Count == 0)
            {
                return Json(new { message = "noAccount" }, JsonRequestBehavior.AllowGet);
            } else
            {
                DateTime serverTime = DateTime.Now;
                var dt = serverTime.ToString("yyyyMMddHHmm");
                var otpcode = GenerateStringCode();
                conn.Execute("UPDATE LRWEB_v1_ADMIN_CRED SET OTPCode ='" + otpcode + "', OTPDateTime = '" + dt + "', sent_status = '00'  WHERE email = '" + unm + "'");
                return Json(new { message = "confirm" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult checkOTP(string unm, string otp)
        {
            DateTime serverTime = DateTime.Now;
            var mytime = serverTime.ToString("yyyyMMddHHmm");
            var data = conn.Query<_Login>(@"SELECT *
                FROM LRWEB_v1_ADMIN_CRED as cred WHERE email =@email and OTPCode=@otp and 
                (CONVERT(bigINT, cred.OTPDateTime) <= CONVERT(bigINT, @dtme) and 
                (CONVERT(bigINT, cred.OTPDateTime) + 5) >= CONVERT(bigINT,  @dtme))", new
            {
                email = unm,
                otp = otp,
                dtme = serverTime.ToString("yyyyMMddHHmm")
            }).ToList();
            if (data.Count != 0)
            {
                return Json(new { message = "valid" }, JsonRequestBehavior.AllowGet);
            } else
            {
                return Json(new { message = "OTP Code is invalid!" }, JsonRequestBehavior.AllowGet);
            }
            //var obj = conn.Query<_Login>("Select cai.*," +
            //    "(Select CUST_GRP_DESC from FIN_CUST_GRP_CODE_CMS as cgp where cgp.CUST_GRP_CODE = cai.GroupCode) as CompanyName" +
            //    @" from EPAY_V2_CLIENT_ACCESS_ID as cai  WHERE email = @email and password = @password and OTPCode=@otp
            //    and (CONVERT(bigINT, cai.OTPDateTime) <= CONVERT(bigINT, @dtme) and 
            //    (CONVERT(bigINT, cai.OTPDateTime) + 5) >= CONVERT(bigINT,  @dtme))", new
            //    {
            //        email = jsonX.unm,
            //        password = GlobalVariableClass.EncryptBCrypt(jsonX.ups),
            //        otp = jsonX.code,
            //        dtme = serverTime.ToString("yyyyMMddHHmm")
            //    }).ToList();
            //return Json("", JsonRequestBehavior.AllowGet);
        }
        public string GenerateStringCode()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 8)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
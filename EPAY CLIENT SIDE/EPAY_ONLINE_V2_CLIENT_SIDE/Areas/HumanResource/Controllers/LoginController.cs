using Dapper;
using LRWEB_V1_CLIENT_SIDE_T24.Areas.HumanResource.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LRWEB_V1_CLIENT_SIDE_T24.Areas.HumanResource.Controllers
{
    public class LoginController : Controller
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMBANKconnection"].ToString());
        DateTime dateTime = DateTime.UtcNow.Date;
         
        // GET: HumanResource/Login
        [AllowAnonymous]
        public ActionResult Index()
        {

            try
            {
                Response.Cookies["ASP_NET_CD"].Expires = DateTime.Now;
                Response.Cookies["ASP_NET_TKT"].Expires = DateTime.Now;
            }
            catch(Exception ex)
            {}
            return View();
        }

        public class USR
        {
            public string unm { get; set; }
            public string ups { get; set; }
            public string code { get; set; }
            public bool otp { get; set; }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult awkfawidjawkdvnkl(USR jsonX)
        {
            //var res = OTPEmailResponse(jsonX); 
            //if(res == "VALID")
            //{
                DateTime serverTime = DateTime.Now;
            var obj = conn.Query<_Login>(@"Select cai.*, (Select clientName from LRWEB_V1_CLIENTS as cgp where cgp.groupCode = cai.groupCode) as clientName From LRWEB_v1_CLIENT_ACCESS_ID as cai  WHERE 
            email = @email and password = @pass", new
            {
                email = jsonX.unm,
                pass = GlobalVariableClass.EncryptBCrypt(jsonX.ups).ToString()
            }).ToList();
            //var obj = conn.Query<_Login>("Select cai.*," +
            //        "(Select CUST_GRP_DESC from FIN_CUST_GRP_CODE_CMS as cgp where cgp.CUST_GRP_CODE = cai.GroupCode) as CompanyName" +
            //        @" from EPAY_V2_CLIENT_ACCESS_ID as cai  WHERE email = @email and password = @password", new
            //        {
            //            email = jsonX.unm,
            //            password = GlobalVariableClass.EncryptBCrypt(jsonX.ups).ToString()
            //}).ToList();
                string cred = "";
                int x = 0;
                if (obj != null)
                {
                    foreach (var row in obj)
                    {
                        cred = row.ID + "," + row.groupCode + "," + row.lastName + "," + row.firstName + "," +
                            row.middleName + "," + row.contactNumber + "," + row.email + "," + row.clientName;
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
                    dt = dt.AddHours(2);
                    string expiry = dt.ToString("yyyyMMddHHmmss");
                    string randchar = GlobalVariableClass.RandomString(64);
                    string timesstring = DateTime.Now.ToString("yyyyMMddHHmmss");
                    conn.Execute("Insert into LRWEB_V1_AUTH (GroupCode,EmployeeEmail,token,timestamp,timeexpiration,logout) values ('" +
                        credentials[1] + "','" + credentials[6] + "','" + randchar + "','" + timesstring + "','" + expiry +
                        "','0')");
                    HttpCookie tickentCookie = new HttpCookie("ASP_NET_TKT");
                    tickentCookie.Value = randchar;
                    tickentCookie.Expires = DateTime.Now.AddMinutes(15);
                    Response.Cookies.Add(tickentCookie);
                    string text = GlobalVariableClass.Base64Encode(GlobalVariableClass.Base64Encode(cred));// 2x encrypt the information
                    HttpCookie CredCookie = new HttpCookie("ASP_NET_CD");
                    CredCookie.Value = text;
                    CredCookie.Expires = DateTime.Now.AddHours(15);
                    Response.Cookies.Add(CredCookie);
                    string faketokens = GlobalVariableClass.RandomString(65);
                    Session["tokens"] = faketokens;
                    HttpCookie myCookie = new HttpCookie("tokens");
                    myCookie.Value = faketokens;
                    myCookie.Expires = DateTime.Now.AddHours(2);
                    Response.Cookies.Add(myCookie);

                    string done = "Successfully Login as HR on " + credentials[7] + ".";
                    conn.Execute("INSERT INTO AUDIT_TRAIL(userName, action) values('" + credentials[6] + "', '" + done + "')");
                    return Json("SC1", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Login Failed", JsonRequestBehavior.AllowGet);
                }
            //}
            //else
            //{
            //    return Json(res,JsonRequestBehavior.AllowGet);
            //}

        }
        
        
        public string OTPEmailResponse(USR jsonX)
        {
            //            if (jsonX.unm == "")
            //            {
            //                return "Username is Required";
            //            }
            //            else if (jsonX.ups == "")
            //            {
            //                return "Password is Required!";
            //            }

            //            var obj = conn.Query<_Login>("Select cai.*," +
            //                @"(Select CUST_GRP_DESC from FIN_CUST_GRP_CODE_CMS as cgp where 
            //cgp.CUST_GRP_CODE = cai.GroupCode) as CompanyName" +
            //                " from EPAY_V2_CLIENT_ACCESS_ID as cai  WHERE email = @email and password = @password", new
            //                {
            //                    email = jsonX.unm,
            //                    password = GlobalVariableClass.EncryptBCrypt(jsonX.ups)
            //                }).ToList();
            //            int x = 0;
            //            string fname = "";
            //            if (obj != null)
            //            {
            //                if(obj.Count != 0)
            //                {
            //                    //var code = GenerateStringCode();
            //                    var code = "123456";
            //                    string cookievalue = string.Empty;
            //                    if (Request.Cookies["ymca"] != null)
            //                    {
            //                        if (obj[0].OTPDateTime != null)
            //                        {
            //                            var dateExpire = DateTime.ParseExact(obj[0].OTPDateTime, "yyyyMMddHHmm", CultureInfo.InvariantCulture).AddDays(30);
            //                            cookievalue = GlobalVariableClass.Base64Decode(GlobalVariableClass.Base64Decode(Request.Cookies["ymca"].Value));
            //                            if (("PM8GNR7BN4" + obj[0].OTPCode + "D89NB3BDH" + obj[0].email) == cookievalue &&
            //                                Int64.Parse(dateExpire.ToString("yyyyMMddHHmm")) > Int64.Parse(DateTime.Now.ToString("yyyyMMddHHmm").ToString()))
            //                            {
            //                                return "VALID";
            //                            }
            //                        }
            //                    }
            //                    DateTime serverTime = DateTime.Now;
            //                    conn.Execute(@"UPDATE EPAY_V2_CLIENT_ACCESS_ID SET
            //                OTPCode=@otp,OTPDateTime=@dt WHERE email = @email and password = @password", new
            //                    {
            //                        email = jsonX.unm,
            //                        password = GlobalVariableClass.EncryptBCrypt(jsonX.ups),
            //                        otp = code,
            //                        dt = serverTime.ToString("yyyyMMddHHmm")
            //                    });
            //                    EmailSending(jsonX.unm, code, fname);
            //                    return "1";
            //                }
            //                else
            //                {
            //                    return "Login Failed!";
            //                }
            //            }
            //            else
            //            {
            //                return "Login Failed!";
            //            }
            return "Login Failed!";
        }
        public string GenerateStringCode()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 6)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult zxcValidate(USR jsonX)
        {
            //if (jsonX.unm == "")
            //{
            //    return Json("Username is Required", JsonRequestBehavior.AllowGet);
            //}
            //else if (jsonX.ups == "")
            //{
            //    return Json("Password is Required!", JsonRequestBehavior.AllowGet);
            //}
            //else if (jsonX.code == null)
            //{
            //    return Json("OTP Code is Required!", JsonRequestBehavior.AllowGet);
            //}
            //DateTime serverTime = DateTime.Now;
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
            //string cred = "";
            //int x = 0;
            //if (obj != null)
            //{
            //    foreach (var row in obj)
            //    {
            //        cred = row.ID + "," + row.groupCode + "," + row.last_name + "," + row.first_name + "," +
            //            row.middle_name + "," + row.contact_no + "," + row.email + "," + row.checker + "," + row.maker + "," + row.CompanyName;
            //        x++;
            //    }
            //}
            //if (x != 0)
            //{
            //    //CREATING TOKEn
            //    string[] credentials = cred.Split(',');
            //    TimeSpan time1 = TimeSpan.FromHours(1);
            //    TimeSpan ts = DateTime.Now.TimeOfDay;
            //    var ts2 = ts.Add(time1);
            //    DateTime dt = new DateTime();
            //    dt = DateTime.Now;
            //    dt = dt.AddHours(2);
            //    string expiry = dt.ToString("yyyyMMddHHmmss");
            //    string randchar = GlobalVariableClass.RandomString(64);
            //    string timesstring = DateTime.Now.ToString("yyyyMMddHHmmss");
            //    conn.Execute("Insert into EPAY_V2_AUTH (GroupCode,EmployeeEmail,token,timestamp,timeexpiration,logout) values ('" +
            //        credentials[1] + "','" + credentials[6] + "','" + randchar + "','" + timesstring + "','" + expiry +
            //        "','0')");
            //    HttpCookie tickentCookie = new HttpCookie("ASP_NET_TKT");
            //    tickentCookie.Value = randchar;
            //    tickentCookie.Expires = DateTime.Now.AddMinutes(15);
            //    Response.Cookies.Add(tickentCookie);
            //    string text = GlobalVariableClass.Base64Encode(GlobalVariableClass.Base64Encode(cred));// 2x encrypt the information
            //    HttpCookie CredCookie = new HttpCookie("ASP_NET_CD");
            //    CredCookie.Value = text;
            //    CredCookie.Expires = DateTime.Now.AddMinutes(60);
            //    Response.Cookies.Add(CredCookie);
            //    string faketokens = GlobalVariableClass.RandomString(65);
            //    Session["tokens"] = faketokens;
            //    if(jsonX.otp == true)
            //    {
            //        Response.Cookies["ymca"].Value = GlobalVariableClass.Base64Encode(GlobalVariableClass.Base64Encode("PM8GNR7BN4" + jsonX.code + "D89NB3BDH" + obj[0].email));
            //        Response.Cookies["ymca"].Expires = DateTime.Now.AddDays(30);
            //    }

            //    HttpCookie myCookie = new HttpCookie("tokens");
            //    myCookie.Value = faketokens;
            //    myCookie.Expires = DateTime.Now.AddHours(2);
            //    Response.Cookies.Add(myCookie);

            //    return Json("1", JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    return Json("OTP Code is Invalid!", JsonRequestBehavior.AllowGet);
            //}
            return Json("OTP Code is Invalid!", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult OTPResendEmail(USR jsonX)
        {
            return Json(OTPEmailResponse(jsonX), JsonRequestBehavior.AllowGet);
        }

        public void EmailSending(string email,string otp,string fname)
        {

//            MailMessage mail = new MailMessage(ConfigurationManager.AppSettings["EmailName"], email);
//            //MailMessage mail = new MailMessage("10.204.2.39", email);
//            SmtpClient client = new SmtpClient();

//            client.DeliveryMethod = SmtpDeliveryMethod.Network;
//            client.UseDefaultCredentials = false;
//            //client.Host = ConfigurationManager.AppSettings["EmailURL"].ToString();
//            client.Host = "10.204.2.39";
//            mail.Subject = "EPAY OTP";
//            mail.IsBodyHtml = true;
//            mail.Body = @"<!DOCTYPE html>
//<html>
//<body style='padding-left: 15px;padding-right:15px;'>
//    <div style='width:100%;background-color: #ff8107;height:5px;'>
//    </div>
//    <div style='background-color: #fff;padding: 15px;font-family: Calibri;'>
//        <p style='font-size: 1.4rem;margin-top:5px;margin-bottom: 3px;'>Hi <b>" + fname + @",</b></p>  
//        <p style='font-size: 1.0rem;margin-top:5px;margin-bottom: 3px;'></p>
//    </div>
//    <div style='padding-left: 15px;padding-right:15px;'>
//        <ul style='-webkit-box-shadow: 0px 0px 5px 0px rgba(0,0,0,0.75);
//        -moz-box-shadow: 0px 0px 5px 0px rgba(0,0,0,0.75);
//        box-shadow: 0px 0px 5px 0px rgba(0,0,0,0.75);width:100%;list-style-type: none;padding:0px;font-family: Calibri;color:#fff;
//        background-color: #ff8107;'>
//            <li style='padding-left: 20px;padding-top: 15px;'>
//               <h3 style='font-size:1.4rem;margin: 0px;'>E-PAY ONE TIME PASSWORD</h3>
                
//            </li>
//            <li style='background-color: #fff;'>
//                    <div style='padding: 10px;background-color:#eee;font-family: Calibri;color:#2e2e2e;width:100%;text-align: center;'>
//                           <u><h1>"+ otp +@"</h1></u> 
//                    </div>
//            </li>
//        </ul>
//    </div>
//    <div style='font-family: Calibri;text-align: center;color:#7e7e7e;'>
//            Please do not reply to this email. This is a system-generated email.
//    </div>
//</body>
//</html>";
            //client.Send(mail);
        }

        public JsonResult checklogin(string unm, string ups)
        {
            var data = conn.Query<_Login>(@"SELECT * 
                FROM LRWEB_V1_CLIENT_ACCESS_ID  WHERE email =@email", new
            {
                email = unm
            }).ToList();

            if (data.Count != 0)
            {
                var ID = data[0].ID;
                if (data[0].status == "DISABLED")
                {
                    return Json(new { message = "disabled" }, JsonRequestBehavior.AllowGet);
                }
                else if (data[0].password != ups)
                {

                    var attempt = data[0].attempt + 1;
                    if (attempt == 3)
                    {
                        string done = "Exceeded number of login attempt.";
                        conn.Execute("UPDATE LRWEB_v1_CLIENT_ACCESS_ID SET status = 'DISABLED', attempt ='" + attempt + "' WHERE ID = " + ID);
                        conn.Execute("INSERT INTO AUDIT_TRAIL(userName, action) values('" + unm + "', '" + done + "')");
                        return Json(new { message = "exceedAttempt" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        conn.Execute("UPDATE LRWEB_v1_CLIENT_ACCESS_ID SET attempt ='" + attempt + "' WHERE ID = " + ID);
                        return Json(new { attempt = attempt, message = "passwordFailed" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (data[0].password == ups)
                {
                    conn.Execute("UPDATE LRWEB_v1_CLIENT_ACCESS_ID SET attempt ='0' WHERE ID = " + ID);
                    if (data[0].newPassword == "YES")
                    {
                        return Json(new { message = "newPassword" }, JsonRequestBehavior.AllowGet);
                    }
                    //else if (data[0].daysgone > 180)
                    //{
                    //    return Json(new { message = "changePassword" }, JsonRequestBehavior.AllowGet);
                    //}
                    else
                    {
                        var obj = conn.QueryFirstOrDefault<string>("SELECT IIF((SELECT TOP 1 logout FROM LRWEB_v1_AUTH WHERE EmployeeEmail ='" + ID + "' ORDER BY IntRecord DESC) = 1 ,'confirm','noLogout') as checker"
                            ).ToString();
                        if (obj == "confirm")
                        {
                            return Json(new { message = "confirm" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { message = "noLogout" }, JsonRequestBehavior.AllowGet);
                        }

                    }
                }
                else
                {
                    return Json(new { message = "wrongPassword" }, JsonRequestBehavior.AllowGet);
                }

            }


            return Json(new { message = "noAccount" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult changepass(string unm, string newpass)
        {
            string done = "Password Change";
            conn.Execute("INSERT INTO AUDIT_TRAIL(userName, action) values('" + unm + "', '" + done + "')");
            conn.Execute("UPDATE LRWEB_v1_CLIENT_ACCESS_ID SET OTPCode='', OTPDateTime='', newPassword = 'NO', password ='" + newpass + "', dateUpdated = getdate(), status='ACTIVE', attempt='0' WHERE email = '" + unm + "'");
            return Json(new { message = "passwordChanged", unm = unm, ups = newpass }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult checkemail(string unm)
        {
            var data = conn.Query<_Login>(@"SELECT * 
                FROM LRWEB_V1_CLIENT_ACCESS_ID  WHERE email =@email", new
            {
                email = unm
            }).ToList();
            if (data.Count == 0)
            {
                return Json(new { message = "noAccount" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                DateTime serverTime = DateTime.Now;
                var dt = serverTime.ToString("yyyyMMddHHmm");
                var otpcode = GenerateStringCode();
                conn.Execute("UPDATE LRWEB_v1_CLIENT_ACCESS_ID SET OTPCode ='" + otpcode + "', OTPDateTime = '" + dt + "', sent_status = '00'  WHERE email = '" + unm + "'");
                return Json(new { message = "confirm" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult checkOTP(string unm, string otp)
        {
            //DateTime serverTime = DateTime.Now;
            //var mytime = serverTime.ToString("yyyyMMddHHmm");
            //var data = conn.Query<_Login>(@"SELECT *
            //    FROM LRWEB_v1_CLIENT_ACCESS_ID as cred WHERE email =@email and OTPCode=@otp and 
            //    (CONVERT(bigINT, cred.OTPDateTime) <= CONVERT(bigINT, @dtme) and 
            //    (CONVERT(bigINT, cred.OTPDateTime) + 5) >= CONVERT(bigINT,  @dtme))", new
            //{
            //    email = unm,
            //    otp = otp,
            //    dtme = serverTime.ToString("yyyyMMddHHmm")
            //}).ToList();
            //if (data.Count != 0)
            //{
            //    return Json(new { message = "valid" }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    return Json(new { message = "OTP Code is invalid!" }, JsonRequestBehavior.AllowGet);
            //}
            DateTime serverTime = DateTime.Now;
            var mytime = serverTime.ToString("yyyyMMddHHmm");
            long thistime = long.Parse(mytime);
            var data = conn.Query<_Login>(@"SELECT *
                FROM LRWEB_v1_CLIENT_ACCESS_ID as cred WHERE email =@email and OTPCode=@otp", new
            {
                email = unm,
                otp = otp
            }).ToList();
            if (data.Count != 0)
            {
                long expirytime = long.Parse(data[0].OTPDateTime) + 5;
                if ( expirytime >= thistime)
                {
                    return Json(new { message = "valid" }, JsonRequestBehavior.AllowGet);
                } else
                {
                    return Json(new { message = "OTP is expired!" }, JsonRequestBehavior.AllowGet);
                }
                
            }
            else
            {
                return Json(new { message = "OTP is invalid!" }, JsonRequestBehavior.AllowGet);
            }
            //return Json("", JsonRequestBehavior.AllowGet);
        }

    }
}
using Dapper;
using LRWEB_V1_CLIENT_SIDE_T24.Areas.LRForms.Models;
using LRWEB_V1_CLIENT_SIDE_T24.Areas.LRForms.ViewModel;
using Microsoft.Ajax.Utilities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Helpers;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using System.Xml;
using Newtonsoft.Json.Linq;
using System.Text;
using Microsoft.Ajax.Utilities;

namespace LRWEB_V1_CLIENT_SIDE_T24.Areas.LRForms.Controllers
{
    public class MessageController : Controller
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMBANKconnection"].ToString());
        MySqlConnection AELcon = new MySqlConnection(ConfigurationManager.ConnectionStrings["AELConnectiont24"].ToString());
        String Uristring = ConfigurationManager.ConnectionStrings["Uristringt24"].ToString();
        String userName = ConfigurationManager.ConnectionStrings["uname"].ToString();
        String passwd = ConfigurationManager.ConnectionStrings["pword"].ToString();
        
        
        // GET: LRForms/Message
        public ActionResult Index()
        {
            var dt = DateTime.Now;
            string dddd = dt.ToString("yyyyMMddHHmmss");
            Uri MyUrl = Request.Url;
            Session["Token"] = MyUrl.Query.ToString().Substring(MyUrl.Query.ToString().LastIndexOf('?') + 1);
            string token = Session["Token"].ToString();
            //string appno = conn.Query<string>(@"SELECT application_number FROM LRWEB_V1_CUSTOMER_APPLICATION WHERE token=@tokens", new
            //{
            //    tokens = token
            //}).Single();
            //Session["appno"] = appno;
            var res = conn.Query<int>(@"SELECT COUNT(*) FROM LRWEB_V1_CUSTOMER_APPLICATION WHERE token=@tokens and tokenExpire >= @datetimenow and status='FOR SIGNING'", new
            {
                tokens = Session["Token"].ToString(),
                datetimenow = dt.ToString("yyyyMMddHHmmss")
            }).Single();

            
            if (res == 0)
            {
                return RedirectToAction("ReferenceInvalid", "Message");
            }
            else
            {
                //var obj = AELcon.Query<_LRWEB_V1_AMORT_PREVIEW>(@"CALL usp_jaf_rpt_amort_sched(@appno)", new
                //{
                //    appno = appno
                //});
                //List<_LRWEB_V1_AMORT_PREVIEW> results = new List<_LRWEB_V1_AMORT_PREVIEW>();
                //if (obj != null)
                //{
                //    foreach (var list in obj)
                //    {
                //        _LRWEB_V1_AMORT_PREVIEW lr = new _LRWEB_V1_AMORT_PREVIEW();
                //        lr.term = list.term;
                //        lr.loan_amt = list.loan_amt;
                //        lr.principal = list.principal;
                //        lr.interest = list.interest;
                //        lr.monthly_amort = list.monthly_amort;
                //        lr.OS_balance = list.OS_balance;
                //        results.Add(lr);
                //    }
                //}

                
                //return View(files);
                string appno = conn.Query<string>(@"SELECT application_number FROM LRWEB_V1_CUSTOMER_APPLICATION WHERE token=@tokens", new
                {
                    tokens = token
                }).Single();
                Session["appno"] = appno;
                return View();
            }
        }
        public ActionResult ReferenceInvalid()
        {

            return View();
        }
        public ActionResult PrivacyNotice()
        {

            return View();
        }
        public ActionResult TermsAndConditions()
        {

            return View();
        }

        public ActionResult FormApplicationDone()
        {

            Uri MyUrl = Request.Url;
            MessageDone msres = new MessageDone();
            msres.URLParameters = GlobalVariableClass.DownloadMyPDFForm() + MyUrl.Query.ToString().Substring(MyUrl.Query.ToString().LastIndexOf('?') + 1);

            return View(msres);
        }

        public ActionResult AlreadyTaken()
        {

            return View();
        }

        public ActionResult LoanAdvices()
        {

            return View();
        }

        [HttpGet]
        public JsonResult getInfo()
        {
            string appno = Session["appno"].ToString();

            var obj = AELcon.Query<loan_details>(@"CALL usp_mgv_rpt_loan_advices_details(@appno)", new
            {
                appno = appno
            });
            List<loan_details> results = new List<loan_details>();
            if (obj != null)
            {
                foreach (var list in obj)
                {
                    loan_details lr = new loan_details();
                    lr.DATE_MATURED = list.DATE_MATURED;
                    lr.LAST_NAME = list.LAST_NAME;
                    lr.FIRST_NAME = list.FIRST_NAME;
                    lr.MIDDLE_NAME = list.MIDDLE_NAME;
                    lr.ADDRESS_1 = list.ADDRESS_1;
                    lr.LOAN_AMOUNT = list.LOAN_AMOUNT;
                    lr.TOTAL_CHARGES = list.TOTAL_CHARGES;
                    lr.TAXES = list.TAXES;
                    lr.PROCESSING_FEE = list.PROCESSING_FEE;
                    lr.NET_PROCEEDS = list.NET_PROCEEDS;
                    lr.START_PAY_DATE = list.START_PAY_DATE;
                    lr.NEW_DEDUCTION = list.NEW_DEDUCTION;
                    lr.TERM = list.TERM;
                    lr.COMPUTED_EIR = list.COMPUTED_EIR;
                    lr.INTEREST_RATE = list.INTEREST_RATE;
                    lr.BORROWER_NAME = list.BORROWER_NAME;
                    lr.DATE_GRANTED = list.DATE_GRANTED;
                    lr.CREDIT_ACCOUNT = list.CREDIT_ACCOUNT;
                    lr.VOUCHER_NUMBER = list.VOUCHER_NUMBER;
                    lr.AMOUNT_IN_WORDS = list.AMOUNT_IN_WORDS;
                    lr.ACCOUNT_NUMBER = list.ACCOUNT_NUMBER;
                    lr.CREATED_BY = list.CREATED_BY;
                    results.Add(lr);
                }
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getAmor()
        {

            var appno = Session["appno"].ToString();
            using (var client = new HttpClient())
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                var authToken = Encoding.ASCII.GetBytes($"{userName}:{passwd}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(authToken));
                var auth = new auth { grant_type = "client_credentials" };
                client.BaseAddress = new Uri(Uristring);
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsync("authentication", new StringContent(new JavaScriptSerializer().Serialize(auth), Encoding.UTF8, "application/json")).Result;

                var contents = response.Content.ReadAsStringAsync().Result;
                JObject res = JObject.Parse(contents);
                Session["auththoken"] = res["access_token"].ToString();

            }
            using (var client = new HttpClient())
            {
                var Bearer = Session["auththoken"].ToString();
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                var authToken = Encoding.ASCII.GetBytes($"{Bearer}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        Bearer);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(appno), "appno");
                client.BaseAddress = new Uri(Uristring);
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsync("getAmortSchedDetails", content).Result;
                
                var contents = response.Content.ReadAsStringAsync().Result;
                //var res = new ByteArrayContent(contents);
                //var decoded = DecodeUrlBase64(contents);
                //byte[] byteInfo = Convert.FromBase64String(decoded);

                //return res;
                return Json(contents, JsonRequestBehavior.AllowGet);

            }

        }

        public static byte[] DecodeUrlBase64(string s)
        {
            s = s.Replace('-', '+').Replace('_', '/').PadRight(4 * ((s.Length + 3) / 4), '=');
            return Convert.FromBase64String(s);
        }

        public JsonResult getDisc()
        {
            var appno = Session["appno"].ToString();
            using (var client = new HttpClient())
            {
                
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                var authToken = Encoding.ASCII.GetBytes($"{userName}:{passwd}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(authToken));
                var auth = new auth { grant_type = "client_credentials" };
                client.BaseAddress = new Uri(Uristring);
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsync("authentication", new StringContent(new JavaScriptSerializer().Serialize(auth), Encoding.UTF8, "application/json")).Result;

                var contents = response.Content.ReadAsStringAsync().Result;
                JObject res = JObject.Parse(contents);
                Session["auththoken"] = res["access_token"].ToString();

            }
            using (var client = new HttpClient())
            {
                var Bearer = Session["auththoken"].ToString();
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                var authToken = Encoding.ASCII.GetBytes($"{Bearer}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        Bearer);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(appno), "appno");
                client.BaseAddress = new Uri(Uristring);
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsync("previewDisclosureStatement", content).Result;

                var contents = response.Content.ReadAsStringAsync().Result;
                return Json(contents, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult getLoan()
        {
            var appno = Session["appno"].ToString();
            using (var client = new HttpClient())
            {
                
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                var authToken = Encoding.ASCII.GetBytes($"{userName}:{passwd}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(authToken));
                var auth = new auth { grant_type = "client_credentials" };
                client.BaseAddress = new Uri(Uristring);
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsync("authentication", new StringContent(new JavaScriptSerializer().Serialize(auth), Encoding.UTF8, "application/json")).Result;

                var contents = response.Content.ReadAsStringAsync().Result;
                JObject res = JObject.Parse(contents);
                Session["auththoken"] = res["access_token"].ToString();

            }
            using (var client = new HttpClient())
            {
                var Bearer = Session["auththoken"].ToString();
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                var authToken = Encoding.ASCII.GetBytes($"{Bearer}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        Bearer);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(appno), "appno");
                client.BaseAddress = new Uri(Uristring);
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsync("previewLoanRelease", content).Result;

                var contents = response.Content.ReadAsStringAsync().Result;
                return Json(contents, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult getProm()
        {
            var appno = Session["appno"].ToString();
            using (var client = new HttpClient())
            {
                
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                var authToken = Encoding.ASCII.GetBytes($"{userName}:{passwd}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(authToken));
                var auth = new auth { grant_type = "client_credentials" };
                client.BaseAddress = new Uri(Uristring);
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsync("authentication", new StringContent(new JavaScriptSerializer().Serialize(auth), Encoding.UTF8, "application/json")).Result;

                var contents = response.Content.ReadAsStringAsync().Result;
                JObject res = JObject.Parse(contents);
                Session["auththoken"] = res["access_token"].ToString();

            }
            using (var client = new HttpClient())
            {
                var Bearer = Session["auththoken"].ToString();
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                var authToken = Encoding.ASCII.GetBytes($"{Bearer}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        Bearer);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(appno), "appno");
                client.BaseAddress = new Uri(Uristring);
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsync("previewPromisorryNote", content).Result;

                var contents = response.Content.ReadAsStringAsync().Result;
                return Json(contents, JsonRequestBehavior.AllowGet);

            }
        }


        class loan_details
        {
            public string DATE_MATURED { get; set; }
            public string LAST_NAME { get; set; }
            public string FIRST_NAME { get; set; }
            public string MIDDLE_NAME { get; set; }
            public string ADDRESS_1 { get; set; }
            public string LOAN_AMOUNT { get; set; }
            public string TOTAL_CHARGES { get; set; }
            public string TAXES { get; set; }
            public string PROCESSING_FEE { get; set; }
            public string NET_PROCEEDS { get; set; }
            public string START_PAY_DATE { get; set; }
            public string NEW_DEDUCTION { get; set; }
            public string TERM { get; set; }
            public string COMPUTED_EIR { get; set; }
            public string INTEREST_RATE { get; set; }
            public string BORROWER_NAME { get; set; }
            public string DATE_GRANTED { get; set; }
            public string CREDIT_ACCOUNT { get; set; }
            public string VOUCHER_NUMBER { get; set; }
            public string AMOUNT_IN_WORDS { get; set; }
            public string ACCOUNT_NUMBER { get; set; }
            public string CREATED_BY { get; set; }

        }

        [HttpPost]
        public JsonResult submitFiles(string otp)
        {
            DateTime serverTime = DateTime.Now;
            TimeZoneInfo tst = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
            DateTime dt2 = TimeZoneInfo.ConvertTime(serverTime, TimeZoneInfo.Local, tst);
            string appno = Session["appno"].ToString();
            
            var sol_id = conn.Query<string>("SELECT sol_id from [LRWEB].[dbo].[LRWEB_V1_CUSTOMER_APPLICATION] where application_number=@appno", new
            {
                appno = appno
            }).Single();
            var cust_id = conn.Query<string>("SELECT CIFno from [LRWEB].[dbo].[LRWEB_V1_CUSTOMER_APPLICATION] where application_number=@appno", new
            {
                appno = appno
            }).Single();
            var address = conn.Query<string>("SELECT upper(concat(addressLine,' ',barangay,' ',city,' ',province)) as address from LRWEB_V1_CLIENTS where groupCode = (select groupCode from LRWEB_V1_CUSTOMER_APPLICATION where application_number = @appno)", new
            {
                appno = appno
            }).Single();

            using (var client = new HttpClient())
            {

                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                var authToken = Encoding.ASCII.GetBytes($"{userName}:{passwd}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(authToken));
                var auth = new auth { grant_type = "client_credentials" };
                client.BaseAddress = new Uri(Uristring);
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsync("authentication", new StringContent(new JavaScriptSerializer().Serialize(auth), Encoding.UTF8, "application/json")).Result;

                var contents = response.Content.ReadAsStringAsync().Result;
                JObject res = JObject.Parse(contents);
                Session["auththoken"] = res["access_token"].ToString();

            }
            using (var client = new HttpClient())
            {
                var Bearer = Session["auththoken"].ToString();
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                var authToken = Encoding.ASCII.GetBytes($"{Bearer}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        Bearer);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(appno), "appno");
                content.Add(new StringContent(sol_id), "sol_id");
                content.Add(new StringContent(cust_id), "cust_id");
                content.Add(new StringContent(otp), "digital_signed");
                content.Add(new StringContent(address), "company_address");
                client.BaseAddress = new Uri(Uristring);
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsync("acceptAdvices", content).Result;

                var contents = response.Content.ReadAsStringAsync().Result;
                JObject res = JObject.Parse(contents);
                try
                {
                    
                    string para = "appno :" + appno + ". sol_id :" + sol_id + ". cust_id :" + cust_id + ". digital_signed:" + otp + ". company_address :" + address + ".";
                    var strquery = "INSERT INTO API_LOGS (application_number,api,message,params,Date) values ('" + appno + "','acceptAdvices','" + contents + "','" + para + "','" + dt2 + "')";
                    conn.Execute(strquery);
                }
                catch (Exception)
                {
                    
                }
                
            }
            conn.Execute(@"UPDATE LRWEB_V1_CUSTOMER_APPLICATION SET status='SIGNED',dateSigned=getdate() where application_number=@appno", new
            {
                appno = appno
            });
            
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult requestOTP()
        {
            DateTime serverTime = DateTime.Now;
            TimeZoneInfo tst = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
            DateTime dt2 = TimeZoneInfo.ConvertTime(serverTime, TimeZoneInfo.Local, tst);
            var dt = dt2.AddMinutes(5).ToString("yyyyMMddHHmm");
            string appno = Session["appno"].ToString();
            var otpcode = GenerateStringCode();
            conn.Execute("UPDATE LRWEB_V1_CUSTOMER_APPLICATION SET OTP ='" + otpcode + "', OTPExpiration = '" + dt + "', sent_status = '00'  WHERE application_number = '" + appno + "'");
            var email = conn.Query<string>(@"select email from LRWEB_V1_CLIENT_EMPLOYEE where IntRecord = (Select employee_reference from LRWEB_V1_CUSTOMER_INFO where ID = (Select referenceNumber from LRWEB_V1_CUSTOMER_APPLICATION where token = @token))", new
            {
                token = Session["Token"].ToString()
            }).Single();


            try
            {
                //String Query
                var strquery = "INSERT INTO LRWEB_V1_OTP (OTP,email,application_number,Date) values ('" + otpcode + "','" + email + "','" + appno + "','" + dt2 + "')";
                conn.Execute(strquery);
            }
            catch (Exception)
            {

                
            }
            
            return Json(new { message = "An OTP has been sent to your company email address." }, JsonRequestBehavior.AllowGet);

        }



        [HttpPost]
        public JsonResult checkOTP(string otp)
        {
            string appno = Session["appno"].ToString();
            var checkOTP = conn.Query<int>(@"SELECT COUNT(*) FROM 
            [LRWEB].[dbo].[LRWEB_V1_CUSTOMER_APPLICATION] WHERE application_number=@appno and OTP=@OTP", new
            {
                appno = appno,
                OTP = otp
            }).Single();
            var email = conn.Query<string>(@"select email from LRWEB_V1_CLIENT_EMPLOYEE where IntRecord = (Select employee_reference from LRWEB_V1_CUSTOMER_INFO where ID = (Select referenceNumber from LRWEB_V1_CUSTOMER_APPLICATION where token = @token))", new
            {
                token = Session["Token"].ToString()
            });
            if (checkOTP != 0)
            {
                return Json(new { message = "valid" , email = email}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { message = "invalid" , email = email}, JsonRequestBehavior.AllowGet);
            }
        }

        class auth
        {
            public string grant_type { get; set; }
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
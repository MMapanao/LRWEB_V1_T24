using Dapper;
using LRWEB_V1_CLIENT_SIDE_T24.Areas.HumanResource.LogCode;
using LRWEB_V1_CLIENT_SIDE_T24.Areas.HumanResource.LogCode.LogsModel;
using LRWEB_V1_CLIENT_SIDE_T24.Areas.HumanResource.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LRWEB_V1_CLIENT_SIDE_T24.Areas.HumanResource.Controllers
{
    public class PendingsController : Controller
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMBANKconnection"].ToString());
        SqlConnection connBankAlerts = new SqlConnection(ConfigurationManager.ConnectionStrings["BANKALERTSconnection"].ToString());
        DateTime dateTime = DateTime.UtcNow.Date;
        // GET: HumanResource/Pendings
 
        public ActionResult Index()
        {
            if (Request.Cookies["ASP_NET_TKT"] == null || Request.Cookies["ASP_NET_CD"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                string ticket = Request.Cookies["ASP_NET_TKT"].Value.ToString(), 
                    cred = Request.Cookies["ASP_NET_CD"].Value.ToString();
                if (GlobalVariableClass.Authenticated(ticket, cred) == false)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                     
                }
            }

            string[] arrcred = GlobalVariableClass.Base64Decode(GlobalVariableClass.Base64Decode(Request.Cookies["ASP_NET_CD"].Value.ToString())).Split(',');
            var obj = conn.Query<_NEW_EMPLOYEE>(@"SELECT * FROM LRWEB_V1_CLIENT_EMPLOYEE where GroupCode=@GroupCode", new {
                GroupCode = arrcred[1].ToString()
            }).ToList();
            List<_NEW_EMPLOYEE> results = new List<_NEW_EMPLOYEE>();
            if(obj != null)
            {
                foreach(var list in obj)
                {
                    _NEW_EMPLOYEE lr = new _NEW_EMPLOYEE();
                      lr.IntRecord = list.IntRecord;
                      lr.GroupCode = list.GroupCode;
                     lr.last_name = list.last_name;
                     lr.first_name = list.first_name;
                     lr.middle_name = list.middle_name;
                     lr.contactnum = list.contactnum;
                     lr.email = list.email;
                     lr.Tokens = list.Tokens;
                     lr.Expiration = list.Expiration;
                     lr.Status = list.Status;
                     lr.Client_Branch = list.Client_Branch;
                     lr.SubmitStatus = list.SubmitStatus;
                     lr.scheme_code = list.scheme_code;
                    results.Add(lr);
                }
            }
            return View(results);
        }


        [HttpPost] 
        public JsonResult GetReviewDATA(string thisid)
        {


            var obj = conn.Query<EMPLOYEE_INFO>(@"SELECT EC.*,ECE.scheme_code FROM
            EPAY_V2_CUSTOMER  AS EC INNER JOIN 
            [TeamBank].[dbo].[EPAY_V2_CLIENT_EMPLOYEE] AS ECE
            on EC.employee_reference = ECE.INTRECORD where EC.employee_reference=@employee", new {
                employee = conn.Query<string>(@"SELECT IntRecord FROM EPAY_V2_CLIENT_EMPLOYEE WHERE Tokens=@tokens", new { tokens = thisid }).Single()
            }).Single();
                     obj.INTRECORD = GlobalVariableClass.EmptyIfNullString(obj.INTRECORD);
                    obj.Salutation = GlobalVariableClass.EmptyIfNullString(obj.Salutation);
                    obj.lastname = GlobalVariableClass.EmptyIfNullString(obj.lastname);
                    obj.firstname = GlobalVariableClass.EmptyIfNullString(obj.firstname);
                    obj.middlename = GlobalVariableClass.EmptyIfNullString(obj.middlename);
                    obj.Present_Address = GlobalVariableClass.EmptyIfNullString(obj.Present_Address);
                    obj.Present_City = GlobalVariableClass.EmptyIfNullString(obj.Present_City);
                    obj.Present_State = GlobalVariableClass.EmptyIfNullString(obj.Present_State);
                    obj.Present_ZipCode = GlobalVariableClass.EmptyIfNullString(obj.Present_ZipCode);
                    obj.Present_Country = GlobalVariableClass.EmptyIfNullString(obj.Present_Country);
                    obj.Permanent_Address = GlobalVariableClass.EmptyIfNullString(obj.Permanent_Address);
                    obj.Permanent_City = GlobalVariableClass.EmptyIfNullString(obj.Permanent_City);
                    obj.Permanent_State = GlobalVariableClass.EmptyIfNullString(obj.Permanent_State);
                    obj.Permanent_ZipCode = GlobalVariableClass.EmptyIfNullString(obj.Permanent_ZipCode);
                    obj.Permanent_Country = GlobalVariableClass.EmptyIfNullString(obj.Permanent_Country);
                    obj.Email_Address = GlobalVariableClass.EmptyIfNullString(obj.Email_Address);
                    obj.ContactNumber = GlobalVariableClass.EmptyIfNullString(obj.ContactNumber);
                    obj.PhoneBrand = GlobalVariableClass.EmptyIfNullString(obj.PhoneBrand);
                    obj.PhoneModel = GlobalVariableClass.EmptyIfNullString(obj.PhoneModel);
                    obj.BirthDate = GlobalVariableClass.EmptyIfNullString(obj.BirthDate);
                    obj.BirthPlace = GlobalVariableClass.EmptyIfNullString(obj.BirthPlace);
                    obj.Nationality = GlobalVariableClass.EmptyIfNullString(obj.Nationality);
                    obj.Gender = GlobalVariableClass.EmptyIfNullString(obj.Gender);
                    obj.Civil_Status = GlobalVariableClass.EmptyIfNullString(obj.Civil_Status);
                    obj.SpouseName = GlobalVariableClass.EmptyIfNullString(obj.SpouseName);
                    obj.Mother_MaidenName = GlobalVariableClass.EmptyIfNullString(obj.Mother_MaidenName);
                    obj.SSS = GlobalVariableClass.EmptyIfNullString(obj.SSS);
                    obj.GSIS = GlobalVariableClass.EmptyIfNullString(obj.GSIS);
                    obj.TIN = GlobalVariableClass.EmptyIfNullString(obj.TIN);
                    obj.Beneficial_Owners = GlobalVariableClass.EmptyIfNullString(obj.Beneficial_Owners);
                    obj.Monthly_Transaction = GlobalVariableClass.EmptyIfNullString(obj.Monthly_Transaction);
                    obj.Source_Funds = GlobalVariableClass.EmptyIfNullString(
                        connBankAlerts.Query<string>(@"SELECT SourceOfFunds FROM  tblSourceofFunds
                        WHERE Code=@Code",new { Code = obj.Source_Funds }).Single()
                        ); 
                    obj.NatureWorkBusiness = connBankAlerts.Query<string>(@"SELECT TOP 1 [NatureOfWork]
                    FROM tblNatureofWork WHERE CODE=@code", new {
                        code = GlobalVariableClass.EmptyIfNullString(obj.NatureWorkBusiness)
                    }).Single();
                    obj.EmployerBusiness = GlobalVariableClass.EmptyIfNullString(obj.EmployerBusiness);
                    obj.Designation = connBankAlerts.Query<string>(@"SELECT TOP 1 NAME FROM 
                    tblOccupation WHERE code=@code", new {
                        code = GlobalVariableClass.EmptyIfNullString(obj.Designation)
                    }).Single();
                    obj.BusinessPhoneNo = GlobalVariableClass.EmptyIfNullString(obj.BusinessPhoneNo);
                    obj.FaxNo = GlobalVariableClass.EmptyIfNullString(obj.FaxNo);
                    obj.Business_Address = GlobalVariableClass.EmptyIfNullString(obj.Business_Address);
                    obj.Business_Country = GlobalVariableClass.EmptyIfNullString(obj.Business_Country);
                    obj.Business_City = GlobalVariableClass.EmptyIfNullString(obj.Business_City);
                    obj.Business_State = GlobalVariableClass.EmptyIfNullString(obj.Business_State);
                    obj.Business_ZipCode = GlobalVariableClass.EmptyIfNullString(obj.Business_ZipCode);
                    obj.OverSeas_Address = GlobalVariableClass.EmptyIfNullString(obj.OverSeas_Address);
                    obj.OverSeas_City = GlobalVariableClass.EmptyIfNullString(obj.OverSeas_City);
                    obj.OverSeas_State = GlobalVariableClass.EmptyIfNullString(obj.OverSeas_State);
                    obj.OverSeas_Country = GlobalVariableClass.EmptyIfNullString(obj.OverSeas_Country);
                    obj.OverSeas_PostalCode = GlobalVariableClass.EmptyIfNullString(obj.OverSeas_PostalCode);
                    obj.OverSeas_Tax_ID_No = GlobalVariableClass.EmptyIfNullString(obj.OverSeas_Tax_ID_No);
                    obj.Overseas_CountryCode = GlobalVariableClass.EmptyIfNullString(obj.Overseas_CountryCode);
                    obj.Overseas_AreaCode = GlobalVariableClass.EmptyIfNullString(obj.Overseas_AreaCode);
                    obj.Overseas_PhoneNo = GlobalVariableClass.EmptyIfNullString(obj.Overseas_PhoneNo);
                    obj.PasspportNo = GlobalVariableClass.EmptyIfNullString(obj.PasspportNo);
                    obj.ExpiryDate = GlobalVariableClass.EmptyIfNullString(obj.ExpiryDate);
                    obj.PlaceIssue = GlobalVariableClass.EmptyIfNullString(obj.PlaceIssue);
                    obj.OthersID = GlobalVariableClass.EmptyIfNullString(obj.OthersID);
                    obj.CUSTID = GlobalVariableClass.EmptyIfNullString(obj.CUSTID);
                    obj.ACCOUNTNO = GlobalVariableClass.EmptyIfNullString(obj.ACCOUNTNO);
                    obj.GroupCode = GlobalVariableClass.EmptyIfNullString(obj.GroupCode);
                    obj.ApprovalStatus = GlobalVariableClass.EmptyIfNullString(obj.ApprovalStatus);
                    obj.employee_reference = GlobalVariableClass.EmptyIfNullString(obj.employee_reference);
                    obj.Overseas_CountryCode = GlobalVariableClass.EmptyIfNullString(obj.Overseas_CountryCode);
                    obj.Overseas_AreaCode = GlobalVariableClass.EmptyIfNullString(obj.Overseas_AreaCode);
                    obj.Overseas_PhoneNo = GlobalVariableClass.EmptyIfNullString(obj.Overseas_PhoneNo);
                    obj.TIN = GlobalVariableClass.EmptyIfNullString(obj.TIN);
                    obj.scheme_code = GlobalVariableClass.EmptyIfNullString(obj.scheme_code);
                    return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeclineUser(string thisid, string txtreason)
        {
            conn.Execute(@"UPDATE EPAY_V2_CLIENT_EMPLOYEE SET SubmitStatus='00', reason_decline=@decline WHERE IntRecord=@Tokens", new {
                Tokens = thisid,
                decline = txtreason
            });
            _ACTIONS action = new _ACTIONS();
            action.EnrolledID = new List<string>();
            LOGSFILE logsfile = new LOGSFILE();
            _LOGS logs = new _LOGS();
            var cred = GlobalVariableClass.Base64Decode(GlobalVariableClass.Base64Decode(Request.Cookies["ASP_NET_CD"].Value.ToString())).Split(',');
            logs.UserCode = cred[0];
            logs.GroupCode = cred[1];
            logs.email = cred[6];
            action.Event = "DECLINED";
            action.Module = "PENDING EMPLOYEES";
            action.ID = thisid;
            action.time = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            action.Before = "";
            action.After = "";
            action.DeclineReason = txtreason;
            logs.Action = action;
            logsfile.ActionLog(logs);
            return Json("",JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult editInfo(string firstname, string middlename, string lastname, string contact, string email, string intrecord)
        {
            conn.Execute(@"UPDATE LRWEB_V1_CLIENT_EMPLOYEE SET first_name=@firstname, middle_name=@middlename, last_name=@lastname, contactnum=@contact, email=@email WHERE IntRecord=@Tokens", new
            {
                Tokens = intrecord,
                firstname = firstname,
                middlename = middlename,
                lastname = lastname,
                contact = contact,
                email = email
            });

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ApproveEmployee(string thisid)
        {
            conn.Execute(@"UPDATE EPAY_V2_CLIENT_EMPLOYEE SET SubmitStatus='11' WHERE IntRecord=@Tokens", new
            {
                Tokens = thisid
            });
            _ACTIONS action = new _ACTIONS();
            action.EnrolledID = new List<string>();
            LOGSFILE logsfile = new LOGSFILE();
            _LOGS logs = new _LOGS();
            var cred = GlobalVariableClass.Base64Decode(GlobalVariableClass.Base64Decode(Request.Cookies["ASP_NET_CD"].Value.ToString())).Split(',');
            logs.UserCode = cred[0];
            logs.GroupCode = cred[1];
            logs.email = cred[6];
            action.Event = "APPROVED";
            action.Module = "PENDING EMPLOYEES";
            action.ID = thisid;
            action.time = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            action.Before = "";
            action.After = "";
            action.DeclineReason = "";
            logs.Action = action;
            logsfile.ActionLog(logs);
            return Json("", JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult ResendEmployee(string thisid)
        {
            string[] arrcred = GlobalVariableClass.Base64Decode(GlobalVariableClass.Base64Decode(Request.Cookies["ASP_NET_CD"].Value.ToString())).Split(',');
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            dt = dt.AddDays(7);
            string token = "";
            token = GlobalVariableClass.RandomString(72);
            var getdate = conn.Query<string>(@"SELECT Expiration from LRWEB_V1_CLIENT_EMPLOYEE
            WHERE  IntRecord=@Tokens", new {
                Tokens = thisid
            }).Single();
            conn.Execute(@"UPDATE LRWEB_V1_CLIENT_EMPLOYEE SET Status='0', Expiration=@expiration, tokenSender=@tokenSender, Tokens=@Tokens WHERE IntRecord=@id", new
            {
                id = thisid,
                expiration = dt.ToString("yyyyMMddHHmmss"),
                tokenSender = arrcred[6].ToString(),
                Tokens = token
            });
            //_ACTIONS action = new _ACTIONS();
            //action.EnrolledID = new List<string>();
            //LOGSFILE logsfile = new LOGSFILE();
            //_LOGS logs = new _LOGS();
            //var cred = GlobalVariableClass.Base64Decode(GlobalVariableClass.Base64Decode(Request.Cookies["ASP_NET_CD"].Value.ToString())).Split(',');
            //logs.UserCode = cred[0];
            //logs.GroupCode = cred[1];
            //logs.email = cred[6];
            //action.Event = "RESEND";
            //action.Module = "PENDING EMPLOYEES";
            //action.ID = thisid;
            //action.time = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            //action.Before = getdate;
            //action.After = dt.ToString("yyyyMMddHHmmss");
            //logs.Action = action;
            //logsfile.ActionLog(logs);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SearchData(string txtsearch)
        {
            string[] arrcred = GlobalVariableClass.Base64Decode(GlobalVariableClass.Base64Decode(Request.Cookies["ASP_NET_CD"].Value.ToString())).Split(',');
            var obj = conn.Query<_NEW_EMPLOYEE>(@"SELECT * FROM EPAY_V2_CLIENT_EMPLOYEE where
            (last_name like @lname or first_name like @fname or email like @email)
            AND  GroupCode=@GroupCode", new
            {
                GroupCode = arrcred[1].ToString(),
                lname = "%" + txtsearch + "%",
                fname = "%" + txtsearch + "%",
                email = "%" + txtsearch + "%"
            }).ToList();
            List<_NEW_EMPLOYEE> results = new List<_NEW_EMPLOYEE>();
            if (obj != null)
            {
                foreach (var list in obj)
                {
                    _NEW_EMPLOYEE lr = new _NEW_EMPLOYEE();
                    lr.IntRecord = list.IntRecord;
                    lr.GroupCode = list.GroupCode;
                    lr.last_name = list.last_name;
                    lr.first_name = list.first_name;
                    lr.middle_name = list.middle_name;
                    lr.contactnum = list.contactnum;
                    lr.email = list.email;
                    lr.Tokens = list.Tokens;
                    lr.Expiration = list.Expiration;
                    lr.Status = list.Status;
                    lr.Client_Branch = list.Client_Branch;
                    lr.SubmitStatus = list.SubmitStatus;
                    lr.scheme_code = list.scheme_code;
                    results.Add(lr);
                }
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}
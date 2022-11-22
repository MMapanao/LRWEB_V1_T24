using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using System.Web.Mvc;
using LRWEB_V1_ADMIN_T24.Areas.Admin.Models;
using LRWEB_V1_ADMIN_T24.Areas.Admin.ViewModel;
using System.Text.RegularExpressions;

namespace LRWEB_V1_ADMIN_T24.Areas.Admin.Controllers
{
    public class VerifyClientsController : Controller
    {
        DateTime dateTime = DateTime.UtcNow.Date;
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMBANKconnection"].ToString());
        SqlConnection connBankAlerts = new SqlConnection(ConfigurationManager.ConnectionStrings["BANKALERTSconnection"].ToString());
 
        // GET: Admin/VerifyClients
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
             FROM LRWEB_V1_CLIENTS as cl WHERE status='PENDING' ORDER BY ID DESC").ToList();
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
                        model.encode_by = row.encode_by.ToUpper();
                    } else
                    {
                        model.encode_by = "Deleted";
                    }
                    
                    result_asd.Add(model);
                }
            }
            return View(result_asd);
        }

        [HttpPost]
        public JsonResult ApproveClients(string model_state)
        {
            var arrcred = GlobalVariableClass.Base64Decode(GlobalVariableClass.Base64Decode(Session["cred"].ToString())).Split(',');
            var groupCode = "";
            var mstate = model_state.Split(',');
            for (int x = 0; x < mstate.Length; x++)
            {
                conn.Execute(@"UPDATE LRWEB_V1_CLIENTS SET Status=@status,approvedBy=@user,dateApproved=getDate() 
                    WHERE groupCode=@gcode", new
                {
                    status = "VERIFIED",
                    user = arrcred[2].ToString() + " " + arrcred[1].ToString(),
                    gcode = mstate[x]
                });
                groupCode = mstate[x];
            }
            string done = "Verified Group Code " + groupCode;
            logit(done);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ReturnToMaker(string model_state)
        {
            var split1 = model_state.Split('|');
            var mstate = split1[0].Split(',');
            var groupCode = "";
            for (int x = 0; x < mstate.Length; x++)
            {
                conn.Execute(@"UPDATE LRWEB_V1_CLIENTS SET Status=@status, 
                    remarks=@remarks,dateUpdated = getdate()
                    WHERE GroupCode=@gcode", new
                {
                    status = "RETURNED",
                    gcode = mstate[x],
                    remarks = split1[1]
                });
                groupCode = mstate[x];
            }
            string done = "Returned Group Code " + groupCode;
            logit(done);
            return Json("", JsonRequestBehavior.AllowGet);
        }


        public ActionResult GroupCodeDetails()
        {
            var province_code = "";
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
            if (Request.QueryString["GroupCode"] == null)
            {
                if(Request.QueryString["content"] == "PendingClients")
                {
                    return RedirectToAction(GlobalVariableClass.FolderName + "/Admin/VerifyClients/Index?content=PendingClients");
                }
                else
                {
                    return RedirectToAction(GlobalVariableClass.FolderName + "/Admin/ReturnEntries/Index?content=ReturnEntries");
                }

            }

            var obj = conn.Query<_EPAY_V2_CLIENTS>(@"Select *,FORMAT(AED,'MMM dd yyyy') as AED from LRWEB_V1_CLIENTS
            WHERE groupCode=@groupCode", new {
                groupcode = Request.QueryString["GroupCode"]
            }).ToList();
            List<_EPAY_V2_CLIENTS> result_clients = new List<_EPAY_V2_CLIENTS>();
            if(obj != null)
            {
                foreach(var row in obj)
                {
                    _EPAY_V2_CLIENTS sub_row = new _EPAY_V2_CLIENTS();
                    sub_row.ID = row.ID;

                    sub_row.AED = row.AED;

                    sub_row.clientName = row.clientName;

                    sub_row.addressLine = row.addressLine;

                    sub_row.barangay = row.barangay;

                    sub_row.province = row.province;
                    //GlobalVariableClass.IFNullEmptyString(connBankAlerts.Query<string>(@"
                    //SELECT tblProvince FROM tblSourceofFunds WHERE
                    //Code=@code", new { code = row.province }).ToList()[0]);

                    sub_row.city = row.city;
                    //GlobalVariableClass.IFNullEmptyString(connBankAlerts.Query<string>(@"
                    //SELECT tblCity FROM tblSourceofFunds WHERE
                    //Code=@code", new { code = row.city }).ToList()[0]);


                    sub_row.mobileNumber = row.mobileNumber;

                    sub_row.officeNumber = row.officeNumber;

                    sub_row.emailAddress = row.emailAddress;

                    sub_row.classification = row.classification;

                    sub_row.structure = row.structure;

                    sub_row.customerID = row.customerID;

                    sub_row.accountNumber = row.accountNumber;

                    sub_row.csbBranch = row.csbBranch;

                    sub_row.schemeCode = row.schemeCode;

                    sub_row.agencyCode = row.agencyCode;

                    sub_row.groupCode = row.groupCode;

                    sub_row.emailFormat = row.emailFormat;

                    sub_row.creditRatio = row.creditRatio;
                    result_clients.Add(sub_row);
                    //sub_row.AccountType = row.AccountType;
                    //sub_row.MaintainingBranch = row.MaintainingBranch;
                    //sub_row.SourceFunds = GlobalVariableClass.IFNullEmptyString(connBankAlerts.Query<string>(@"
                    //SELECT SourceOfFunds FROM tblSourceofFunds WHERE
                    //Code=@code", new { code = row.SourceFunds }).ToList()[0]);
                    //sub_row.GroupCode = row.GroupCode;
                    ////sub_row.CardType = row.CardType;
                    //sub_row.ServicingBranch = row.ServicingBranch;
                    //sub_row.CardDesignType = row.CardDesignType;
                    //sub_row.PictureSize = row.PictureSize;
                    //sub_row.ApplicationDate = row.ApplicationDate;
                    //sub_row.SiteName = row.SiteName;
                    //sub_row.NoStreet = row.NoStreet;
                    //sub_row.DistrictBarangay = row.DistrictBarangay;
                    //sub_row.CityProvince = row.CityProvince;
                    //sub_row.ZipCode = row.ZipCode;
                    //sub_row.phonenumber = row.phonenumber;
                    //sub_row.status = row.status;
                    //sub_row.remarks = row.remarks;
                    //sub_row.CardDeliverySol = conn.Query<string>(@"SELECT CTR.BR_DESC
                    //FROM EPAY_V2_CLIENTS_CARDDELIVERYSOL AS CD
                    //INNER JOIN FIN_PROFIT_CTR AS CTR ON CD.BranchCode = CTR.BR_CODE WHERE GroupCode=@gcode", new {
                    //    gcode = Request.QueryString["GroupCode"].ToString()
                    //}).ToList();
                    //sub_row.Province = connBankAlerts.Query<string>(@"SELECT name FROM [BankAlerts].[dev].[tblProvince] WHERE code=@code", new {
                    //    code = row.ZipCode.Substring(0,3)
                    //}).Single();
                    //sub_row.City   = connBankAlerts.Query<string>(@"SELECT name FROM [BankAlerts].[dev].[tblCity] WHERE code=@code", new
                    //{
                    //    code = row.ZipCode.Trim()
                    //}).Single();
                    //sub_row.Province = row.Province;
                    //sub_row.City = row.City;
                    //result_clients.Add(sub_row);
                    //province_code = GlobalVariableClass.IFNullEmptyString(connBankAlerts.Query<string>(@"
                    //SELECT province_code FROM tblCity WHERE
                    //Code=@code", new { code = row.ZipCode }).ToList()[0]);
                }
            }
            obj = result_clients;
            List<_INFO_TABLE> result_infotable = new List<_INFO_TABLE>();

            if (obj != null)
            {
                foreach (var row in obj)
                {

                    _INFO_TABLE info = new _INFO_TABLE();
                    //info.title = "ID Number";
                    //info.info = row.ID;
                    //result_infotable.Add(info);

                    //info = new _INFO_TABLE();
                    info.title = "Agreement Effectivity Date";
                    info.info = row.AED;
                    result_infotable.Add(info);

                    info = new _INFO_TABLE();
                    info.title = "Client Name";
                    info.info = row.clientName;
                    result_infotable.Add(info);

                    info = new _INFO_TABLE();
                    info.title = "Business Address";
                    info.info = row.addressLine + " BRGY." + row.barangay + " " + row.city + ", " + row.province;
                    result_infotable.Add(info);

                    info = new _INFO_TABLE();
                    info.title = "Mobile Number";
                    info.info = row.mobileNumber;
                    result_infotable.Add(info);

                    info = new _INFO_TABLE();
                    info.title = "Office Number";
                    info.info = row.officeNumber;
                    result_infotable.Add(info);

                    info = new _INFO_TABLE();
                    info.title = "Email Address";
                    info.info = row.emailAddress;
                    result_infotable.Add(info);

                    info = new _INFO_TABLE();
                    info.title = "Institutional classification";
                    info.info = row.classification;
                    result_infotable.Add(info);

                    info = new _INFO_TABLE();
                    info.title = "Business structure";
                    info.info = row.structure;
                    result_infotable.Add(info);

                    info = new _INFO_TABLE();
                    info.title = "Email format";
                    info.info = row.emailFormat;
                    result_infotable.Add(info);

                    //info = new _INFO_TABLE();
                    //info.title = "Account number";
                    //info.info = row.accountNumber;
                    //result_infotable.Add(info);

                    //info = new _INFO_TABLE();
                    //info.title = "Customer ID";
                    //info.info = row.customerID;
                    //result_infotable.Add(info);

                    //info = new _INFO_TABLE();
                    //info.title = "CSB Branch";
                    //info.info = row.csbBranch;
                    //result_infotable.Add(info);

                    //info = new _INFO_TABLE();
                    //info.title = "Scheme Code";
                    //info.info = row.schemeCode;
                    //result_infotable.Add(info);

                    //info = new _INFO_TABLE();
                    //info.title = "Email format";
                    //info.info = row.emailFormat;
                    //result_infotable.Add(info);
                    ///////////////////////////////////
                    //info.title = "ID Number";
                    //info.info = row.IntRecord;
                    //result_infotable.Add(info);

                    //info = new _INFO_TABLE();
                    //info.title = "Company Name";
                    //info.info = row.CompanyName;
                    //result_infotable.Add(info);

                    //info = new _INFO_TABLE();
                    //info.title = "Company Email";
                    //info.info = row.CompanyEmail;
                    //result_infotable.Add(info);

                    //info = new _INFO_TABLE();
                    //info.title = "Employer ID";
                    //info.info = row.EmployerID;
                    //result_infotable.Add(info);

                    //info = new _INFO_TABLE();
                    //info.title = "Customer ID";
                    //info.info = row.CustomerID;
                    //result_infotable.Add(info);

                    //info = new _INFO_TABLE();
                    //info.title = "Account #";
                    //info.info = row.AccountNo;
                    //result_infotable.Add(info);

                    //info = new _INFO_TABLE();
                    //info.title = "Account Type";
                    //info.info = row.AccountType;
                    //result_infotable.Add(info);

                    //info = new _INFO_TABLE();
                    //info.title = "Servicing Branch";
                    //info.info = conn.Query<string>(@"SELECT BR_DESC FROM FIN_PROFIT_CTR WHERE BR_CODE=@code", new {
                    //    code = row.ServicingBranch
                    //}).Single() + " - " + row.ServicingBranch;
                    //result_infotable.Add(info);

                    //info = new _INFO_TABLE();
                    //info.title = "Source Funds";
                    //info.info = row.SourceFunds;
                    //result_infotable.Add(info);

                    //info = new _INFO_TABLE();
                    //info.title = "Group Code";
                    //info.info = row.GroupCode;
                    //result_infotable.Add(info);

                    //info = new _INFO_TABLE();
                    //info.title = "RM Code";
                    //info.info = row.RMCode;
                    //result_infotable.Add(info);

                    //info = new _INFO_TABLE();
                    //info.title = "Card Type";
                    //info.info = row.CardType;
                    //result_infotable.Add(info);




                    //info = new _INFO_TABLE();
                    //info.title = "Business Type";
                    //info.info = row.CardDesignType;
                    //result_infotable.Add(info);

                    //info = new _INFO_TABLE();
                    //info.title = "Picture Size";
                    //info.info = row.PictureSize;
                    //result_infotable.Add(info);

                    //info = new _INFO_TABLE();
                    //info.title = "Site Name";
                    //info.info = row.SiteName;
                    //result_infotable.Add(info);

                    //info = new _INFO_TABLE();
                    //info.title = "Street";
                    //info.info = row.NoStreet;
                    //result_infotable.Add(info);

                    //info = new _INFO_TABLE();
                    //info.title = "Barangay/District";
                    //info.info =connBankAlerts.Query<string>(@"SELECT name FROM tblCity WHERE code=@code", new
                    //{
                    //    code = row.ZipCode.Trim() 
                    //}).Single();
                    //result_infotable.Add(info);

                    //info = new _INFO_TABLE();
                    //info.title = "Province/City";
                    //info.info = connBankAlerts.Query<string>(@"SELECT name FROM tblProvince WHERE code=@code", new
                    //{
                    //    code = province_code
                    //}).Single();
                    //result_infotable.Add(info);




                    //info = new _INFO_TABLE();
                    //info.title = "Zip Code";
                    //info.info = row.ZipCode;
                    //result_infotable.Add(info);

                    //info = new _INFO_TABLE();
                    //info.title = "Mobile Number";
                    //info.info = row.phonenumber;
                    //result_infotable.Add(info);

                    //info = new _INFO_TABLE();
                    //info.title = "Status";
                    //info.info = row.status;
                    //result_infotable.Add(info);

                    //int x = 0;
                    //foreach (var rrr in row.CardDeliverySol)
                    //{
                    //    if (x == 0)
                    //    {
                    //        info = new _INFO_TABLE();
                    //        info.title = "Branches";
                    //        info.info = rrr;
                    //        result_infotable.Add(info);
                    //    }
                    //    else
                    //    {
                    //        info = new _INFO_TABLE();
                    //        info.title = "";
                    //        info.info = rrr;
                    //        result_infotable.Add(info);
                    //    }
                    //    x++;
                    //}


                }
            }

            //var obj_scheme = conn.Query<_SCHEME_CODE>(@"SELECT sc.*,
            //(Select scl.Definition from EPAY_V2_SCHEMECODE_LIST as scl where 
            //scl.SchemeCodes = sc.Schemecode) as Definitions
            //FROM EPAY_V2_SCHEMECODE as sc where groupcode=@groupcode", new
            //{
            //    groupcode = Request.QueryString["GroupCode"]
            //}).ToList();
            //List<_SCHEME_CODE> result_sc = new List<_SCHEME_CODE>();
            //if (obj_scheme != null)
            //{
            //    foreach (var sc in obj_scheme)
            //    {
            //        _SCHEME_CODE temp = new _SCHEME_CODE();
            //        temp.IntRecord = sc.IntRecord;
            //        temp.Schemecode = sc.Schemecode;
            //        temp.groupcode = sc.groupcode;
            //        temp.Definitions = sc.Definitions;
            //        result_sc.Add(temp);
            //    }
            //}


            //var obj_branch = conn.Query<_BRANCHES>(@"SELECT  *
            //FROM EPAY_V2_CLIENTS_BRANCH where GroupCode = @groupcode and Status<>@status", new
            //{
            //    groupcode = Request.QueryString["GroupCode"],
            //    status = "DELETE"
            //}).ToList();
            //List<_BRANCHES> result_branch = new List<_BRANCHES>();
            //if (obj_branch != null)
            //{
            //    foreach (var row in obj_branch)
            //    {
            //        _BRANCHES evcb = new _BRANCHES();
            //        evcb.IntRecord = row.IntRecord;
            //        evcb.CompanyID = row.CompanyID;
            //        evcb.SiteName = row.SiteName;
            //        evcb.Street = row.Street;
            //        evcb.District = row.District;
            //        evcb.City = row.City;
            //        evcb.ZipCode = row.ZipCode;
            //        evcb.Status = row.Status;
            //        evcb.GroupCode = Request.QueryString["GroupCode"];
            //        result_branch.Add(evcb);
            //    }
            //}

            //var obj_hrclient = conn.Query<_EPAY_V2_CLIENT_ACCESS_ID>(@"
            //SELECT * FROM EPAY_V2_CLIENT_ACCESS_ID WHERE GroupCode=@GroupCode", new {
            //    GroupCode = Request.QueryString["GroupCode"]
            //}).ToList();
            //List<_EPAY_V2_CLIENT_ACCESS_ID> result_hr = new List<_EPAY_V2_CLIENT_ACCESS_ID>();
            //if(obj_hrclient != null)
            //{
            //    foreach(var hr in obj_hrclient)
            //    {
            //        _EPAY_V2_CLIENT_ACCESS_ID epay = new _EPAY_V2_CLIENT_ACCESS_ID();
            //        epay.IntRecord = hr.IntRecord;
            //        epay.company_id = hr.company_id;
            //        epay.last_name = hr.last_name;
            //        epay.first_name = hr.first_name;
            //        epay.middle_name = hr.middle_name;
            //        epay.contact_no = hr.contact_no;
            //        epay.email = hr.email;
            //        epay.maker = hr.maker;
            //        epay.companyname = hr.companyname;
            //        epay.GroupCode = hr.GroupCode;
            //        epay.changepass = hr.changepass;
            //        epay.status = hr.status;
            //        result_hr.Add(epay);
            //    }
            //}

            ViewModel_NewGroupCode vm = new ViewModel_NewGroupCode();
            vm.vm_EPAY_V2_CLIENTS = result_clients;
            //vm.vm_scheme_code = result_sc;
            vm.vm_info_table = result_infotable;
            //vm.vm_branches = result_branch;
            //vm.vm_access_id = result_hr;

            return View(vm);
        }


        public ActionResult NewClientAccess()
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
            if (Request.QueryString["GroupCode"] == null)
            {
                return Redirect(GlobalVariableClass.FolderName+ "/Admin/ClientAccess/Index?content=ClientAccess");
            }
            var obj = conn.Query<_EPAY_V2_CLIENTS>(@"SELECT * 
            FROM EPAY_V2_CLIENTS where GroupCode=@GroupCode", new {
                GroupCode = Request.QueryString["GroupCode"]
            }).Single();
            _EPAY_V2_CLIENTS epp = new _EPAY_V2_CLIENTS();
            if(obj != null)
            {
                epp.clientName = obj.clientName;
                epp.groupCode = obj.groupCode;
            }

            return View(epp);
        }

        public bool isValidEmail(string email)
        {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(email);
                 //   return addr.Address == email;
                return false;
                }
                catch
                {
                    return true;
                }
        }

        public void logit(string done)
        {
            var arrcred = GlobalVariableClass.Base64Decode(GlobalVariableClass.Base64Decode(Session["cred"].ToString())).Split(',');
            var unm = arrcred[5].ToString();
            conn.Execute("INSERT INTO AUDIT_TRAIL(userName, action) values('" + unm + "', '" + done + "')");
        }

        [HttpPost]
        public JsonResult SubmitAccess(CLIENT_ACCESS model_state)
        {
            var checkExistingEmail = conn.Query<int>(@"SELECT ID FROM LRWEB_V1_CLIENT_ACCESS_ID WHERE email=@email", new
            {
                email = model_state.email
            }).Count();
            if (checkExistingEmail != 0)
            {
                // PUT RESPONSE HERE ON VALIDATION OF EXISITING EMAIL
                return Json("Client’s email address already exists in the system.", JsonRequestBehavior.AllowGet);
            }
            //List<ValidationMessage> messages = new List<ValidationMessage>();
            //        ValidationMessage vm;
            //        if (model_state.firstName == null || model_state.firstName == "")
            //        {
            //            vm = new ValidationMessage();
            //            vm.content_id = "firstname";
            //            vm.message_error = "";
            //            messages.Add(vm);
            //        }
            //        if (model_state.lastName == null || model_state.lastName == "")
            //        {
            //            vm = new ValidationMessage();
            //            vm.content_id = "lastname";
            //            vm.message_error = "";
            //            messages.Add(vm);
            //        }
            //        if (model_state.mobileNumber == null || model_state.mobileNumber == "" || !Regex.Match(model_state.mobileNumber, @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$").Success)
            //        {
            //            vm = new ValidationMessage();
            //            vm.content_id = "mobilenumber";
            //            vm.message_error = "";
            //            messages.Add(vm);
            //        }
            //        if (model_state.email == null || model_state.email == "" || model_state.email.ToString().ToLower().Contains(".com") == false)
            //        {
            //            vm = new ValidationMessage();
            //            vm.content_id = "emailaddress";
            //            vm.message_error = "";
            //            messages.Add(vm);
            //        }

            //if(messages.Count != 0)
            //{
            //    return Json(messages, JsonRequestBehavior.AllowGet);
            //}

            string sss = GlobalVariableClass.EncryptBCrypt((model_state.firstName + model_state.lastName).Trim()).ToString();
            string mname = "";
            if (model_state.middleName != null)
            {
                mname = model_state.middleName;
            }
            conn.Execute(@"INSERT INTO LRWEB_V1_CLIENT_ACCESS_ID
                    (groupCode,lastName,firstName,middleName,contactNumber,email,password,sent_status)
                    VALUES
                    (@GroupCode,@last_name,@first_name,@middle_name,@contact_no,@email,
                    @password,@status)", new
            {
                GroupCode = model_state.groupCode,
                last_name = model_state.lastName,
                first_name = model_state.firstName,
                middle_name = mname,
                contact_no = model_state.mobileNumber,
                email = model_state.email,
                password = sss,
                status = "0"
            });

            string done = "Added HR " + model_state.email + " on Group Code " + model_state.groupCode;
            logit(done);




            return Json("Success",JsonRequestBehavior.AllowGet);
        }
    }
}
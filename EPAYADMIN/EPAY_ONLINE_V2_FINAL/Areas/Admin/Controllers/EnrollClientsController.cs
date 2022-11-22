
using BankAlertsBL;
using BusinessObjectEntities.BankAlerts.Entity;
using Dapper;
using System.Net.Http;
using System.Net.Http.Headers;
using LRWEB_V1_ADMIN_T24.Areas.Admin.Models;
using LRWEB_V1_ADMIN_T24.Areas.Admin.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace LRWEB_V1_ADMIN_T24.Areas.Admin.Controllers
{
    public class EnrollClientsController : Controller
    {
        string post_search = "";
        public string authtoken = "";
        //string myConnectionString = "Server=10.204.99.30;Port=1234;Database=csblhagency;Uid=lrwebdev;Pwd=lrw3bd3v123#";
        DateTime dateTime = DateTime.UtcNow.Date;
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMBANKconnection"].ToString());
        SqlConnection connBankAlerts = new SqlConnection(ConfigurationManager.ConnectionStrings["BANKALERTSconnection"].ToString());
        MySqlConnection AELcon = new MySqlConnection(ConfigurationManager.ConnectionStrings["AELConnection"].ToString());
        string Uristring = ConfigurationManager.ConnectionStrings["unifiedapi"].ToString();
        string token = ConfigurationManager.ConnectionStrings["unifiedtoken"].ToString();
        BLMapper blMapper = new BLMapper();
        // GET: Admin/EnrollClients
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

            var obj = conn.Query<_EPAY_V2_CLIENTS>(@"SELECT * FROM LRWEB_V1_CLIENTS WHERE status<>'DELETE' ORDER BY ID DESC").ToList();
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
                    result_asd.Add(model);
                }
            }

            VM_Epay viewmodel = new VM_Epay();
            viewmodel._EPAY_V2_CLIENTS = result_asd;
            return View(viewmodel);
        }

        public ActionResult NewGroupCode()
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
            //using (var client = new HttpClient())
            //{

            //    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            //    var auth = new auth { username = uname, password = pword };
            //    client.BaseAddress = new Uri(Uristring);
            //    client.DefaultRequestHeaders.Accept
            //        .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    var response = client.PostAsync("auth/getToken", new StringContent(new JavaScriptSerializer().Serialize(auth), Encoding.UTF8, "application/json")).Result;

            //    var contents = response.Content.ReadAsStringAsync().Result;
            //    JObject res = JObject.Parse(contents);
            //    Session["auththoken"] = res["data"].ToString();

            //}

                return View(GetDatasGroupcode());
        }

        public ActionResult UpdateGroupcode()
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

            if (Request.QueryString["GroupCode"] == null || Request.QueryString["GroupCode"] =="")
            {
                return Redirect(GlobalVariableClass.FolderName + "/Admin/EnrollClients/Index?content=EnrollClient");
            }
            //using (var client = new HttpClient())
            //{

            //    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            //    var auth = new auth { username = uname, password = pword };
            //    client.BaseAddress = new Uri(Uristring);
            //    client.DefaultRequestHeaders.Accept
            //        .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    var response = client.PostAsync("auth/getToken", new StringContent(new JavaScriptSerializer().Serialize(auth), Encoding.UTF8, "application/json")).Result;

            //    var contents = response.Content.ReadAsStringAsync().Result;
            //    JObject res = JObject.Parse(contents);
            //    Session["auththoken"] = res["data"].ToString();

            //}
            return View(GetDataUpdateGroupCode());
        }

        public ViewModel_NewGroupCode GetDatasGroupcode()
        {
            
            var obj = conn.Query<_FIN_PROFIT_CTR>("SELECT * FROM CSB_BRANCH ORDER BY BR_DESC ASC").ToList();
            List<_FIN_PROFIT_CTR> result_profitctr = new List<_FIN_PROFIT_CTR>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    _FIN_PROFIT_CTR fpc = new _FIN_PROFIT_CTR();
                    fpc.BR_DESC = row.BR_DESC;
                    result_profitctr.Add(fpc);
                }
            }


            var obj_scheme = AELcon.Query<_FIN_CUST_GRP_CODE_CMS>("select " +
                "scheme_code,agency_code,agency_name,SUBSTR(group_code,1,5) AS group_code " +
                "from m_agency where product = 'Agency Employee Loan'").ToList();
            List<_FIN_CUST_GRP_CODE_CMS> result_groupcode = new List<_FIN_CUST_GRP_CODE_CMS>();
            if (obj_scheme != null)
            {
                foreach (var sc in obj_scheme)
                {
                    _FIN_CUST_GRP_CODE_CMS temp = new _FIN_CUST_GRP_CODE_CMS();
                    temp.group_code = sc.group_code;
                    temp.scheme_code = sc.scheme_code;
                    temp.agency_name = sc.agency_name;
                    temp.agency_code = sc.agency_code;
                    result_groupcode.Add(temp);
                }
            }

            List<BankAlerts_Province_Entity> result_location = blMapper.provinceBL.Get(connBankAlerts);

            ViewModel_NewGroupCode vmn = new ViewModel_NewGroupCode();
            vmn.vm_fpc = result_profitctr;
            vmn.vm_groupcode = result_groupcode; 
            vmn.vm_getlocations = result_location;
            //vmn.vm_scheme_code = result_sc;
            string identity_string = "";
            if (Request.QueryString["GroupCode"] != null)
            {
                var obj_clients = conn.Query<_EPAY_V2_CLIENTS>(Queries.QueryModel_EnrollClients._EPAY_V2_CLIENTS_QUERY("and GroupCode=@GroupCode"), new
                {
                    GroupCode = Request.QueryString["GroupCode"]
                }).ToList();
                List<_EPAY_V2_CLIENTS> result_clients = new List<_EPAY_V2_CLIENTS>();
                if(obj_clients != null)
                {
                    foreach(var row in obj_clients)
                    {
                        _EPAY_V2_CLIENTS sub_row = new _EPAY_V2_CLIENTS();
                        sub_row.ID = row.ID;
                        sub_row.AED = row.AED;
                        sub_row.clientName = row.clientName;
                        sub_row.emailAddress = row.emailAddress;
                        sub_row.customerID = row.customerID;
                        sub_row.accountNumber = row.accountNumber;
                        sub_row.csbBranch = row.csbBranch;
                        sub_row.groupCode = row.groupCode;
                        sub_row.addressLine = row.addressLine;
                        sub_row.barangay = row.barangay;
                        sub_row.province = row.province;

                        sub_row.city = row.city;
                        sub_row.status = row.status;
                        sub_row.remarks = row.remarks;
                        result_clients.Add(sub_row);
                    }
                    vmn.vm_EPAY_V2_CLIENTS = result_clients;
                }
            }
            return vmn;
        }

        public ViewModel_NewGroupCode GetDataUpdateGroupCode()
        {

            var obj = conn.Query<_FIN_PROFIT_CTR>("SELECT * FROM CSB_BRANCH ORDER BY BR_DESC ASC").ToList();
            List<_FIN_PROFIT_CTR> result_profitctr = new List<_FIN_PROFIT_CTR>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    _FIN_PROFIT_CTR fpc = new _FIN_PROFIT_CTR();
                    //fpc.BR_CODE = row.BR_CODE;
                    fpc.BR_DESC = row.BR_DESC;
                    result_profitctr.Add(fpc);
                }
            }

            List<BankAlerts_Province_Entity> result_location = blMapper.provinceBL.Get(connBankAlerts);


            ViewModel_NewGroupCode vmn = new ViewModel_NewGroupCode();
            vmn.vm_fpc = result_profitctr;
            vmn.vm_getlocations = result_location;
            string identity_string = "";
            if (Request.QueryString["GroupCode"] != null)
            {
                var obj_clients = conn.Query<_EPAY_V2_CLIENTS>(Queries.QueryModel_EnrollClients._EPAY_V2_CLIENTS_QUERY("and groupCode=@GroupCode"), new
                {
                    GroupCode = Request.QueryString["GroupCode"]
                }).ToList();
                List<_EPAY_V2_CLIENTS> result_clients = new List<_EPAY_V2_CLIENTS>();
                if (obj_clients != null)
                {
                    foreach (var row in obj_clients)
                    {
                        _EPAY_V2_CLIENTS sub_row = new _EPAY_V2_CLIENTS();
                        sub_row.ID = row.ID;
                        sub_row.AED = row.AED;
                        sub_row.clientName = row.clientName;
                        sub_row.addressLine = row.addressLine;
                        sub_row.barangay = row.barangay;
                        sub_row.province = row.province;
                        sub_row.city = row.city;
                        sub_row.barangayId = row.barangayId;
                        sub_row.provinceId = row.provinceId;
                        sub_row.cityId = row.cityId;
                        sub_row.mobileNumber = row.mobileNumber;
                        sub_row.officeNumber = row.officeNumber;
                        sub_row.emailAddress = row.emailAddress;
                        sub_row.emailFormat = row.emailFormat;
                        sub_row.classification = row.classification;
                        sub_row.structure = row.structure;
                        sub_row.accountNumber = row.accountNumber;
                        sub_row.customerID = row.customerID;
                        sub_row.sol_id = row.sol_id;
                        sub_row.finacle_sol_id = row.finacle_sol_id;
                        sub_row.csbBranch = row.csbBranch;
                        sub_row.schemeCode = row.schemeCode;
                        sub_row.creditRatio = row.creditRatio;
                        sub_row.groupCode = row.groupCode;
                        sub_row.status = row.status;
                        sub_row.remarks = row.remarks;

                        result_clients.Add(sub_row);
                    }

                    vmn.vm_EPAY_V2_CLIENTS = result_clients;
                }
            }
            return vmn;
        }

        public List<_ERROR_MESSAGE> ValidateGroupCode(List<_ERROR_MESSAGE> result, _EPAY_V2_CLIENTS model_state)
        {
            var checkGroupcodeExist_New = conn.Query<int>(@"SELECT COUNT(GroupCode) FROM 
            [LRWEB].[dbo].[LRWEB_V1_CLIENTS] WHERE GroupCode=@GroupCode", new
            {
                GroupCode = model_state.groupCode
            }).Single();

            //var checkGroupcodeExist_Old = conn.Query<int>(@"SELECT  COUNT([CUST_GRP_CODE])
            //FROM [TeamBank].[dbo].[FIN_CUST_GRP_CODE_CMS] WHERE CUST_GRP_CODE=@CUST_GRP_CODE", new
            //{
            //    CUST_GRP_CODE = model_state.groupCode
            //}).Single();
            // if (checkGroupcodeExist_New != 0 && checkGroupcodeExist_Old != 0)
            if (checkGroupcodeExist_New != 0)
            {
                _ERROR_MESSAGE err = new _ERROR_MESSAGE();
                err.content_id = "GroupCode";
                err.error_message = "Group code already exists";
                result.Add(err);
                return result;
            }
            else if (model_state.groupCode == "")
            {
                _ERROR_MESSAGE err = new _ERROR_MESSAGE();
                err.content_id = "GroupCode";
                err.error_message = "Group code is required";
                result.Add(err);
                return result;
            }
            return result;
        }

        [HttpPost]
        public JsonResult GetRMCode(string grpcode)
        {

            string htmltags = "";
            //var obj = conn.Query<_FIN_CUST_GRP_CODE_CMS>("SELECT * FROM FIN_CUST_GRP_CODE_CMS where CUST_GRP_CODE=@CUST_GRP_CODE ORDER BY CUST_GRP_DESC ASC", new { CUST_GRP_CODE = grpcode }).ToList();
            //if (obj != null)
            //{
            //    int x = 0;
            //    foreach (var row in obj)
            //    {
            //        htmltags = htmltags + "<option value='" + row.RM_ID + "'>" + row.RM_ID + "</option>";
            //    }
            //}

            return Json(htmltags, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ValidateGroupCode(string model_state)
        {
            List<_ERROR_MESSAGE> result = new List<_ERROR_MESSAGE>();
            var checkGroupcodeExist_New = conn.Query<int>(@"SELECT COUNT(GroupCode) FROM 
            [LRWEB].[dbo].[LRWEB_V1_CLIENTS] WHERE GroupCode=@GroupCode", new
            {
                GroupCode = model_state
            }).Single();

            //var checkGroupcodeExist_Old = conn.Query<int>(@"SELECT  COUNT([CUST_GRP_CODE])
            //FROM [TeamBank].[dbo].[FIN_CUST_GRP_CODE_CMS] WHERE CUST_GRP_CODE=@CUST_GRP_CODE", new
            //{
            //    CUST_GRP_CODE = model_state
            //}).Single();
            //if (checkGroupcodeExist_New != 0 && checkGroupcodeExist_Old != 0)
            if (checkGroupcodeExist_New != 0)
            {
                _ERROR_MESSAGE err = new _ERROR_MESSAGE();
                err.content_id = "GroupCode";
                err.error_message = "Group Code already exist";
                result.Add(err);
                return Json(result, JsonRequestBehavior.AllowGet);
            }else if(model_state == "")
            {
                _ERROR_MESSAGE err = new _ERROR_MESSAGE();
                err.content_id = "GroupCode";
                err.error_message = "Group Code is required";
                result.Add(err);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            //else if (model_state.Length > 5 || model_state.Length < 3)
            //{
            //    _ERROR_MESSAGE err = new _ERROR_MESSAGE();
            //    err.content_id = "GroupCode";
            //    err.error_message = "Group code is 3-5 Characters allowed";
            //    result.Add(err);
            //    return Json(result, JsonRequestBehavior.AllowGet);
            //}

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertNewClient(_EPAY_V2_CLIENTS model_state)
        {
            var response_array = new string[2,30];
            List<_ERROR_MESSAGE> result = new List<_ERROR_MESSAGE>();
            result = ValidateGroupCode(result,model_state);
            if (!ModelState.IsValid)
            {

                    int model_length = ViewData.ModelState.Values.Count;
                    var ErrorArray = new string[model_length];
                    int xx = 0;
                    foreach (var ms in ViewData.ModelState.Values)
                    {
                        foreach (var errors in ms.Errors)
                        {
                            ErrorArray[xx] = errors.ErrorMessage;
                        }
                        xx++;
                    }
                    int counter = 0;
                    int ccnc = 0;
                    foreach (var modelnames in ModelState)
                    {
                        _ERROR_MESSAGE err = new _ERROR_MESSAGE();
                        err.content_id = modelnames.Key.Replace("[" + counter + "].", "");
                        err.error_message = ErrorArray[ccnc];
                        ccnc++;
                        result.Add(err);
                    }

                    return Json(result, JsonRequestBehavior.AllowGet);
           }
           else
           {
                    if (result.Count != 0)
                    {
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    ExecuteClientRequest(model_state);
                    return Json("", JsonRequestBehavior.AllowGet);
           }
 
        }


        [HttpPost]
        public JsonResult UpdateClient(_EPAY_V2_CLIENTS model_state)
        {
            var response_array = new string[2, 30];
            if (!ModelState.IsValid)
            {
                List<_ERROR_MESSAGE> result = new List<_ERROR_MESSAGE>();
                int model_length = ViewData.ModelState.Values.Count;
                var ErrorArray = new string[model_length];
                int xx = 0;
                foreach (var ms in ViewData.ModelState.Values)
                {
                    foreach (var errors in ms.Errors)
                    {
                        ErrorArray[xx] = errors.ErrorMessage;
                    }
                    xx++;
                }
                int counter = 0;
                int ccnc = 0;
                foreach (var modelnames in ModelState)
                {
                    _ERROR_MESSAGE err = new _ERROR_MESSAGE();
                    err.content_id = modelnames.Key.Replace("[" + counter + "].", "");
                    err.error_message = ErrorArray[ccnc];
                    ccnc++;
                    result.Add(err);
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ExecuteClientUpdateRequest(model_state);
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }


        public void ExecuteClientUpdateRequest(_EPAY_V2_CLIENTS model_state)
        {
            conn.Execute(Queries.QueryModel_EnrollClients._EPAY_V2_CLIENT_UPDATE(), new
            {
                groupCode = model_state.groupCode,
                schemeCode = model_state.schemeCode,
                clientName = model_state.clientName,
                csbbranch = model_state.csbBranch,
                sol_id = model_state.sol_id,
                finacle_sol_id = model_state.finacle_sol_id,
                AED = model_state.AED,
                addressLine = model_state.addressLine,
                barangay = model_state.barangay,
                city = model_state.city,
                province = model_state.province,
                barangayId = model_state.barangayId,
                cityId = model_state.cityId,
                provinceId = model_state.provinceId,
                mobileNumber = model_state.mobileNumber,
                officeNumber = model_state.officeNumber,
                emailAddress = model_state.emailAddress,
                emailFormat = model_state.emailFormat,
                classification = model_state.classification,
                structure = model_state.structure,
                accountNumber = model_state.accountNumber,
                customerID = model_state.customerID,
                creditRatio = model_state.creditRatio
            });

            //conn.Execute(@"UPDATE FIN_CUST_GRP_CODE_CMS SET NewCardType=@CARD_TYPE
            //        WHERE CUST_GRP_CODE=@GroupCode", new {
            //    CARD_TYPE = model_state.CARD_TYPE,
            //    GroupCode=model_state.GroupCode
            //});

            //conn.Execute(@"DELETE EPAY_V2_SCHEMECODE WHERE groupcode=@groupcode", new {
            //    groupcode = model_state.GroupCode
            //});

            //var arr_scheme = model_state.AccountType.ToString().Split(',');
            //for (int x = 0; x < arr_scheme.Length; x++)
            //{
            //    conn.Execute(@"INSERT INTO EPAY_V2_SCHEMECODE(Schemecode,groupcode) 
            //    VALUES(@Schemecode,@groupcode)", new
            //    {
            //        Schemecode = arr_scheme[x],
            //        groupcode = model_state.GroupCode
            //    });
            //}

            //conn.Execute(@"DELETE EPAY_V2_CLIENTS_CARDDELIVERYSOL WHERE
            //GroupCode=@gcode", new {
            //     gcode = model_state.GroupCode
            //});

            //if(model_state.CardDeliverySol != null)
            //{
            //    foreach(var bcode in model_state.CardDeliverySol)
            //    {
            //        conn.Execute(@"INSERT EPAY_V2_CLIENTS_CARDDELIVERYSOL 
            //        (GroupCode,BranchCode) values (@GroupCode, @BranchCode)",new {
            //            GroupCode = model_state.GroupCode,
            //            BranchCode = bcode
            //        });
            //    }
            //}

            if (model_state.contentMark != null)
            {
                if (model_state.contentMark == "ReturnEntries")
                {
                    conn.Execute(@"UPDATE LRWEB_V1_CLIENTS SET status='PENDING'
                    WHERE groupCode=@groupCode", new
                    {
                        groupCode = model_state.groupCode
                    });
                    string done = "Updated returned Group Code " + model_state.groupCode;
                    logit(done);
                } else
                {
                    string done = "Updated Group Code " + model_state.groupCode;
                    logit(done);
                }
            }

        }

        public void ExecuteClientRequest(_EPAY_V2_CLIENTS model_state)
        {
            var arrcred = GlobalVariableClass.Base64Decode(GlobalVariableClass.Base64Decode(Session["cred"].ToString())).Split(',');
            var obj = conn.Query<int>(Queries.QueryModel_EnrollClients._EPAY_V2_CLIENT_INSERT(), new
            {
                groupCode = model_state.groupCode,
                schemeCode = model_state.schemeCode,
                agencyCode = model_state.agencyCode,
                clientName = model_state.clientName,
                sol_id = model_state.sol_id,
                finacle_sol_id = model_state.finacle_sol_id,
                csbbranch = model_state.csbBranch,
                AED = model_state.AED,
                addressLine = model_state.addressLine,
                barangay = model_state.barangay,
                province = model_state.province,
                city = model_state.city,
                barangayId = model_state.barangayId,
                provinceId = model_state.provinceId,
                cityId = model_state.cityId,
                mobileNumber = model_state.mobileNumber,
                officeNumber = model_state.officeNumber,
                emailAddress = model_state.emailAddress,
                emailFormat = model_state.emailFormat,
                classification = model_state.classification,
                structure = model_state.structure,
                accountNumber = model_state.accountNumber,
                customerID = model_state.customerID,
                creditRatio = model_state.creditRatio,
                admin_id = arrcred[0]
            }).Single();
            string done = "Added Group Code " + model_state.groupCode;
            logit(done);
            
        }

        public void logit(string done)
        {
            var arrcred = GlobalVariableClass.Base64Decode(GlobalVariableClass.Base64Decode(Session["cred"].ToString())).Split(',');
            var unm = arrcred[5].ToString();
            conn.Execute("INSERT INTO AUDIT_TRAIL(userName, action) values('" + unm + "', '" + done + "')");
        }

        [HttpPost]
        public JsonResult DeleteClient(string thisid)
        {

            conn.Execute("UPDATE LRWEB_V1_CLIENTS SET status=@Status where ID=@ID", new
            {
                Status = "DELETE",
                IntRecord = thisid
            });
            //string done = "Updated Group Code " + model_state.groupCode;
            //logit(done);
            return Json("Successfully Delete Selected Client!", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getProvince()
        {
            using (var client = new HttpClient())
            {
                authtoken = token;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                client.BaseAddress = new Uri(Uristring);
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("authtoken", authtoken);
                var response = client.GetAsync("lookup/province").Result;

                var contents = response.Content.ReadAsStringAsync().Result;
                JObject res = JObject.Parse(contents);
                return Json(res["data"].ToString(), JsonRequestBehavior.AllowGet);
            }     
        }

        [HttpPost]
        public JsonResult getCity(string provinceId)
        {
            using (var client = new HttpClient())
            {
                authtoken = token;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                client.BaseAddress = new Uri(Uristring);
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("authtoken", authtoken);
                string address = "lookup/city/" + provinceId;
                var response = client.GetAsync(address).Result;

                var contents = response.Content.ReadAsStringAsync().Result;
                JObject res = JObject.Parse(contents);
                return Json(res["data"].ToString(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult getBarangay(string cityId)
        {
            using (var client = new HttpClient())
            {
                authtoken = token;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                client.BaseAddress = new Uri(Uristring);
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("authtoken", authtoken);
                string address = "lookup/brgy/" + cityId;
                var response = client.GetAsync(address).Result;

                var contents = response.Content.ReadAsStringAsync().Result;
                JObject res = JObject.Parse(contents);
                return Json(res["data"].ToString(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult getCompany()
        {
            using (var client = new HttpClient())
            {
                authtoken = token;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                client.BaseAddress = new Uri(Uristring);
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("authtoken", authtoken);
                var response = client.GetAsync("company").Result;

                var contents = response.Content.ReadAsStringAsync().Result;
                JObject res = JObject.Parse(contents);
                return Json(res["data"].ToString(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult getSol()
        {
            using (var client = new HttpClient())
            {
                authtoken = token;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                client.BaseAddress = new Uri(Uristring);
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("authtoken", authtoken);
                var response = client.GetAsync("sol/type?company=City Savings Bank").Result;

                var contents = response.Content.ReadAsStringAsync().Result;
                JObject res = JObject.Parse(contents);
                return Json(res["data"].ToString(), JsonRequestBehavior.AllowGet);
            }
        }
        
        [HttpPost]
        public JsonResult getCompanyDetails(string agencyCode)
        {
            //using (var client = new HttpClient())
            //{
            //    authtoken = token;
            //    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            //    client.BaseAddress = new Uri(Uristring);
            //    client.DefaultRequestHeaders
            //        .Accept
            //        .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    client.DefaultRequestHeaders.Add("authtoken", authtoken);
            //    string address = "company/" + companyId;
            //    var response = client.GetAsync(address).Result;

            //    var contents = response.Content.ReadAsStringAsync().Result;
            //    JObject res = JObject.Parse(contents);
            //    return Json(res["data"].ToString(), JsonRequestBehavior.AllowGet);
            //}

            var obj_scheme = AELcon.Query<_FIN_CUST_GRP_CODE_CMS>(@"select " +
                "scheme_code,agency_code,agency_name,SUBSTR(group_code,1,5) AS group_code " +
                "from m_agency where product = 'Agency Employee Loan' and agency_code=@agencyCode", new { 
                agencyCode = agencyCode
                }).ToList();
            List<_FIN_CUST_GRP_CODE_CMS> result_groupcode = new List<_FIN_CUST_GRP_CODE_CMS>();
            if (obj_scheme != null)
            {
                foreach (var sc in obj_scheme)
                {
                    _FIN_CUST_GRP_CODE_CMS temp = new _FIN_CUST_GRP_CODE_CMS();
                    temp.group_code = sc.group_code;
                    temp.scheme_code = sc.scheme_code;
                    temp.agency_name = sc.agency_name;
                    temp.agency_code = sc.agency_code;
                    result_groupcode.Add(temp);
                }
            }
            return Json(result_groupcode, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SearchClient(string txtsearch)
        {           
            var obj = conn.Query<_EPAY_V2_CLIENTS>(@"SELECT TOP 300 cl.*, 
            (Select Count(brn.GroupCode) as brnasd from EPAY_V2_CLIENTS_BRANCH as brn where brn.GroupCode = cl.GroupCode) as branches
             FROM EPAY_V2_CLIENTS as cl WHERE  
            (AccountNo like @AccountNo or GroupCode like @GroupCode or 
            CompanyName like @CompanyName or CustomerID like @CustomerID)
            And status<>'DELETE' 
            ORDER BY IntRecord DESC", new {
                AccountNo = "%" + txtsearch + "%",
                GroupCode = "%" + txtsearch + "%",
                CompanyName = "%" + txtsearch + "%",
                CustomerID = "%" + txtsearch + "%"
            }).ToList();
            List<_EPAY_V2_CLIENTS> result_asd = new List<_EPAY_V2_CLIENTS>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    _EPAY_V2_CLIENTS model = new _EPAY_V2_CLIENTS();
                    //model.IntRecord = row.IntRecord;
                    //model.AccountNo = row.AccountNo;
                    //model.GroupCode = row.GroupCode;
                    //model.CompanyName = row.CompanyName;
                    //model.CustomerID = row.CustomerID;
                    //model.RMCode = row.RMCode;
                    //model.status = row.status;
                    //model.branches = row.branches;
                    result_asd.Add(model);
                }
            }

            return Json(result_asd);
        }

        class auth
        {
            public string username { get; set; }
            public string password { get; set; }
        }
        class getauth
        {
            public string message { get; set; }
            public string data { get; set; }
        }

        private class province
        {
            public string message { get; set; }
            public string data {get; set;}
        }
    }
}
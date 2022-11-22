using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using LRWEB_V1_CLIENT_SIDE_T24.Areas.HumanResource.Models;
using LRWEB_V1_CLIENT_SIDE_T24.Areas.HumanResource.LogCode;
using LRWEB_V1_CLIENT_SIDE_T24.Areas.HumanResource.LogCode.LogsModel;

namespace LRWEB_V1_CLIENT_SIDE_T24.Areas.HumanResource.Controllers
{
    public class ManageController : Controller
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMBANKconnection"].ToString());
        DateTime dateTime = DateTime.UtcNow.Date;
        // GET: HumanResource/Manage
 
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
                    //return View();
                }
            }
 

            string[] arrcred = GlobalVariableClass.Base64Decode(GlobalVariableClass.Base64Decode(Request.Cookies["ASP_NET_CD"].Value.ToString())).Split(',');
            var obj = conn.Query<_GCODE_SCHEME>(@"SELECT * FROM LRWEB_V1_CLIENTS WHERE groupCode = @gcode", new
            {
                gcode = arrcred[1].ToString()
            }).ToList();
            List<_GCODE_SCHEME> list = new List<_GCODE_SCHEME>();
            if (obj != null)
            {
                foreach (var sccl in obj)
                {
                    _GCODE_SCHEME scheme = new _GCODE_SCHEME();
                    scheme.ID = sccl.ID;
                    scheme.schemeCode = sccl.schemeCode;
                    scheme.groupCode = sccl.groupCode;
                    scheme.csbBranch = sccl.csbBranch;
                    scheme.emailFormat = sccl.emailFormat;
                    list.Add(scheme);
                }
            }

            //var obj = conn.Query<_GCODE_SCHEME>(@"SELECT   sc.*,
            //(SELECT sl.Definition FROM EPAY_V2_SCHEMECODE_LIST as sl WHERE sl.SchemeCodes = sc.Schemecode) as DefinitionScheme
            //  FROM EPAY_V2_SCHEMECODE as sc WHERE sc.groupcode=@gcode", new {
            //    gcode = arrcred[1].ToString()
            //}).ToList();
            //List<_GCODE_SCHEME> list = new List<_GCODE_SCHEME>();
            //if(obj != null)
            //{
            //    foreach(var sccl in obj)
            //    {
            //        _GCODE_SCHEME scheme = new _GCODE_SCHEME();
            //        scheme.IntRecord = sccl.IntRecord;
            //        scheme.Schemecode = sccl.Schemecode;
            //        scheme.groupcode = sccl.groupcode;
            //        scheme.DefinitionScheme = sccl.DefinitionScheme;
            //        list.Add(scheme);
            //    }
            //}

            //var obj_BRANCH = conn.Query<_FIN_PROFIT_CTR>(@"SELECT CD.*, CD.BranchCode as BR_CODE,CTR.BR_DESC
            //FROM EPAY_V2_CLIENTS_CARDDELIVERYSOL AS CD
            //INNER JOIN FIN_PROFIT_CTR AS CTR ON CD.BranchCode = CTR.BR_CODE WHERE GroupCode=@gcode",new {
            //    gcode = arrcred[1].ToString()
            //}).ToList();
            //List<_FIN_PROFIT_CTR> result_profitctr = new List<_FIN_PROFIT_CTR>();
            //if (obj != null)
            //{
            //    foreach (var row in obj_BRANCH)
            //    {
            //        _FIN_PROFIT_CTR fpc = new _FIN_PROFIT_CTR();
            //        fpc.BR_CODE = row.BR_CODE;
            //        fpc.BR_DESC = row.BR_DESC;
            //        result_profitctr.Add(fpc);
            //    }
            //}

            VM_INDEX_MANAGE vm = new VM_INDEX_MANAGE();
            //vm.vm_FIN_PROFIT_CTR = obj_BRANCH;
            vm.vm_GCODE_SCHEME = obj;
            return View(vm);
        }

        public class VM_INDEX_MANAGE
        {
            public List<_GCODE_SCHEME> vm_GCODE_SCHEME { get; set; }
            public List<_FIN_PROFIT_CTR> vm_FIN_PROFIT_CTR { get; set; }
        }


        [HttpPost] 
        public JsonResult SubmitNewEmployeee(List<_ENROLL_EMPLOYEE> model_state)
        {
            var sss = ModelState;

            string[] arrcred = GlobalVariableClass.Base64Decode(GlobalVariableClass.Base64Decode(Request.Cookies["ASP_NET_CD"].Value.ToString())).Split(',');
            if (arrcred[1] == null)
            {
                return Json("Request Failed", JsonRequestBehavior.AllowGet);
            }
            List<_ERROR_MESSAGE> error_msg = new List<_ERROR_MESSAGE>();
            if (!ModelState.IsValid)
            {
                int haba = ViewData.ModelState.Values.Count;
                var ErrorArray = new string[haba];
                int cc = 0;
                int xx = 0;
                foreach (var ms in ViewData.ModelState.Values)
                {
                  
                    foreach (var errors in ms.Errors)
                    {
                        string error = errors.ErrorMessage;
                        ErrorArray[xx] = errors.ErrorMessage;
                    }
                    xx++;
                }

                int counter = 0;
                int ccnc = 0;
                foreach (var modelnames in ModelState)
                {
                    _ERROR_MESSAGE err = new _ERROR_MESSAGE();
                    err.component_id = modelnames.Key.Replace("["+ (counter - 1) +"].","").Replace("[" + counter + "].", "") ;
                    err.message = ErrorArray[ccnc];
                    ccnc++;
                    if ((ccnc%7) == 0) {
                        //ccnc = 0;
                        counter++;
                    }

                    error_msg.Add(err);
                }
                return Json(error_msg, JsonRequestBehavior.AllowGet);
            }

            var checker = model_state;
            int cnt = 0;
            foreach (var ck in model_state)
            {
                int x=0;
                foreach(var mm in checker)
                {
                    if(mm.email_address == ck.email_address)
                    {
                        x++;
                    }
                }
                if(x > 1)
                {
                    _ERROR_MESSAGE err = new _ERROR_MESSAGE();
                    err.component_id = "email_address"; //+ cnt ;
                    err.message = "Email address duplicate is not allowed";
                    error_msg.Add(err);
                }
                cnt++;
            }
            if(error_msg.Count != 0)
            {
                return Json(error_msg, JsonRequestBehavior.AllowGet);
            }

            _ACTIONS action = new _ACTIONS();
            action.EnrolledID = new List<string>();
            foreach (var mstate in model_state)
            {
                
                DateTime dt = new DateTime();
                dt = DateTime.Now;
                dt = dt.AddDays(7);
                string token = "";

                string mname = "";
                if(mstate.middlename != null)
                {
                    mname = mstate.middlename;
                }
                int mark = 0;
                token = GlobalVariableClass.RandomString(72);
                var obj = conn.Query<_EPAY_V2_CLIENT_EMPLOYEE>("Select * from LRWEB_V1_CLIENT_EMPLOYEE where Tokens=@tokens", new {
                    tokens = token
                }).ToList();
                if (obj != null)
                {
                    foreach (var row in obj)
                    {
                        mark++;
                    }
                }

                var getid = conn.Query<string>(@"INSERT INTO LRWEB_V1_CLIENT_EMPLOYEE(GroupCode,last_name
            ,first_name,middle_name,contactnum,email,Tokens,Expiration,Status,Client_Branch
            ,SubmitStatus,scheme_code,branch_delivery,tokenSender) VALUES (@GroupCode,@last_name
            ,@first_name,@middle_name,@contactnum,@email,@Tokens,@Expiration,@Status,@Client_Branch
            ,@SubmitStatus,@scheme_code,@branch_delivery,@tokenSender);SELECT CAST(SCOPE_IDENTITY() as int)", new {
                    GroupCode = arrcred[1],
                    last_name= mstate.lastname,
                    first_name = mstate.firstname,
                    middle_name = mname,
                    contactnum = mstate.contactnumber,
                    email = mstate.email_address,
                    Tokens = token,
                    Expiration = dt.ToString("yyyyMMddHHmmss"),
                    Status = "0",
                    Client_Branch = mname,
                    SubmitStatus = "0",
                    scheme_code = mstate.scheme_code,
                    branch_delivery = mstate.branch_delivery,
                    tokenSender = arrcred[6].ToString()
                }).Single();
                action.EnrolledID.Add(getid);
            }

            //LOGSFILE logsfile = new LOGSFILE();
            //_LOGS logs = new _LOGS();
            //var cred = GlobalVariableClass.Base64Decode(GlobalVariableClass.Base64Decode(Request.Cookies["ASP_NET_CD"].Value.ToString())).Split(',');
            //logs.UserCode = cred[0];
            //logs.GroupCode = cred[1];
            //logs.email = cred[6];
            //action.Event = "ENROLL";
            //action.Module = "EMPLOYEES";
            //action.ID = "";
            //action.time = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            //action.Before = "";
            //action.After = "";
            //logs.Action = action;
            //logsfile.ActionLog(logs);
            return Json("");
        }
        
    }
}
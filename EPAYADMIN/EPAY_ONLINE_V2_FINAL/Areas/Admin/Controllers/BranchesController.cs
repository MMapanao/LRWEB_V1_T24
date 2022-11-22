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
    public class BranchesController : Controller
    {
        string post_search = "";
        DateTime dateTime = DateTime.UtcNow.Date;
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMBANKconnection"].ToString());
        SqlConnection connBankAlerts = new SqlConnection(ConfigurationManager.ConnectionStrings["BANKALERTSconnection"].ToString());
 
        // GET: Admin/Branches
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
            if (Request.QueryString["GroupCode"] == null)
            {
                return Redirect(GlobalVariableClass.FolderName + "/Admin/EnrollClients/Index?content=EnrollClient");
            }
            var obj = conn.Query<_BRANCHES>(@"SELECT  *
            FROM EPAY_V2_CLIENTS_BRANCH where GroupCode = @groupcode and Status<>@status", new {
                groupcode = Request.QueryString["GroupCode"],
                status = "DELETE"
            }).ToList();
            List<_BRANCHES> result = new List<_BRANCHES>();
            if(obj != null)
            {
                foreach(var row in obj)
                {
                    _BRANCHES evcb = new _BRANCHES();
                    evcb.IntRecord = row.IntRecord;
                    evcb.CompanyID = row.CompanyID;
                    evcb.SiteName = row.SiteName;
                    evcb.Street = row.Street;
                    evcb.District = row.District;
                    evcb.City = row.City;
                    evcb.ZipCode = row.ZipCode;
                    evcb.Status = row.Status;
                    evcb.GroupCode = Request.QueryString["GroupCode"];
                    result.Add(evcb);
                }
            }

            return View(result);
        }

        public ActionResult NewBranch()
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
                return Redirect(GlobalVariableClass.FolderName + "/Admin/EnrollClients/Index?content=EnrollClient");
            }
            //var obj_zipcode = conn.Query<_GET_LOCATIONS>("select left(a.REF_CODE,5) as zip, a.REF_DESC as loc, b.REF_DESC as cty from TEAMBANK..rct a, TEAMBANK..rct b where a.ref_rec_type = '01' and b.ref_rec_type = '02' and b.REF_CODE= left(a.REF_CODE,3) order by zip").ToList();
            //List<_GET_LOCATIONS> result_zipcodes = new List<_GET_LOCATIONS>();
            //if (obj_zipcode != null)
            //{
            //    foreach (var row in obj_zipcode)
            //    {
            //        if(row.zip != null)
            //        {
            //            if(row.zip != ".")
            //            {
            //                _GET_LOCATIONS GL = new _GET_LOCATIONS();
            //                GL.zip = row.zip;
            //                GL.cty = row.cty;
            //                GL.loc = row.loc;
            //                result_zipcodes.Add(GL);
            //            }

            //        }

            //    }
            //}

            var obj_location = connBankAlerts.Query<_CITY_PROVINCES_LIST>(@"
                SELECT * FROM tblProvince order by code asc").ToList();
            List<_CITY_PROVINCES_LIST> result_location = new List<_CITY_PROVINCES_LIST>();
            if (obj_location != null)
            {
                foreach (var row in obj_location)
                {
                    _CITY_PROVINCES_LIST loc = new _CITY_PROVINCES_LIST();
                    loc.code = row.code.Trim();
                    loc.name = row.name;
                    loc.city_list = connBankAlerts.Query<_CITY>(@"
                        SELECT * FROM tblCity WHERE code like @code order by code asc", new
                    {
                        code = row.code.Trim() + "%" 
                    }).ToList();
                    result_location.Add(loc);
                }
            }

            return View(result_location);
        }


        [HttpPost]
        public JsonResult SubmitNewBranches([Bind(Include = "SiteName,Street,ZipCode,GroupCode")] List<_BRANCHES> model_state)
        {
            if(ModelState.IsValid)
            {
                foreach(var tt in model_state)
                {
                    conn.Execute(@"INSERT INTO EPAY_V2_CLIENTS_BRANCH
                    (SiteName,Street,ZipCode,Status,GroupCode) VALUES
                    (@SiteName,@Street,@ZipCode,'ACTIVE',@GroupCode)", new
                    {
                        SiteName = tt.SiteName,
                        Street = tt.Street,
                        ZipCode = tt.ZipCode,
                        GroupCode = tt.GroupCode
                    });
                }
            }else
            {
                List<ValidationMessage> messages = new List<ValidationMessage>();
                int asd = model_state.Count();
                var count_row = 0;
                foreach(var ss in model_state)
                {
                    ValidationMessage vm;
                    if (ss.SiteName == null || ss.SiteName == "")
                    {
                        vm = new ValidationMessage();
                        vm.content_id = "txtsitename" + count_row;
                        vm.message_error = "";
                        messages.Add(vm);
                    }
                    if (ss.Street == null || ss.Street == "")
                    {
                        vm = new ValidationMessage();
                        vm.content_id = "txtstreet" + count_row;
                        vm.message_error = "";
                        messages.Add(vm);
                    }
                    if (ss.ZipCode == null || ss.ZipCode == "")
                    {
                        vm = new ValidationMessage();
                        vm.content_id = "txtzipcode" + count_row;
                        vm.message_error = "";
                        messages.Add(vm);
                    }
                    if (ss.GroupCode == null || ss.GroupCode == "")
                    {
                        vm = new ValidationMessage();
                        vm.content_id = "txtgroupcode" ;
                        vm.message_error = "";
                        messages.Add(vm);
                    }
                    count_row++;
                }
                return Json(messages);
                
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SearchBranches(string txtsearch,string GroupCode)
        {
            var obj = conn.Query<_BRANCHES>(@"SELECT  *
            FROM EPAY_V2_CLIENTS_BRANCH where  
            (SiteName like @SiteName or Street like @Street or ZipCode like @ZipCode) And
            GroupCode = @groupcode and Status<>'DELETE'", new
            {
                groupcode = GroupCode,
                SiteName = "%"+txtsearch +"%",
                Street = "%" + txtsearch + "%",
                ZipCode = "%" + txtsearch + "%"
            }).ToList();
            List<_BRANCHES> result = new List<_BRANCHES>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    _BRANCHES evcb = new _BRANCHES();
                    evcb.IntRecord = row.IntRecord;
                    evcb.CompanyID = row.CompanyID;
                    evcb.SiteName = row.SiteName;
                    evcb.Street = row.Street;
                    evcb.District = row.District;
                    evcb.City = row.City;
                    evcb.ZipCode = row.ZipCode;
                    evcb.Status = row.Status;
                    evcb.GroupCode = Request.QueryString["GroupCode"];
                    result.Add(evcb);
                }
            }

            return Json(result);
        }

        
        public JsonResult DeleteBranch(string identity)
        {
            conn.Execute(@"DELETE EPAY_V2_CLIENTS_BRANCH WHERE IntRecord=@intrecord", new {
                intrecord = identity
            });
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}
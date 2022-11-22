using Dapper;
using LRWEB_V1_CLIENT_SIDE_T24.Areas.HumanResource.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.IO.Compression;
using System.Web.Hosting;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Web.Helpers;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using System.Text;
using Microsoft.Ajax.Utilities;

namespace LRWEB_V1_CLIENT_SIDE_T24.Areas.HumanResource.Controllers
{

    public class ApproveApplicationsController : Controller
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMBANKconnection"].ToString());
        SqlConnection connBankAlerts = new SqlConnection(ConfigurationManager.ConnectionStrings["BANKALERTSconnection"].ToString());
        MySqlConnection AELcon = new MySqlConnection(ConfigurationManager.ConnectionStrings["AELConnectiont24"].ToString());
        DateTime dateTime = DateTime.UtcNow.Date;
        String Uristring = ConfigurationManager.ConnectionStrings["Uristringt24"].ToString();
        String userName = ConfigurationManager.ConnectionStrings["uname"].ToString();
        String passwd = ConfigurationManager.ConnectionStrings["pword"].ToString();
        string centraluri = ConfigurationManager.ConnectionStrings["centralapi"].ToString();
        string centraluname = ConfigurationManager.ConnectionStrings["centraluname"].ToString();
        string centralpword = ConfigurationManager.ConnectionStrings["centralpword"].ToString();
        // GET: HumanResource/ApproveApplications
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
            var obj = conn.Query<_LRWEB_V1_CUSTOMER_APPLICATION>(@"SELECT *,(select concat(firstName, ' ', middleName, ' ', lastName) from LRWEB_V1_CUSTOMER_INFO as b where b.ID= a.referenceNumber )as name FROM LRWEB_V1_CUSTOMER_APPLICATION as a where groupCode=@groupCode order by dateCreated desc", new
            {
                groupCode = arrcred[1].ToString()
            }).ToList();
            List<_LRWEB_V1_CUSTOMER_APPLICATION> results = new List<_LRWEB_V1_CUSTOMER_APPLICATION>();
            if (obj != null)
            {
                foreach (var list in obj)
                {
                    _LRWEB_V1_CUSTOMER_APPLICATION lr = new _LRWEB_V1_CUSTOMER_APPLICATION();
                    lr.ID = list.ID;
                    lr.application_number = list.application_number;
                    lr.referenceNumber = list.referenceNumber;
                    lr.name = list.name;
                    lr.status = list.status;
                    lr.dateCreated = list.dateCreated;
                    results.Add(lr);
                }
            }
            return View(results);
            //return View(GetDataApproved(""));
        }

        public JsonResult SearchApproved(string txtsearch)
        {

            return Json(GetDataApproved(txtsearch), JsonRequestBehavior.AllowGet);
        }

        public List<_NEW_EMPLOYEE> GetDataApproved(string txtsearch)
        {
            string[] arrcred = GlobalVariableClass.Base64Decode(GlobalVariableClass.Base64Decode(Request.Cookies["ASP_NET_CD"].Value.ToString())).Split(',');
            var obj = conn.Query<_NEW_EMPLOYEE>(@"SELECT * FROM EPAY_V2_CLIENT_EMPLOYEE_HIST where GroupCode=@GroupCode AND 
            (first_name like @first_name OR last_name like @last_name OR email=@email)", new
            {
                GroupCode = arrcred[1].ToString(),
                first_name = "%" + txtsearch + "%",
                last_name = "%" + txtsearch + "%",
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
            return results;
        }

        [HttpPost]
        public JsonResult getInfo(string ID)
        {
            var obj = conn.Query<_LRWEB_V1_CUSTOMER_PREVIEW>(@"select a.ID as appID,a.*, b.* from LRWEB_V1_CUSTOMER_APPLICATION as a LEFT JOIN LRWEB_V1_CUSTOMER_INFO as b ON a.referenceNumber = b.ID  where a.ID =@ID", new
            {
                ID = ID
            }).ToList();
            List<_LRWEB_V1_CUSTOMER_PREVIEW> results = new List<_LRWEB_V1_CUSTOMER_PREVIEW>();
            if (obj != null)
            {
                foreach (var list in obj)
                {
                    _LRWEB_V1_CUSTOMER_PREVIEW lr = new _LRWEB_V1_CUSTOMER_PREVIEW();
                    lr.appID = list.appID;
                    lr.CIFno = list.CIFno;
                    lr.name = list.name;
                    lr.application_number = list.application_number;
                    lr.referenceNumber = list.referenceNumber;
                    lr.netIncome = list.netIncome;
                    lr.netPay = list.netPay;
                    lr.otherLoans = list.otherLoans;
                    lr.loanAmount = list.loanAmount;
                    lr.loanTerms = list.loanTerms;
                    lr.loanPurpose = list.loanPurpose;
                    lr.loanPurposeID = list.loanPurposeID;
                    lr.loanPurposeOthers = list.loanPurposeOthers;
                    lr.creditOption = list.creditOption;
                    lr.creditOptionID = list.creditOptionID;
                    lr.nameToDisplay = list.nameToDisplay;
                    lr.accountNumber = list.accountNumber;
                    lr.bankName = list.bankName;
                    lr.attachment1 = list.attachment1;
                    lr.attachment2 = list.attachment2;
                    lr.attachment3 = list.attachment3;
                    lr.attachment4 = list.attachment4;
                    lr.attachment5 = list.attachment5;
                    lr.attachment6 = list.attachment6;
                    lr.attachment7 = list.attachment7;
                    lr.attachment8 = list.attachment8;
                    lr.attachment9 = list.attachment9;
                    lr.attachment10 = list.attachment10;
                    lr.legalID = list.legalID;
                    lr.nameOnID = list.nameOnID;
                    lr.documentName = list.documentName;
                    lr.documentNameID = list.documentNameID;
                    lr.issueAuth = list.issueAuth;
                    lr.issueAuthID = list.issueAuthID;
                    lr.issueDate = list.issueDate;
                    lr.expirationDate = list.expirationDate;
                    lr.salutation = list.salutation;
                    lr.firstName = list.firstName;
                    lr.middleName = list.middleName;
                    lr.lastName = list.lastName;
                    lr.suffix = (list.suffix != null ? list.suffix : "");
                    lr.birthPlace = list.birthPlace;
                    lr.birthDate = list.birthDate;
                    lr.age = list.age;
                    lr.gender = list.gender;
                    lr.religion = list.religion;
                    lr.citizenship = list.citizenship;
                    lr.civilStatus = list.civilStatus;
                    lr.civilStatusID = list.civilStatusID;
                    lr.TIN = list.TIN;
                    lr.mothersMaidenName = list.mothersMaidenName;
                    lr.SSS = (list.SSS != null ? list.SSS : "");
                    lr.GSIS = (list.GSIS != null ? list.GSIS : "");
                    lr.spouseSalutation = (list.spouseSalutation != null ? list.spouseSalutation : "");
                    lr.spouseFirstname = (list.spouseFirstname != null ? list.spouseFirstname : "");
                    lr.spouseMiddlename = (list.spouseMiddlename != null ? list.spouseMiddlename : "");
                    lr.spouseLastname = (list.spouseLastname != null ? list.spouseLastname : "");
                    lr.spouseSuffix = (list.spouseSuffix != null ? list.spouseSuffix : "");
                    lr.spouseGender = list.spouseGender;
                    lr.spouseBirthDate = list.spouseBirthDate;
                    lr.spouseAge = list.spouseAge;
                    lr.dependents = (list.dependents != null ? list.dependents : "0");
                    lr.spouseEmployer = list.spouseEmployer;
                    lr.spouseOccupation = list.spouseOccupation;
                    lr.spouseMonthlyIncome = list.spouseMonthlyIncome;
                    lr.spouseNumber = list.spouseNumber;
                    lr.Present_Address = list.Present_Address;
                    lr.Present_Province = list.Present_Province;
                    lr.Present_ProvinceID = list.Present_ProvinceID;
                    lr.Present_City = list.Present_City;
                    lr.Present_CityID = list.Present_CityID;
                    lr.Present_Barangay = list.Present_Barangay;
                    lr.Present_BarangayID = list.Present_BarangayID;
                    lr.Present_Country = list.Present_Country;
                    lr.Present_Zipcode = list.Present_Zipcode;
                    lr.Present_Ownership = list.Present_Ownership;
                    lr.Present_OwnershipID = list.Present_OwnershipID;
                    lr.Present_Years = list.Present_Years;
                    lr.Present_Months = list.Present_Months;
                    lr.Present_LengthOfStay = list.Present_LengthOfStay;
                    lr.Present_Telephone = list.Present_Telephone;
                    lr.Permanent_Address = list.Permanent_Address;
                    lr.Permanent_Province = list.Permanent_Province;
                    lr.Permanent_ProvinceID = list.Permanent_ProvinceID;
                    lr.Permanent_City = list.Permanent_City;
                    lr.Permanent_CityID = list.Permanent_CityID;
                    lr.Permanent_Barangay = list.Permanent_Barangay;
                    lr.Permanent_BarangayID = list.Permanent_BarangayID;
                    lr.Permanent_Country = list.Permanent_Country;
                    lr.Permanent_Zipcode = list.Permanent_Zipcode;
                    lr.Permanent_Ownership = list.Permanent_Ownership;
                    lr.Permanent_OwnershipID = list.Permanent_OwnershipID;
                    lr.Permanent_Years = list.Permanent_Years;
                    lr.Permanent_Months = list.Permanent_Months;
                    lr.Permanent_LengthOfStay = list.Permanent_LengthOfStay;
                    lr.Permanent_Telephone = list.Permanent_Telephone;
                    lr.dateHired = list.dateHired;
                    lr.tenure = list.tenure;
                    lr.employeeNumber = list.employeeNumber;
                    lr.rank = list.rank;
                    lr.department = list.department;
                    lr.monthlyAllowance = list.monthlyAllowance;
                    lr.occupation = list.occupation;
                    lr.natureOfWork = list.natureOfWork;
                    lr.natureOfWorkID = list.natureOfWorkID;
                    lr.sourceOfIncomeOthers = (list.sourceOfIncomeOthers != null ? list.sourceOfIncomeOthers : "");
                    lr.monthlyIncomeOthers = (list.monthlyIncomeOthers != null ? list.monthlyIncomeOthers : "0");
                    lr.Employer_Address = list.Employer_Address;
                    lr.Employer_Province = list.Employer_Province;
                    lr.Employer_ProvinceID = list.Employer_ProvinceID;
                    lr.Employer_City = list.Employer_City;
                    lr.Employer_CityID = list.Employer_CityID;
                    lr.Employer_Barangay = list.Employer_Barangay;
                    lr.Employer_BarangayID = list.Employer_BarangayID;
                    lr.Employer_Country = list.Employer_Country;
                    lr.Employer_Zipcode = list.Employer_Zipcode;
                    lr.Employer_Telephone = list.Employer_Telephone;
                    lr.employee_reference = list.employee_reference;
                    lr.status = list.status;
                    results.Add(lr);
                }
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult verifyClient(_LRWEB_V1_CUSTOMER_PREVIEW model_state)
        {
            DateTime serverTime = DateTime.Now;
            TimeZoneInfo tst = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
            DateTime dt2 = TimeZoneInfo.ConvertTime(serverTime, TimeZoneInfo.Local, tst);
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
            var path = HostingEnvironment.ApplicationPhysicalPath + "Areas\\ID_Storage\\";
            var pathFile1 = path + model_state.attachment1.ToString();
            var pathFile2 = path + model_state.attachment2.ToString();
            var pathFile3 = path + model_state.attachment3.ToString();
            var pathFile4 = path + model_state.attachment4.ToString();
            var pathFile5 = path + model_state.attachment5.ToString();
            var pathFile6 = path + model_state.attachment6.ToString();
            var pathFile7 = path + model_state.attachment7.ToString();
            var pathFile8 = path + model_state.attachment8.ToString();
            var pathFile9 = path + model_state.attachment9.ToString();
            var pathFile10 = path + model_state.attachment10.ToString();

            string[] arrcred = GlobalVariableClass.Base64Decode(GlobalVariableClass.Base64Decode(Request.Cookies["ASP_NET_CD"].Value.ToString())).Split(',');
            var user = "LRWEB";
            var agentCode = "LRWEB01";
            var groupCode = arrcred[1].ToString();
            var salutaion = model_state.salutation.ToUpper();
            var salutaionSpouse = model_state.spouseSalutation == null ? "" : model_state.spouseSalutation.ToUpper();
            var gender = model_state.gender == "MALE" ? "M" : "F";
            var email = conn.Query<string>("select email from LRWEB_V1_CLIENT_EMPLOYEE where IntRecord = (select employee_reference from LRWEB_V1_CUSTOMER_INFO where ID = (select referenceNumber from LRWEB_V1_CUSTOMER_APPLICATION where ID =@ID))", new
            {
                ID = model_state.appID
            }).Single();
            var mobilenumber = conn.Query<string>("select contactnum from LRWEB_V1_CLIENT_EMPLOYEE where IntRecord = (select employee_reference from LRWEB_V1_CUSTOMER_INFO where ID = (select referenceNumber from LRWEB_V1_CUSTOMER_APPLICATION where ID =@ID))", new
            {
                ID = model_state.appID
            }).Single();
            var schemeCode = conn.Query<string>("Select schemeCode from LRWEB_V1_CLIENTS where groupCode=@groupCode", new
            {
                groupCode = groupCode
            }).Single();
            var sol_id = conn.Query<string>("Select sol_id from LRWEB_V1_CLIENTS where groupCode=@groupCode", new
            {
                groupCode = groupCode
            }).Single();
            var agency = conn.Query<string>("Select clientName from LRWEB_V1_CLIENTS where groupCode=@groupCode", new
            {
                groupCode = groupCode
            }).Single();
            var spouseBirthDate = "";
            DateTime sbd;
            DateTime dh = DateTime.Parse(model_state.dateHired);
            DateTime bd = DateTime.Parse(model_state.birthDate);
            if (DateTime.TryParse(model_state.spouseBirthDate, out sbd))
            {
                spouseBirthDate = sbd.ToString("MM/dd/yyyy");
            }
            //DateTime? sbd = String.IsNullOrEmpty(model_state.spouseBirthDate) ? (DateTime?)null : DateTime.Parse(model_state.spouseBirthDate);
            var dateHired = dh.ToString("MM/dd/yyyy");
            var birthDate = bd.ToString("MM/dd/yyyy");
            var birthDate2 = bd.ToString("m/D/Y");
            var civilStatus = model_state.civilStatus.ToUpper();

            var cust_id = model_state.CIFno;

            var applicationNumber = AELcon.Query<string>(@"call usp_bfk_add_application(@pi_application_number,@pi_employee_number,@pi_last_name,@pi_first_name,@pi_middle_name,@pi_birth_date,@pi_date_hired,@pi_age,@pi_tenure,@pi_cust_id,@pi_agency,@pi_nthp,@pi_checkdread,@pi_loan_purpose,@pi_loan_purpose_others,@pi_region_code,@pi_division_code,@pi_station_code,@pi_agent_code,@pi_user_sol_id,@pi_user,@pi_queue_rowno,@pi_cashout_flag,@pi_chkEntryDHired,@pi_chkEntryBirthdate,@pi_chkLoanAmount,@pi_chkLoanTerm)", new
            {
                pi_application_number = 0,
                pi_employee_number = model_state.employeeNumber,
                pi_last_name = model_state.lastName,
                pi_first_name = model_state.firstName,
                pi_middle_name = model_state.middleName,
                pi_birth_date = birthDate,
                pi_date_hired = dateHired,
                pi_age = model_state.age,
                pi_tenure = model_state.tenure,
                pi_cust_id = cust_id,
                pi_agency = agency,
                pi_nthp = model_state.netIncome,
                pi_checkdread = "N",
                pi_loan_purpose = model_state.loanPurposeID,
                pi_loan_purpose_others = model_state.loanPurposeOthers,
                pi_region_code = "",
                pi_division_code = "",
                pi_station_code = "",
                pi_agent_code = agentCode,
                pi_user_sol_id = sol_id,
                pi_user = user,
                pi_queue_rowno = 0,
                pi_cashout_flag = "N",
                pi_chkEntryDHired = "N",
                pi_chkEntryBirthdate = "N",
                pi_chkLoanAmount = "N",
                pi_chkLoanTerm = "N"
            }).Single();

            conn.Execute(@"UPDATE LRWEB_V1_CUSTOMER_APPLICATION SET status='ONPROCESS', sent_status='1',dateSubmitted=@dt2,application_number=@appNumber,hrID=@hrID where ID=@appID", new
            {
                appNumber = applicationNumber,
                appID = model_state.appID,
                hrID = arrcred[0].ToString(),
                dt2 = dt2
            });

            var toupload = ZipThis(pathFile1, pathFile2, pathFile3, pathFile4, pathFile5, pathFile6, pathFile7, pathFile8, pathFile9, pathFile10, applicationNumber);

            using (var client = new HttpClient())
            {
                var Bearer = Session["auththoken"].ToString();
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                var authToken = Encoding.ASCII.GetBytes($"{Bearer}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        Bearer);
                var content = new MultipartFormDataContent();
                var filestream = new FileStream(toupload, FileMode.Open);
                var fileName = System.IO.Path.GetFileName(toupload);
                content.Add(new StreamContent(filestream), "attachment", fileName);
                content.Add(new StringContent(applicationNumber), "appno");
                client.BaseAddress = new Uri(Uristring);
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsync("uploadAttachment", content).Result;

                var contents = response.Content.ReadAsStringAsync().Result;
                JObject res = JObject.Parse(contents);
                if (System.IO.File.Exists(toupload))
                {
                    //System.IO.File.Delete(toupload);
                }
                try
                {
                    var strquery = "INSERT INTO API_LOGS (application_number,api,message,Date) values ('" + applicationNumber + "','uploadAttachment','" + contents + "','" + dt2 + "')";
                    conn.Execute(strquery);
                }
                catch (Exception e)
                {

                }
            }
            var saveAddressQuery = "call usp_amr_save_customer_address(" +
                "'" + cust_id + "'," +
                "'" + model_state.Permanent_Address + " " + model_state.Permanent_Barangay + "'," +
                "'" + model_state.Permanent_Province + "'," +
                "'" + model_state.Permanent_City + "'," +
                "'" + model_state.Permanent_Country + "'," +
                "'" + model_state.Permanent_Zipcode + "'," +
                "'" + model_state.Present_Address + " " + model_state.Present_Barangay + "'," +
                "'" + model_state.Present_Province + "'," +
                "'" + model_state.Present_City + "'," +
                "'" + model_state.Present_Country + "'," +
                "'" + model_state.Present_Zipcode + "'," +
                "'" + model_state.Permanent_OwnershipID + "'," +
                "'" + model_state.Permanent_Years + "'," +
                "'" + model_state.Permanent_Months + "'," +
                "'" + model_state.Present_OwnershipID + "'," +
                "'" + model_state.Present_Years + "'," +
                "'" + model_state.Present_Months + "'," +
                "'" + user + "')";


            var saveCustomerEmployment = "Call usp_amr_save_customer_employment('" + cust_id + "','" + model_state.Employer_Zipcode + "','" + model_state.occupation + "')";
            var saveCustomerMaster = "Call usp_amr_save_customer_master(" +
                "'" + cust_id + "'," +
                "'" + model_state.TIN + "'," +
                "'" + salutaion + "'," +
                "'" + model_state.lastName + "'," +
                "'" + model_state.firstName + "'," +
                "'" + model_state.middleName + "'," +
                "'" + birthDate + "'," +
                "'" + gender + "'," +
                "'" + model_state.birthPlace + "'," +
                "'" + civilStatus + "'," +
                "'" + email + "'," +
                "'" + model_state.GSIS + "'," +
                "'" + model_state.SSS + "'," +
                "'" + mobilenumber + "'," +
                "'" + model_state.Present_Telephone + "'," +
                "''," +
                "'" + model_state.employeeNumber + "'," +
                "''," +
                "''," +
                "''," +
                "'" + model_state.dependents + "'," +
                "'" + model_state.Permanent_Telephone + "'," +
                "'" + user + "'," +
                "''," +
                "''," +
                "''," +
                "''," +
                "''," +
                "''" +
                ")";

            var saveSpouse = "Call usp_amr_save_customer_spouse(" +
                "'" + cust_id + "'," +
                "'" + model_state.spouseFirstname + "'," +
                "'" + model_state.spouseMiddlename + "'," +
                "'" + model_state.spouseLastname + "'," +
                "'" + spouseBirthDate + "'," +
                "''," +
                "'" + model_state.spouseOccupation + "'," +
                "''," +
                "'" + model_state.spouseEmployer + "'," +
                "'" + model_state.spouseMonthlyIncome + "'," +
                "'" + model_state.spouseNumber + "'" +
                ")";
            var steroids = "Call usp_amr_steroids_engine_t24(" +
                "'" + applicationNumber + "'," +
                "'" + user + "'," +
                "'" + cust_id + "'," +
                "'" + agency + "'," +
                "'" + model_state.netIncome + "'," +
                "'" + model_state.loanAmount + "'," +
                "'" + schemeCode + "'," +
                "'" + model_state.loanTerms + "'," +
                "'" + sol_id + "'," +
                "'Y'," +
                "0," +
                "0," +
                "0," +
                "0," +
                "0," +
                "'" + model_state.loanAmount + "'," +
                "0," +
                "0," +
                "0," +
                "0," +
                "0," +
                "'N'," +
                "'" + sol_id + "'," +
                "'" + user + "'," +
                "''," +
                "''," +
                "0," +
                "'" + birthDate + "'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "0," +
                "''" +
                ")";

            var obj = AELcon.Query<_steroid_return>(steroids).ToList();
            var loan_amount = obj[0].loan_amount.Replace(",", "");
            var new_deduction = obj[0].new_deduction.Replace(",", "");
            var nthp = obj[0].nthp.Replace(",", "");
            var old_deduction = obj[0].old_deduction.Replace(",", "");
            var pli_deduction = obj[0].pli_deduction.Replace(",", "");
            var total_nthp = obj[0].total_nthp.Replace(",", "");
            var onqueue = obj[0].onqueue.Replace(",", "");
            var incoming_deduction = obj[0].incoming_deduction.Replace(",", "");
            var net_nthp_after_ded = obj[0].net_nthp_after_ded.Replace(",", "");
            var service_fee = obj[0].service_fee.Replace(",", "");
            var mri_fee = obj[0].mri_fee.Replace(",", "");
            var dread_amount = obj[0].dread_amount.Replace(",", "");
            var processing_fee = obj[0].processing_fee.Replace(",", "");
            var taxes = obj[0].taxes.Replace(",", "");
            var docstamp = obj[0].docstamp.Replace(",", "");
            var eir = obj[0].eir.Replace(",", "");
            var irr = obj[0].irr.Replace(",", "");
            var redeemed_amount = obj[0].redeemed_amount.Replace(",", "");
            var v_max_loan_fixed = obj[0].v_max_loan_fixed.Replace(",", "");
            var net_proceeds = obj[0].net_proceeds.Replace(",", "");
            var aggregate_amount = obj[0].aggregate_amount.Replace(",", "");
            var other_amount = obj[0].other_amount.Replace(",", "");
            var v_mri_refund = obj[0].v_mri_refund.Replace(",", "");
            var v_dread_refund = obj[0].v_dread_refund.Replace(",", "");

            var loandetails = "call usp_amr_save_loan_detail_ranger(" +
                "'" + applicationNumber + "'," +
                "'" + obj[0].term + "'," +
                "'" + obj[0].month_lag + "'," +
                "'" + obj[0].interest_rate + "'," +
                "0," +
                "'" + loan_amount + "'," +
                "'" + new_deduction + "'," +
                "0," +
                "'" + obj[0].old_balance + "'," +
                "'" + nthp + "'," +
                "'" + old_deduction + "'," +
                "'" + pli_deduction + "'," +
                "'" + total_nthp + "'," +
                "'" + new_deduction + "'," +
                "'" + onqueue + "'," +
                "'" + incoming_deduction + "'," +
                "'" + net_nthp_after_ded + "'," +
                "'" + service_fee + "'," +
                "'" + mri_fee + "'," +
                "'" + dread_amount + "'," +
                "'" + processing_fee + "'," +
                "'" + taxes + "'," +
                "'" + docstamp + "'," +
                "'" + eir + "'," +
                "'" + irr + "'," +
                "'" + redeemed_amount + "'," +
                "'" + v_max_loan_fixed + "'," +
                "'" + obj[0].cash_out + "'," +
                "'" + net_proceeds + "'," +
                "'" + aggregate_amount + "'," +
                "15," +
                "0," +
                "0," +
                "0," +
                "0," +
                "0," +
                "0," +
                "0," +
                "'" + other_amount + "'," +
                "0," +
                "'" + obj[0].pn_start_pay_date + "'," +
                "'" + obj[0].pn_maturity_date + "'," +
                "'" + obj[0].loan_type + "'," +
                "'" + obj[0].scheme_code + "'," +
                "'" + v_mri_refund + "'," +
                "'" + v_dread_refund + "'," +
                "'" + user + "')";
            var beneloan = "call usp_jaf_save_beneloans(" +
                "'" + applicationNumber + "'," +
                "'" + cust_id + "'," +
                "'" + model_state.firstName + "'," +
                "'" + model_state.lastName + "'," +
                "'" + model_state.middleName + "'," +
                "'" + gender + "'," +
                "'" + civilStatus + "'," +
                "'" + model_state.Present_Address + " " + model_state.Present_Barangay + "'," +
                "'" + model_state.Permanent_Address + " " + model_state.Permanent_Barangay + "'," +
                "'" + birthDate + "'," +
                "'" + model_state.birthPlace + "'," +
                "'" + model_state.citizenship + "'," +
                "'" + model_state.Permanent_Zipcode + "'," +
                "'" + model_state.religion + "'," +
                "'" + email + "'," +
                "'" + model_state.TIN + "'," +
                "'" + model_state.GSIS + "'," +
                "'" + model_state.SSS + "'," +
                "'" + model_state.Present_Telephone + "'," +
                "'" + mobilenumber + "'," +
                "'" + user + "'," +
                "null," +
                "null," +
                "'" + model_state.netIncome + "'," +
                "''," +
                "''," +
                "''," +
                "''," +
                "null," +
                "''," +
                "''," +
                "''," +
                "''," +
                "''," +
                "'" + agency + "'," +
                "'" + model_state.natureOfWork + "'," +
                "'" + model_state.occupation + "'," +
                "'" + model_state.Employer_Address + "'," +
                "'" + model_state.netIncome + "'," +
                "'" + dateHired + "'," +
                "'" + model_state.monthlyAllowance + "'," +
                "'" + model_state.department + "'," +
                "'" + model_state.employeeNumber + "'," +
                "'" + model_state.tenure + "'," +
                "''," +
                "''," +
                "''," +
                "''," +
                "0," +
                "0," +
                "'" + model_state.sourceOfIncomeOthers + "'," +
                "'" + model_state.monthlyIncomeOthers + "'," +
                "'" + model_state.Permanent_Province + "'," +
                "'" + model_state.Permanent_City + "'," +
                "'" + model_state.Present_Province + "'," +
                "'" + model_state.Present_City + "'," +
                "'" + model_state.Present_Zipcode + "'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "'N')";

            AELcon.Execute(loandetails);
            AELcon.Execute(saveCustomerMaster);
            AELcon.Execute(saveAddressQuery);
            AELcon.Execute(saveCustomerEmployment);
            AELcon.Execute(saveSpouse);
            AELcon.Execute(beneloan);

            

            AELcon.Execute(@"call usp_bfk_update_t_loan_stat(@appNumber,'ONPROCESS')", new
            {
                appNumber = applicationNumber
            });

            

            return Json("Success!", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult reverifyClient(_LRWEB_V1_CUSTOMER_PREVIEW model_state)
        {
            DateTime serverTime = DateTime.Now;
            TimeZoneInfo tst = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
            DateTime dt2 = TimeZoneInfo.ConvertTime(serverTime, TimeZoneInfo.Local, tst);
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
            var path = HostingEnvironment.ApplicationPhysicalPath + "Areas\\ID_Storage\\";
            var pathFile1 = path + model_state.attachment1.ToString();
            var pathFile2 = path + model_state.attachment2.ToString();
            var pathFile3 = path + model_state.attachment3.ToString();
            var pathFile4 = path + model_state.attachment4.ToString();
            var pathFile5 = path + model_state.attachment5.ToString();
            var pathFile6 = path + model_state.attachment6.ToString();
            var pathFile7 = path + model_state.attachment7.ToString();
            var pathFile8 = path + model_state.attachment8.ToString();
            var pathFile9 = path + model_state.attachment9.ToString();
            var pathFile10 = path + model_state.attachment10.ToString();

            string[] arrcred = GlobalVariableClass.Base64Decode(GlobalVariableClass.Base64Decode(Request.Cookies["ASP_NET_CD"].Value.ToString())).Split(',');
            var user = "LRWEB";
            var agentCode = "LRWEB01";
            var groupCode = arrcred[1].ToString();
            var salutaion = model_state.salutation.ToUpper();
            var salutaionSpouse = model_state.spouseSalutation == null ? "" : model_state.spouseSalutation.ToUpper();
            var gender = model_state.gender == "MALE" ? "M" : "F";
            var email = conn.Query<string>("select email from LRWEB_V1_CLIENT_EMPLOYEE where IntRecord = (select employee_reference from LRWEB_V1_CUSTOMER_INFO where ID = (select referenceNumber from LRWEB_V1_CUSTOMER_APPLICATION where ID =@ID))", new
            {
                ID = model_state.appID
            }).Single();
            var mobilenumber = conn.Query<string>("select contactnum from LRWEB_V1_CLIENT_EMPLOYEE where IntRecord = (select employee_reference from LRWEB_V1_CUSTOMER_INFO where ID = (select referenceNumber from LRWEB_V1_CUSTOMER_APPLICATION where ID =@ID))", new
            {
                ID = model_state.appID
            }).Single();
            var schemeCode = conn.Query<string>("Select schemeCode from LRWEB_V1_CLIENTS where groupCode=@groupCode", new
            {
                groupCode = groupCode
            }).Single();
            var sol_id = conn.Query<string>("Select sol_id from LRWEB_V1_CLIENTS where groupCode=@groupCode", new
            {
                groupCode = groupCode
            }).Single();
            var agency = conn.Query<string>("Select clientName from LRWEB_V1_CLIENTS where groupCode=@groupCode", new
            {
                groupCode = groupCode
            }).Single();
            var spouseBirthDate = "";
            DateTime sbd;
            DateTime dh = DateTime.Parse(model_state.dateHired);
            DateTime bd = DateTime.Parse(model_state.birthDate);
            if (DateTime.TryParse(model_state.spouseBirthDate, out sbd))
            {
                spouseBirthDate = sbd.ToString("MM/dd/yyyy");
            }
            //DateTime? sbd = String.IsNullOrEmpty(model_state.spouseBirthDate) ? (DateTime?)null : DateTime.Parse(model_state.spouseBirthDate);
            var dateHired = dh.ToString("MM/dd/yyyy");
            var birthDate = bd.ToString("MM/dd/yyyy");
            var birthDate2 = bd.ToString("m/D/Y");
            var civilStatus = model_state.civilStatus.ToUpper();

            var cust_id = model_state.CIFno;

            var applicationNumber = AELcon.Query<string>(@"call usp_bfk_add_application(@pi_application_number,@pi_employee_number,@pi_last_name,@pi_first_name,@pi_middle_name,@pi_birth_date,@pi_date_hired,@pi_age,@pi_tenure,@pi_cust_id,@pi_agency,@pi_nthp,@pi_checkdread,@pi_loan_purpose,@pi_loan_purpose_others,@pi_region_code,@pi_division_code,@pi_station_code,@pi_agent_code,@pi_user_sol_id,@pi_user,@pi_queue_rowno,@pi_cashout_flag,@pi_chkEntryDHired,@pi_chkEntryBirthdate,@pi_chkLoanAmount,@pi_chkLoanTerm)", new
            {
                pi_application_number = model_state.application_number,
                pi_employee_number = model_state.employeeNumber,
                pi_last_name = model_state.lastName,
                pi_first_name = model_state.firstName,
                pi_middle_name = model_state.middleName,
                pi_birth_date = birthDate,
                pi_date_hired = dateHired,
                pi_age = model_state.age,
                pi_tenure = model_state.tenure,
                pi_cust_id = cust_id,
                pi_agency = agency,
                pi_nthp = model_state.netIncome,
                pi_checkdread = "N",
                pi_loan_purpose = model_state.loanPurposeID,
                pi_loan_purpose_others = model_state.loanPurposeOthers,
                pi_region_code = "",
                pi_division_code = "",
                pi_station_code = "",
                pi_agent_code = agentCode,
                pi_user_sol_id = sol_id,
                pi_user = user,
                pi_queue_rowno = 0,
                pi_cashout_flag = "N",
                pi_chkEntryDHired = "N",
                pi_chkEntryBirthdate = "N",
                pi_chkLoanAmount = "N",
                pi_chkLoanTerm = "N"
            }).Single();
            conn.Execute(@"UPDATE LRWEB_V1_CUSTOMER_APPLICATION SET status='ONPROCESS', sent_status='1',dateSubmitted=@dt2,application_number=@appNumber,hrID=@hrID where ID=@appID", new
            {
                appNumber = applicationNumber,
                appID = model_state.appID,
                hrID = arrcred[0].ToString(),
                dt2 = dt2
            });
            var toupload = ZipThis(pathFile1, pathFile2, pathFile3, pathFile4, pathFile5, pathFile6, pathFile7, pathFile8, pathFile9, pathFile10, applicationNumber);

            using (var client = new HttpClient())
            {
                var Bearer = Session["auththoken"].ToString();
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                var authToken = Encoding.ASCII.GetBytes($"{Bearer}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        Bearer);
                var content = new MultipartFormDataContent();
                var filestream = new FileStream(toupload, FileMode.Open);
                var fileName = System.IO.Path.GetFileName(toupload);
                content.Add(new StreamContent(filestream), "attachment", fileName);
                content.Add(new StringContent(applicationNumber), "appno");
                client.BaseAddress = new Uri(Uristring);
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsync("uploadAttachment", content).Result;

                var contents = response.Content.ReadAsStringAsync().Result;
                JObject res = JObject.Parse(contents);
                if (System.IO.File.Exists(toupload))
                {
                    //System.IO.File.Delete(toupload);
                }
                try
                {
                    var strquery = "INSERT INTO API_LOGS (application_number,api,message,Date) values ('" + applicationNumber + "','uploadAttachment','" + contents + "','" + dt2 + "')";
                    conn.Execute(strquery);
                }
                catch (Exception e)
                {

                }
            }
            var saveAddressQuery = "call usp_amr_save_customer_address(" +
                "'" + cust_id + "'," +
                "'" + model_state.Permanent_Address + " " + model_state.Permanent_Barangay + "'," +
                "'" + model_state.Permanent_Province + "'," +
                "'" + model_state.Permanent_City + "'," +
                "'" + model_state.Permanent_City + "'," +
                "'" + model_state.Permanent_Zipcode + "'," +
                "'" + model_state.Present_Address + " " + model_state.Present_Barangay + "'," +
                "'" + model_state.Present_Province + "'," +
                "'" + model_state.Present_City + "'," +
                "'" + model_state.Present_Country + "'," +
                "'" + model_state.Present_Zipcode + "'," +
                "'" + model_state.Permanent_OwnershipID + "'," +
                "'" + model_state.Permanent_Years + "'," +
                "'" + model_state.Permanent_Months + "'," +
                "'" + model_state.Present_OwnershipID + "'," +
                "'" + model_state.Present_Years + "'," +
                "'" + model_state.Present_Months + "'," +
                "'" + user + "')";


            var saveCustomerEmployment = "Call usp_amr_save_customer_employment('" + cust_id + "','" + model_state.Employer_Zipcode + "','" + model_state.occupation + "')";
            var dependents = "0";
            if (model_state.dependents == null)
            {
                dependents = "0";
            }
            else
            {
                dependents = model_state.dependents;
            };
            var saveCustomerMaster = "Call usp_amr_save_customer_master(" +
                "'" + cust_id + "'," +
                "'" + model_state.TIN + "'," +
                "'" + salutaion + "'," +
                "'" + model_state.lastName + "'," +
                "'" + model_state.firstName + "'," +
                "'" + model_state.middleName + "'," +
                "'" + birthDate + "'," +
                "'" + gender + "'," +
                "'" + model_state.birthPlace + "'," +
                "'" + civilStatus + "'," +
                "'" + email + "'," +
                "'" + model_state.GSIS + "'," +
                "'" + model_state.SSS + "'," +
                "'" + mobilenumber + "'," +
                "'" + model_state.Present_Telephone + "'," +
                "''," +
                "'" + model_state.employeeNumber + "'," +
                "''," +
                "''," +
                "''," +
                "'" + dependents + "'," +
                "'" + model_state.Permanent_Telephone + "'," +
                "'" + user + "'," +
                "''," +
                "''," +
                "''," +
                "''," +
                "''," +
                "''" +
                ")";

            var saveSpouse = "Call usp_amr_save_customer_spouse(" +
                "'" + cust_id + "'," +
                "'" + model_state.spouseFirstname + "'," +
                "'" + model_state.spouseMiddlename + "'," +
                "'" + model_state.spouseLastname + "'," +
                "'" + spouseBirthDate + "'," +
                "''," +
                "'" + model_state.spouseOccupation + "'," +
                "''," +
                "'" + model_state.spouseEmployer + "'," +
                "'" + model_state.spouseMonthlyIncome + "'," +
                "'" + model_state.spouseNumber + "'" +
                ")";
            var steroids = "Call usp_amr_steroids_engine_t24(" +
                "'" + applicationNumber + "'," +
                "'" + user + "'," +
                "'" + cust_id + "'," +
                "'" + agency + "'," +
                "'" + model_state.netIncome + "'," +
                "'" + model_state.loanAmount + "'," +
                "'" + schemeCode + "'," +
                "'" + model_state.loanTerms + "'," +
                "'" + sol_id + "'," +
                "'Y'," +
                "0," +
                "0," +
                "0," +
                "0," +
                "0," +
                "'" + model_state.loanAmount + "'," +
                "0," +
                "0," +
                "0," +
                "0," +
                "0," +
                "'N'," +
                "'" + sol_id + "'," +
                "'" + user + "'," +
                "''," +
                "''," +
                "0," +
                "'" + birthDate + "'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "0," +
                "''" +
                ")";

            var obj = AELcon.Query<_steroid_return>(steroids).ToList();
            var loan_amount = obj[0].loan_amount.Replace(",", "");
            var new_deduction = obj[0].new_deduction.Replace(",", "");
            var nthp = obj[0].nthp.Replace(",", "");
            var old_deduction = obj[0].old_deduction.Replace(",", "");
            var pli_deduction = obj[0].pli_deduction.Replace(",", "");
            var total_nthp = obj[0].total_nthp.Replace(",", "");
            var onqueue = obj[0].onqueue.Replace(",", "");
            var incoming_deduction = obj[0].incoming_deduction.Replace(",", "");
            var net_nthp_after_ded = obj[0].net_nthp_after_ded.Replace(",", "");
            var service_fee = obj[0].service_fee.Replace(",", "");
            var mri_fee = obj[0].mri_fee.Replace(",", "");
            var dread_amount = obj[0].dread_amount.Replace(",", "");
            var processing_fee = obj[0].processing_fee.Replace(",", "");
            var taxes = obj[0].taxes.Replace(",", "");
            var docstamp = obj[0].docstamp.Replace(",", "");
            var eir = obj[0].eir.Replace(",", "");
            var irr = obj[0].irr.Replace(",", "");
            var redeemed_amount = obj[0].redeemed_amount.Replace(",", "");
            var v_max_loan_fixed = obj[0].v_max_loan_fixed.Replace(",", "");
            var net_proceeds = obj[0].net_proceeds.Replace(",", "");
            var aggregate_amount = obj[0].aggregate_amount.Replace(",", "");
            var other_amount = obj[0].other_amount.Replace(",", "");
            var v_mri_refund = obj[0].v_mri_refund.Replace(",", "");
            var v_dread_refund = obj[0].v_dread_refund.Replace(",", "");

            var loandetails = "call usp_amr_save_loan_detail_ranger(" +
                "'" + applicationNumber + "'," +
                "'" + obj[0].term + "'," +
                "'" + obj[0].month_lag + "'," +
                "'" + obj[0].interest_rate + "'," +
                "0," +
                "'" + loan_amount + "'," +
                "'" + new_deduction + "'," +
                "0," +
                "'" + obj[0].old_balance + "'," +
                "'" + nthp + "'," +
                "'" + old_deduction + "'," +
                "'" + pli_deduction + "'," +
                "'" + total_nthp + "'," +
                "'" + new_deduction + "'," +
                "'" + onqueue + "'," +
                "'" + incoming_deduction + "'," +
                "'" + net_nthp_after_ded + "'," +
                "'" + service_fee + "'," +
                "'" + mri_fee + "'," +
                "'" + dread_amount + "'," +
                "'" + processing_fee + "'," +
                "'" + taxes + "'," +
                "'" + docstamp + "'," +
                "'" + eir + "'," +
                "'" + irr + "'," +
                "'" + redeemed_amount + "'," +
                "'" + v_max_loan_fixed + "'," +
                "'" + obj[0].cash_out + "'," +
                "'" + net_proceeds + "'," +
                "'" + aggregate_amount + "'," +
                "15," +
                "0," +
                "0," +
                "0," +
                "0," +
                "0," +
                "0," +
                "0," +
                "'" + other_amount + "'," +
                "0," +
                "'" + obj[0].pn_start_pay_date + "'," +
                "'" + obj[0].pn_maturity_date + "'," +
                "'" + obj[0].loan_type + "'," +
                "'" + obj[0].scheme_code + "'," +
                "'" + v_mri_refund + "'," +
                "'" + v_dread_refund + "'," +
                "'" + user + "')";
            var monthlyIncomeOthers = "0";
            if (model_state.monthlyIncomeOthers != null) monthlyIncomeOthers = model_state.monthlyIncomeOthers;
            var beneloan = "call usp_jaf_save_beneloans(" +
                "'" + applicationNumber + "'," +
                "'" + cust_id + "'," +
                "'" + model_state.firstName + "'," +
                "'" + model_state.lastName + "'," +
                "'" + model_state.middleName + "'," +
                "'" + gender + "'," +
                "'" + civilStatus + "'," +
                "'" + model_state.Present_Address + " " + model_state.Present_Barangay + "'," +
                "'" + model_state.Permanent_Address + " " + model_state.Permanent_Barangay + "'," +
                "'" + birthDate + "'," +
                "'" + model_state.birthPlace + "'," +
                "'" + model_state.citizenship + "'," +
                "'" + model_state.Permanent_Zipcode + "'," +
                "'" + model_state.religion + "'," +
                "'" + email + "'," +
                "'" + model_state.TIN + "'," +
                "'" + model_state.GSIS + "'," +
                "'" + model_state.SSS + "'," +
                "'" + model_state.Present_Telephone + "'," +
                "'" + mobilenumber + "'," +
                "'" + user + "'," +
                "null," +
                "null," +
                "'" + model_state.netIncome + "'," +
                "''," +
                "''," +
                "''," +
                "''," +
                "null," +
                "''," +
                "''," +
                "''," +
                "''," +
                "''," +
                "'" + agency + "'," +
                "'" + model_state.natureOfWork + "'," +
                "'" + model_state.occupation + "'," +
                "'" + model_state.Employer_Address + "'," +
                "'" + model_state.netIncome + "'," +
                "'" + dateHired + "'," +
                "'" + model_state.monthlyAllowance + "'," +
                "'" + model_state.department + "'," +
                "'" + model_state.employeeNumber + "'," +
                "'" + model_state.tenure + "'," +
                "''," +
                "''," +
                "''," +
                "''," +
                "0," +
                "0," +
                "'" + model_state.sourceOfIncomeOthers + "'," +
                "'" + monthlyIncomeOthers + "'," +
                "'" + model_state.Permanent_Province + "'," +
                "'" + model_state.Permanent_City + "'," +
                "'" + model_state.Present_Province + "'," +
                "'" + model_state.Present_City + "'," +
                "'" + model_state.Present_Zipcode + "'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "'N'," +
                "'N')";

            try
            {
                AELcon.Execute(loandetails);
                AELcon.Execute(saveCustomerMaster);
                AELcon.Execute(saveAddressQuery);
                AELcon.Execute(saveCustomerEmployment);
                AELcon.Execute(saveSpouse);
                AELcon.Execute(beneloan);
                AELcon.Execute(@"call usp_bfk_update_t_loan_stat(@appNumber,'ONPROCESS')", new
                {
                    appNumber = applicationNumber
                });
            }
            catch (Exception e)
            {
                var sqlstring = "Insert into API_LOGS (application_number,api,message,date) values ('" + applicationNumber + "','data sending','" + e.Message + "','" + dt2 + "')";
                conn.Execute(sqlstring);
                return Json("Somthing went wrong when sending data to AEL", JsonRequestBehavior.AllowGet);

            }
            return Json("Success!", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult resendAttachment(_LRWEB_V1_CUSTOMER_PREVIEW model_state)
        {
            DateTime serverTime = DateTime.Now;
            TimeZoneInfo tst = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");

            DateTime datenow = TimeZoneInfo.ConvertTime(serverTime, TimeZoneInfo.Local, tst);
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
            var path = HostingEnvironment.ApplicationPhysicalPath + "Areas\\ID_Storage\\";
            
            var applicationNumber = model_state.application_number;
            var toupload = path + applicationNumber + "-attachment.zip";
            using (var client = new HttpClient())
            {
                var Bearer = Session["auththoken"].ToString();
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                var authToken = Encoding.ASCII.GetBytes($"{Bearer}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                        Bearer);
                var content = new MultipartFormDataContent();
                var filestream = new FileStream(toupload, FileMode.Open);
                var fileName = System.IO.Path.GetFileName(toupload);
                content.Add(new StreamContent(filestream), "attachment", fileName);
                content.Add(new StringContent(applicationNumber), "appno");
                client.BaseAddress = new Uri(Uristring);
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsync("uploadAttachment", content).Result;

                var contents = response.Content.ReadAsStringAsync().Result;
                JObject res = JObject.Parse(contents);

                var sqlstring = "Insert into API_LOGS (application_number,api,message,date) values ('" + applicationNumber + "','uploadAttachment','" + res + "','" + datenow + "')";
                conn.Execute(sqlstring);
                return Json(contents, JsonRequestBehavior.AllowGet);
            }
            
        }

        public class _steroid_return
        {
            public string applicationNumber { get; set; }
            public string term { get; set; }
            public string month_lag { get; set; }
            public string interest_rate { get; set; }
            public string loan_amount { get; set; }
            public string old_balance { get; set; }
            public string nthp { get; set; }
            public string old_deduction { get; set; }
            public string pli_deduction { get; set; }
            public string total_nthp { get; set; }
            public string new_deduction { get; set; }
            public string onqueue { get; set; }
            public string incoming_deduction { get; set; }
            public string net_nthp_after_ded { get; set; }
            public string service_fee { get; set; }
            public string mri_fee { get; set; }
            public string dread_amount { get; set; }
            public string processing_fee { get; set; }
            public string taxes { get; set; }
            public string docstamp { get; set; }
            public string eir { get; set; }
            public string irr { get; set; }
            public string redeemed_amount { get; set; }
            public string v_max_loan_fixed { get; set; }
            public string cash_out { get; set; }
            public string net_proceeds { get; set; }
            public string aggregate_amount { get; set; }
            public string other_amount { get; set; }
            public string pn_start_pay_date { get; set; }
            public string pn_maturity_date { get; set; }
            public string loan_type { get; set; }
            public string scheme_code { get; set; }
            public string v_mri_refund { get; set; }
            public string v_dread_refund { get; set; }
        }

        public static string ZipThis(string path1, string path2, string path3, string path4, string path5, string path6, string path7, string path8, string path9, string path10, string appID)
        {
            var path = HostingEnvironment.ApplicationPhysicalPath + "\\Areas\\ID_Storage\\";
            var zipPath = path + appID + "-attachment.zip";
            System.IO.File.Delete(zipPath);
            // Create FileStream for output ZIP archive
            using (ZipArchive zip = ZipFile.Open(zipPath, ZipArchiveMode.Update))
            {
                zip.CreateEntryFromFile(path1, "picture" + Path.GetExtension(path1));
                zip.CreateEntryFromFile(path2, "payslip" + Path.GetExtension(path2));
                zip.CreateEntryFromFile(path3, "COE" + Path.GetExtension(path3));
                zip.CreateEntryFromFile(path4, "IDfront" + Path.GetExtension(path4));
                zip.CreateEntryFromFile(path5, "IDBack" + Path.GetExtension(path5));
                zip.CreateEntryFromFile(path6, "Signature" + Path.GetExtension(path6));
                zip.CreateEntryFromFile(path7, "CIFform" + Path.GetExtension(path7));
                zip.CreateEntryFromFile(path8, "ATMform" + Path.GetExtension(path8));
                zip.CreateEntryFromFile(path9, "APPform" + Path.GetExtension(path9));
                zip.CreateEntryFromFile(path10, "terms" + Path.GetExtension(path10));
            }
            return zipPath;
        }

        [HttpPost]
        public JsonResult Decline(string appID, string reason, string employee_reference)
        {
            conn.Execute(@"UPDATE LRWEB_V1_CUSTOMER_APPLICATION SET status=@status, 
                reason_decline=@remarks,dateDeclined = getdate()
                WHERE ID=@appID", new
            {
                status = "DECLINE",
                appID = appID,
                remarks = reason
            });
            DateTime serverTime = DateTime.Now;
            TimeZoneInfo tst = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
            DateTime dt2 = TimeZoneInfo.ConvertTime(serverTime, TimeZoneInfo.Local, tst);
            string[] arrcred = GlobalVariableClass.Base64Decode(GlobalVariableClass.Base64Decode(Request.Cookies["ASP_NET_CD"].Value.ToString())).Split(',');
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            dt = dt.AddDays(7);
            string token = "";
            token = GlobalVariableClass.RandomString(72);
            var getdate = conn.Query<string>(@"SELECT Expiration from LRWEB_V1_CLIENT_EMPLOYEE
            WHERE  IntRecord=@Tokens", new
            {
                Tokens = employee_reference
            }).Single();
            var CIFno = conn.Query<string>(@"SELECT CIFno from LRWEB_V1_CUSTOMER_APPLICATION WHERE ID=@appID", new
            {
                appID = appID
            }).Single();
            conn.Execute(@"UPDATE LRWEB_V1_CLIENT_EMPLOYEE SET SubmitStatus='00', reason_decline = @reason,Expiration=@expiration, tokenSender=@tokenSender, Tokens=@Tokens WHERE IntRecord=@id", new
            {
                id = employee_reference,
                expiration = dt.ToString("yyyyMMddHHmmss"),
                tokenSender = arrcred[6].ToString(),
                Tokens = token,
                reason = reason
            });

            using (var client = new HttpClient())
            {
                var appno = "0";
                var appname = "LRWEB";
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                var authToken = Encoding.ASCII.GetBytes($"{centraluname}:{centralpword}");
                client.BaseAddress = new Uri(centraluri);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

                var content = new MultipartFormDataContent();
                content.Add(new StringContent(CIFno), "mnemonic");
                string address = "delete_reserve_cifkey";
                var response = client.PostAsync(address, content).Result;
                var contents = response.Content.ReadAsStringAsync().Result;
                //JObject res = JObject.Parse(contents);
                try
                {
                    string para = "mnemonic: " + CIFno;
                    var strquery = "INSERT INTO API_LOGS (application_number,api,message,params,Date) values ('0','delete_reserve_cifkey','" + contents + "','" + para + "','" + dt2 + "')";
                    conn.Execute(strquery);
                }
                catch (Exception e)
                {

                }

                //return Json(contents, JsonRequestBehavior.AllowGet);
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }
        
        class auth
        {
            public string grant_type { get; set; }
        }
    }
}
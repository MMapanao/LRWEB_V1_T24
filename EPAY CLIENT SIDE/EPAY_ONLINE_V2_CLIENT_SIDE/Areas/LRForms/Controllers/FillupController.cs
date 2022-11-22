using BankAlertsBL;
using BusinessObjectEntities.BankAlerts.Entity;
using Dapper;
using LRWEB_V1_CLIENT_SIDE_T24.Areas.LRForms.ViewModel;
using LRWEB_V1_CLIENT_SIDE_T24.Areas.LRForms.Models;
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
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace LRWEB_V1_CLIENT_SIDE_T24.Areas.LRForms.Controllers
{
    public class FillupController : Controller
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMBANKconnection"].ToString());
        SqlConnection connBankAlerts = new SqlConnection(ConfigurationManager.ConnectionStrings["BANKALERTSconnection"].ToString());
        MySqlConnection AELcon = new MySqlConnection(ConfigurationManager.ConnectionStrings["AELConnectiont24"].ToString());
        string Uristring = ConfigurationManager.ConnectionStrings["unifiedapi"].ToString();
        string lookup = ConfigurationManager.ConnectionStrings["unifiedlookup"].ToString();
        string token = ConfigurationManager.ConnectionStrings["unifiedtoken"].ToString();
        string centraluri = ConfigurationManager.ConnectionStrings["centralapi"].ToString();
        string centraluname = ConfigurationManager.ConnectionStrings["centraluname"].ToString();
        string centralpword = ConfigurationManager.ConnectionStrings["centralpword"].ToString();

        DateTime dateTime = DateTime.UtcNow.Date;
        BLMapper blMapper = new BLMapper();
        public string authtoken = "";
        // GET: LRForms/Fillup
        public ActionResult Index()
        {
            _ViewModel_Form vm = new _ViewModel_Form();
            vm.vm_naturework = blMapper.workNatureBL.GET(connBankAlerts);
            vm.vm_occupations = blMapper.occupationBL.GET(AELcon);
            vm.vm_country = blMapper.countryBL.GET(connBankAlerts);
            Uri MyUrl = Request.Url;
            Session["Token"] = MyUrl.Query.ToString().Substring(MyUrl.Query.ToString().LastIndexOf('?') + 1);
            //return View();
            return ValidateResult(vm);
        }
        public ActionResult sample()
        {
            return View();
        }

        public ActionResult Returned()
        {
            _ViewModel_Form vm = new _ViewModel_Form();
            vm.vm_naturework = blMapper.workNatureBL.GET(connBankAlerts);
            vm.vm_occupations = blMapper.occupationBL.GET(AELcon);
            vm.vm_country = blMapper.countryBL.GET(connBankAlerts);
            Uri MyUrl = Request.Url;
            Session["Token"] = MyUrl.Query.ToString().Substring(MyUrl.Query.ToString().LastIndexOf('?') + 1);

            return ValidateReturn(vm);
        }

        public ActionResult ValidateResult(_ViewModel_Form vm)
        {
            Uri MyUrl = Request.Url;
            string token = MyUrl.Query.ToString().Substring(MyUrl.Query.ToString().LastIndexOf('?') + 1);
            string arrcred = "";
            var dt = DateTime.Now;
            int x = 0;
            string dddd = dt.ToString("yyyyMMddHHmmss");
            var obj = conn.Query<_EPAY_V2_CLIENT_EMPLOYEE>("SELECT ep.*, " +
            "(Select fcg.clientName from LRWEB_V1_CLIENTS as " +
            " fcg where fcg.groupCode = ep.GroupCode) as CompanyName " +
                " FROM LRWEB_V1_CLIENT_EMPLOYEE as ep WHERE ep.Tokens=@tokens and ep.Expiration >= @datetimenow", new
                {
                    tokens = token.ToString(),
                    datetimenow = dt.ToString("yyyyMMddHHmmss")
                }).ToList();
            _EPAY_V2_CLIENT_EMPLOYEE epay = new _EPAY_V2_CLIENT_EMPLOYEE();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    arrcred = row.GroupCode + "," + row.last_name + "," + row.first_name + "," + row.middle_name + "," +
                       row.contactnum + "," + row.email;
                    x++;
                    if (row.SubmitStatus == "1")
                    {
                        return RedirectToAction("AlreadyTaken", "Message");
                    }
                    epay.IntRecord = row.IntRecord;
                    epay.GroupCode = row.GroupCode;
                    epay.last_name = row.last_name;
                    epay.first_name = row.first_name;
                    epay.middle_name = row.middle_name;
                    epay.contactnum = row.contactnum;
                    epay.email = row.email;
                    epay.Tokens = row.Tokens;
                    epay.Expiration = row.Expiration;
                    epay.Status = row.Status;
                    epay.CompanyName = row.CompanyName;
                }
            }

            if (x == 0)
            {
                return RedirectToAction("ReferenceInvalid", "Message");
            }
            else
            {
                //using (var client = new HttpClient())
                //{

                //    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                //    var auth = new auth { username = Uriuname, password = Uripword };
                //    client.BaseAddress = new Uri(Uristring);
                //    client.DefaultRequestHeaders.Accept
                //        .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //    var response = client.PostAsync("auth/getToken", new StringContent(new JavaScriptSerializer().Serialize(auth), Encoding.UTF8, "application/json")).Result;

                //    var contents = response.Content.ReadAsStringAsync().Result;
                //    JObject res = JObject.Parse(contents);
                //    Session["auththoken"] = res["data"].ToString();

                //}
                //Session["auththoken"] = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzeXNfaWQiOiIxYTdkNTlmZS1lYTY0LTRiYzYtODNjNC1kMmJjMDc3NThlOTgiLCJ1c2VyUm9sZXMiOlsiQ09OU1VNRVIiLCJBRE1JTklTVFJBVE9SIl0sInVzZXJQb2xpY2llcyI6eyJDT01QQU5ZIjpbIkdFVCIsIkFSQ0hJVkUiLCJQT1NUIiwiUFVUIl0sIkNVU1RPTUVSIjpbIlBVVCIsIlBPU1QiLCJHRVQiLCJBUkNISVZFIl0sIkxPT0tVUCI6WyJQT1NUIiwiQVJDSElWRSIsIkdFVCIsIlBVVCJdLCJQT0xJQ1kiOlsiUE9TVCIsIkFSQ0hJVkUiLCJHRVQiLCJQVVQiXSwiUE9MSUNZTUFQIjpbIkFSQ0hJVkUiLCJQT1NUIiwiUFVUIiwiR0VUIl0sIlBVQyI6WyJHRVQiLCJQT1NUIiwiQVJDSElWRSIsIlBVVCJdLCJST0xFIjpbIlBPU1QiLCJQVVQiLCJHRVQiLCJBUkNISVZFIl0sIlJPTEVNQVAiOlsiQVJDSElWRSIsIlBVVCIsIkdFVCIsIlBPU1QiXSwiU09MIjpbIlBPU1QiLCJBUkNISVZFIiwiR0VUIiwiUFVUIl0sIlVTRVJTIjpbIkdFVCIsIlBVVCIsIlBPU1QiLCJQVVQiLCJBUkNISVZFIl19LCJpYXQiOjE2MDE2MDQ1NjAsImV4cCI6MTYwMTY5MDk2MH0.22-_iQOIsnXIoP2bectNfFv8qMy8bobUyLNsVC_aTdQ";

                var obj_clients = conn.Query<_EPAY_CLIENTS>(@" SELECT TOP 300 cl.*, 
                (Select (cred.firstname +' ' +cred.lastname) FROM LRWEB_V1_ADMIN_CRED AS cred where cred.IntRecord = cl.admin_id) AS encode_by,
                (Select interestRate FROM SCHEMECODE AS scode where scode.schemeCode = cl.schemeCode) AS interestRate,
                (Select bankCharge FROM SCHEMECODE AS scode where scode.schemeCode = cl.schemeCode) AS bankCharge
                FROM LRWEB_V1_CLIENTS as cl WHERE status='VERIFIED' and GroupCode=@groupcode ORDER BY ID DESC", new
                {
                    groupcode = epay.GroupCode
                }).ToList();
                _EPAY_CLIENTS model = new _EPAY_CLIENTS();
                if (obj != null)
                {
                    foreach (var row in obj_clients)
                    {
                        model.ID = row.ID;
                        model.AED = row.AED;
                        model.groupCode = row.groupCode;
                        model.clientName = row.clientName;
                        model.customerID = row.customerID;
                        model.status = row.status;
                        model.csbBranch = row.csbBranch;
                        model.admin_id = row.admin_id;
                        model.officeNumber = row.officeNumber;
                        model.encode_by = GlobalVariableClass.IFNULLresponseEmpty(row.encode_by).ToUpper();
                        model.addressLine = row.addressLine;
                        model.barangay = row.barangay;
                        model.province = row.province;
                        model.city = row.city;
                        model.barangayId = row.barangayId;
                        model.provinceId = row.provinceId;
                        model.cityId = row.cityId;
                        model.schemeCode = row.schemeCode;
                        model.interestRate = row.interestRate;
                        model.bankCharge = row.bankCharge;
                        model.classification = row.classification;
                        model.creditRatio = row.creditRatio;
                        model.sol_id = row.sol_id;
                        model.finacle_sol_id = row.finacle_sol_id;
                    }
                }



                vm.vm_loc = blMapper.provinceBL.Get(connBankAlerts);

                vm.vm_EPAY_V2_CLIENT_EMPLOYEE = epay;
                vm.vm_clients = model;
                Session["GroupCode"] = model.groupCode;
                Session["EmployeeID"] = epay.IntRecord;
                return View(vm);
            }
        }


        public ActionResult ValidateReturn(_ViewModel_Form vm)
        {
            var dt = DateTime.Now;
            Uri MyUrl = Request.Url;
            string token = MyUrl.Query.ToString().Substring(MyUrl.Query.ToString().LastIndexOf('?') + 1);
            
            string arrcred = "";
            int x = 0;
            string dddd = dt.ToString("yyyyMMddHHmmss");
            var obj = conn.Query<_CLIENT_INFO>("Select a.*, b.* from LRWEB_V1_CUSTOMER_APPLICATION as a left join LRWEB_V1_CUSTOMER_INFO as b on a.referenceNumber = b.ID where status = 'RETURNED' and token = @tokens and a.tokenExpire >= @datetimenow", new
            {
                tokens = token.ToString(),
                datetimenow = dt.ToString("yyyyMMddHHmmss")
            }).ToList();
            _CLIENT_INFO lrweb = new _CLIENT_INFO();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    x++;
                    lrweb.salutation = row.salutation;
                    lrweb.firstName = row.firstName;
                    lrweb.middleName = row.middleName;
                    lrweb.lastName = row.lastName;
                    lrweb.suffix = row.suffix;
                    lrweb.birthPlace = row.birthPlace;
                    lrweb.birthDate = row.birthDate;
                    lrweb.age = row.age;
                    lrweb.gender = row.gender;
                    lrweb.religion = row.religion;
                    lrweb.citizenship = row.citizenship;
                    lrweb.civilStatus = row.civilStatus;
                    lrweb.civilStatusID = row.civilStatusID;
                    lrweb.TIN = row.TIN;
                    lrweb.mothersMaidenName = row.mothersMaidenName;
                    lrweb.SSS = row.SSS;
                    lrweb.GSIS = row.GSIS;
                    lrweb.spouseSalutation = row.spouseSalutation;
                    lrweb.spouseFirstname = row.spouseFirstname;
                    lrweb.spouseMiddlename = row.spouseMiddlename;
                    lrweb.spouseLastname = row.spouseLastname;
                    lrweb.spouseSuffix = row.spouseSuffix;
                    lrweb.spouseGender = row.spouseGender;
                    lrweb.spouseBirthDate = row.spouseBirthDate;
                    lrweb.spouseAge = row.spouseAge;
                    lrweb.dependents = row.dependents;
                    lrweb.spouseEmployer = row.spouseEmployer;
                    lrweb.spouseOccupation = row.spouseOccupation;
                    lrweb.spouseMonthlyIncome = row.spouseMonthlyIncome;
                    lrweb.spouseNumber = row.spouseNumber;
                    lrweb.Present_Address = row.Present_Address;
                    lrweb.Present_Province = row.Present_Province;
                    lrweb.Present_ProvinceID = row.Present_ProvinceID;
                    lrweb.Present_City = row.Present_City;
                    lrweb.Present_CityID = row.Present_CityID;
                    lrweb.Present_Barangay = row.Present_Barangay;
                    lrweb.Present_BarangayID = row.Present_BarangayID;
                    lrweb.Present_Country = row.Present_Country;
                    lrweb.Present_Zipcode = row.Present_Zipcode;
                    lrweb.Present_Ownership = row.Present_Ownership;
                    lrweb.Present_OwnershipID = row.Present_OwnershipID;
                    lrweb.Present_Years = row.Present_Years;
                    lrweb.Present_Months = row.Present_Months;
                    lrweb.Present_LengthOfStay = row.Present_LengthOfStay;
                    lrweb.Present_Telephone = row.Present_Telephone;
                    lrweb.Permanent_Address = row.Permanent_Address;
                    lrweb.Permanent_Province = row.Permanent_Province;
                    lrweb.Permanent_ProvinceID = row.Permanent_ProvinceID;
                    lrweb.Permanent_City = row.Permanent_City;
                    lrweb.Permanent_CityID = row.Permanent_CityID;
                    lrweb.Permanent_Barangay = row.Permanent_Barangay;
                    lrweb.Permanent_BarangayID = row.Permanent_BarangayID;
                    lrweb.Permanent_Country = row.Permanent_Country;
                    lrweb.Permanent_Zipcode = row.Permanent_Zipcode;
                    lrweb.Permanent_Ownership = row.Permanent_Ownership;
                    lrweb.Permanent_OwnershipID = row.Permanent_OwnershipID;
                    lrweb.Permanent_Years = row.Permanent_Years;
                    lrweb.Permanent_Months = row.Permanent_Months;
                    lrweb.Permanent_LengthOfStay = row.Permanent_LengthOfStay;
                    lrweb.Permanent_Telephone = row.Permanent_Telephone;
                    lrweb.dateHired = row.dateHired;
                    lrweb.tenure = row.tenure;
                    lrweb.employeeNumber = row.employeeNumber;
                    lrweb.rank = row.rank;
                    lrweb.department = row.department;
                    lrweb.monthlyAllowance = row.monthlyAllowance;
                    lrweb.occupation = row.occupation;
                    lrweb.natureOfWork = row.natureOfWork;
                    lrweb.natureOfWorkID = row.natureOfWorkID;
                    lrweb.sourceOfIncomeOthers = row.sourceOfIncomeOthers;
                    lrweb.monthlyIncomeOthers = row.monthlyIncomeOthers;
                    lrweb.Employer_Address = row.Employer_Address;
                    lrweb.Employer_Province = row.Employer_Province;
                    lrweb.Employer_ProvinceID = row.Employer_ProvinceID;
                    lrweb.Employer_City = row.Employer_City;
                    lrweb.Employer_CityID = row.Employer_CityID;
                    lrweb.Employer_Barangay = row.Employer_Barangay;
                    lrweb.Employer_BarangayID = row.Employer_BarangayID;
                    lrweb.Employer_Country = row.Employer_Country;
                    lrweb.Employer_Zipcode = row.Employer_Zipcode;
                    lrweb.Employer_Telephone = row.Employer_Telephone;
                    lrweb.employee_reference = row.employee_reference;
                    lrweb.CIFno = row.CIFno;
                    lrweb.application_number = row.application_number;
                    lrweb.referenceNumber = row.referenceNumber;
                    lrweb.netIncome = row.netIncome;
                    lrweb.netPay = row.netPay;
                    lrweb.otherLoans = row.otherLoans;
                    lrweb.loanAmount = row.loanAmount;
                    lrweb.loanTerms = row.loanTerms;
                    lrweb.loanPurpose = row.loanPurpose;
                    lrweb.loanPurposeID = row.loanPurposeID;
                    lrweb.loanPurposeOthers = row.loanPurposeOthers;
                    lrweb.groupCode = row.groupCode;
                    lrweb.creditOption = row.creditOption;
                    lrweb.creditOptionID = row.creditOptionID;
                    lrweb.nameToDisplay = row.nameToDisplay;
                    lrweb.accountNumber = row.accountNumber;
                    lrweb.bankName = row.bankName;
                    lrweb.legalID = row.legalID;
                    lrweb.nameOnID = row.nameOnID;
                    lrweb.documentName = row.documentName;
                    lrweb.documentNameID = row.documentNameID;
                    lrweb.issueAuth = row.issueAuth;
                    lrweb.issueAuthID = row.issueAuthID;
                    lrweb.issueDate = row.issueDate;
                    lrweb.expirationDate = row.expirationDate;
                    lrweb.finacle_sol_id = row.finacle_sol_id;
                    lrweb.sol_id = row.sol_id;
                }
            }

            if (x == 0)
            {
                return RedirectToAction("ReferenceInvalid", "Message");
            }
            else
            {
                string appno = conn.Query<string>(@"SELECT application_number FROM LRWEB_V1_CUSTOMER_APPLICATION WHERE token=@tokens", new
                {
                    tokens = Session["Token"].ToString(),
                    datetimenow = dt.ToString("yyyyMMddHHmmss")
                }).Single();
                Session["appno"] = appno;
                //Session["Appno"] = Session["Token"].ToString().Substring(0, 3);
                //using (var client = new HttpClient())
                //{

                //    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                //    var auth = new auth { username = Uriuname, password = Uripword };
                //    client.BaseAddress = new Uri(Uristring);
                //    client.DefaultRequestHeaders.Accept
                //        .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //    var response = client.PostAsync("auth/getToken", new StringContent(new JavaScriptSerializer().Serialize(auth), Encoding.UTF8, "application/json")).Result;

                //    var contents = response.Content.ReadAsStringAsync().Result;
                //    JObject res = JObject.Parse(contents);
                //    Session["auththoken"] = res["data"].ToString();

                //}
                //Session["auththoken"] = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzeXNfaWQiOiIxYTdkNTlmZS1lYTY0LTRiYzYtODNjNC1kMmJjMDc3NThlOTgiLCJ1c2VyUm9sZXMiOlsiQ09OU1VNRVIiLCJBRE1JTklTVFJBVE9SIl0sInVzZXJQb2xpY2llcyI6eyJDT01QQU5ZIjpbIkdFVCIsIkFSQ0hJVkUiLCJQT1NUIiwiUFVUIl0sIkNVU1RPTUVSIjpbIlBVVCIsIlBPU1QiLCJHRVQiLCJBUkNISVZFIl0sIkxPT0tVUCI6WyJQT1NUIiwiQVJDSElWRSIsIkdFVCIsIlBVVCJdLCJQT0xJQ1kiOlsiUE9TVCIsIkFSQ0hJVkUiLCJHRVQiLCJQVVQiXSwiUE9MSUNZTUFQIjpbIkFSQ0hJVkUiLCJQT1NUIiwiUFVUIiwiR0VUIl0sIlBVQyI6WyJHRVQiLCJQT1NUIiwiQVJDSElWRSIsIlBVVCJdLCJST0xFIjpbIlBPU1QiLCJQVVQiLCJHRVQiLCJBUkNISVZFIl0sIlJPTEVNQVAiOlsiQVJDSElWRSIsIlBVVCIsIkdFVCIsIlBPU1QiXSwiU09MIjpbIlBPU1QiLCJBUkNISVZFIiwiR0VUIiwiUFVUIl0sIlVTRVJTIjpbIkdFVCIsIlBVVCIsIlBPU1QiLCJQVVQiLCJBUkNISVZFIl19LCJpYXQiOjE2MDE2MDQ1NjAsImV4cCI6MTYwMTY5MDk2MH0.22-_iQOIsnXIoP2bectNfFv8qMy8bobUyLNsVC_aTdQ";

                var obj_clients = conn.Query<_EPAY_CLIENTS>(@" SELECT TOP 300 cl.*, 
                (Select (cred.firstname +' ' +cred.lastname) FROM LRWEB_V1_ADMIN_CRED AS cred where cred.IntRecord = cl.admin_id) AS encode_by,
                (Select interestRate FROM SCHEMECODE AS scode where scode.schemeCode = cl.schemeCode) AS interestRate,
                (Select bankCharge FROM SCHEMECODE AS scode where scode.schemeCode = cl.schemeCode) AS bankCharge
                FROM LRWEB_V1_CLIENTS as cl WHERE status='VERIFIED' and GroupCode=@groupcode ORDER BY ID DESC", new
                {
                    groupcode = lrweb.groupCode
                }).ToList();
                _EPAY_CLIENTS model = new _EPAY_CLIENTS();
                if (obj != null)
                {
                    foreach (var row in obj_clients)
                    {
                        model.ID = row.ID;
                        model.AED = row.AED;
                        model.groupCode = row.groupCode;
                        model.clientName = row.clientName;
                        model.customerID = row.customerID;
                        model.status = row.status;
                        model.csbBranch = row.csbBranch;
                        model.admin_id = row.admin_id;
                        model.officeNumber = row.officeNumber;
                        model.encode_by = GlobalVariableClass.IFNULLresponseEmpty(row.encode_by).ToUpper();
                        model.addressLine = row.addressLine;
                        model.barangay = row.barangay;
                        model.province = row.province;
                        model.city = row.city;
                        model.barangayId = row.barangayId;
                        model.provinceId = row.provinceId;
                        model.cityId = row.cityId;
                        model.schemeCode = row.schemeCode;
                        model.interestRate = row.interestRate;
                        model.bankCharge = row.bankCharge;
                        model.classification = row.classification;
                        model.creditRatio = row.creditRatio;
                    }
                }

                var obj1 = conn.Query<_EPAY_V2_CLIENT_EMPLOYEE>("select * from LRWEB_V1_CLIENT_EMPLOYEE where IntRecord = @ID", new
                {
                    ID = lrweb.employee_reference
                }).ToList();
                _EPAY_V2_CLIENT_EMPLOYEE epay = new _EPAY_V2_CLIENT_EMPLOYEE();
                if (obj1 != null)
                {
                    foreach (var row in obj1)
                    {
                        arrcred = row.GroupCode + "," + row.last_name + "," + row.first_name + "," + row.middle_name + "," +
                           row.contactnum + "," + row.email;
                        x++;
                        
                        epay.IntRecord = row.IntRecord;
                        epay.GroupCode = row.GroupCode;
                        epay.last_name = row.last_name;
                        epay.first_name = row.first_name;
                        epay.middle_name = row.middle_name;
                        epay.contactnum = row.contactnum;
                        epay.email = row.email;
                        epay.Tokens = row.Tokens;
                        epay.Expiration = row.Expiration;
                        epay.Status = row.Status;
                        epay.CompanyName = row.CompanyName;
                    }
                }

                vm.vm_loc = blMapper.provinceBL.Get(connBankAlerts);
                vm.vm_EPAY_V2_CLIENT_EMPLOYEE = epay;
                vm.vm_client_info = lrweb;
                vm.vm_clients = model;
                Session["GroupCode"] = model.groupCode;
                Session["EmployeeID"] = epay.IntRecord;
                return View(vm);
            }
        }


        [HttpPost]
        public JsonResult SubmitEmployeeData(_EMPLOYEE_DATA model_state)
        {


            Session["ReferenceCode"] = ExecuteEmployeeData(model_state);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ChangeEmployeeData(_EMPLOYEE_DATA model_state)
        {
            Session["ReferenceCode"] = EditEmployeeData(model_state);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public JsonResult UploadPicture()
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                //Uploaded file
                //Use the following properties to get file's name, size and MIMEType
                int fileSize = file.ContentLength;
                string fileName = file.FileName;
                string mimeType = file.ContentType;
                System.IO.Stream fileContent = file.InputStream;
                //To save file, use SaveAs method

                //File will be saved in application root
                file.SaveAs(Server.MapPath("~/Areas/LRForms/Resources/ClientFiles/") + Session["ReferenceCode"].ToString() + "_" + (i + 1).ToString());
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public string ExecuteEmployeeData(_EMPLOYEE_DATA model_state)
        {
            _EMPLOYEE_DATA extract_data = new _EMPLOYEE_DATA();
            DateTime serverTime = DateTime.Now;
            TimeZoneInfo tst = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
            DateTime dt2 = TimeZoneInfo.ConvertTime(serverTime, TimeZoneInfo.Local, tst);
            //Uri MyUrl = Request.Url;
            //string token = MyUrl.Query.ToString().Substring(MyUrl.Query.ToString().LastIndexOf('?') + 1);

            var obj = conn.Query<int>(@"Insert into LRWEB_V1_CUSTOMER_INFO (salutation,firstName,middleName,lastName,suffix,birthPlace,birthDate,age,gender,religion,citizenship,civilStatus,civilStatusID,TIN,mothersMaidenName,SSS,GSIS,spouseSalutation,spouseFirstname,spouseMiddlename,spouseLastname,spouseSuffix,spouseGender,spouseBirthDate,spouseAge,dependents,spouseEmployer,spouseOccupation,spouseMonthlyIncome,spouseNumber,Present_Address,Present_Province,Present_ProvinceID,Present_City,Present_CityID,Present_Barangay,Present_BarangayID,Present_Country,Present_Zipcode,Present_Ownership,Present_OwnershipID,Present_Years,Present_Months,Present_LengthOfStay,Present_Telephone,Permanent_Address,Permanent_Province,Permanent_ProvinceID,Permanent_City,Permanent_CityID,Permanent_Barangay,Permanent_BarangayID,Permanent_Country,Permanent_Zipcode,Permanent_Ownership,Permanent_OwnershipID,Permanent_Years,Permanent_Months,Permanent_LengthOfStay,Permanent_Telephone,dateHired,tenure,employeeNumber,rank,department,monthlyAllowance,occupation,natureOfWork,natureOfWorkID,sourceOfIncomeOthers,monthlyIncomeOthers,Employer_Address,Employer_Province,Employer_ProvinceID,Employer_City,Employer_CityID,Employer_Barangay,Employer_BarangayID,Employer_Country,Employer_Zipcode,Employer_Telephone,employee_reference,dateCreated) VALUES " +
           "(@salutation,@firstName,@middleName,@lastName,@suffix,@birthPlace,@birthDate,@age,@gender,@religion,@citizenship,@civilStatus,@civilStatusID,@TIN,@mothersMaidenName,@SSS,@GSIS,@spouseSalutation,@spouseFirstname,@spouseMiddlename,@spouseLastname,@spouseSuffix,@spouseGender,@spouseBirthDate,@spouseAge,@dependents,@spouseEmployer,@spouseOccupation,@spouseMonthlyIncome,@spouseNumber,@Present_Address,@Present_Province,@Present_ProvinceID,@Present_City,@Present_CityID,@Present_Barangay,@Present_BarangayID,@Present_Country,@Present_Zipcode,@Present_Ownership,@Present_OwnershipID,@Present_Years,@Present_Months,@Present_LengthOfStay,@Present_Telephone,@Permanent_Address,@Permanent_Province,@Permanent_ProvinceID,@Permanent_City,@Permanent_CityID,@Permanent_Barangay,@Permanent_BarangayID,@Permanent_Country,@Permanent_Zipcode,@Permanent_Ownership,@Permanent_OwnershipID,@Permanent_Years,@Permanent_Months,@Permanent_LengthOfStay,@Permanent_Telephone,@dateHired,@tenure,@employeeNumber,@rank,@department,@monthlyAllowance,@occupation,@natureOfWork,@natureOfWorkID,@sourceOfIncomeOthers,@monthlyIncomeOthers,@Employer_Address,@Employer_Province,@Employer_ProvinceID,@Employer_City,@Employer_CityID,@Employer_Barangay,@Employer_BarangayID,@Employer_Country,@Employer_Zipcode,@Employer_Telephone,@employee_reference,@dt2); SELECT CAST(SCOPE_IDENTITY() as int)", new
           {
               salutation = model_state.salutation,
               firstName = model_state.firstName,
               middleName = model_state.middleName,
               lastName = model_state.lastName,
               suffix = model_state.suffix,
               birthPlace = model_state.birthPlace,
               birthDate = model_state.birthDate,
               age = model_state.age,
               gender = model_state.gender,
               religion = model_state.religion,
               citizenship = model_state.citizenship,
               civilStatus = model_state.civilStatus,
               civilStatusID = model_state.civilStatusID,
               TIN = model_state.TIN,
               mothersMaidenName = model_state.mothersMaidenName,
               SSS = model_state.SSS,
               GSIS = model_state.GSIS,
               spouseSalutation = model_state.spouseSalutation,
               spouseFirstname = model_state.spouseFirstname,
               spouseMiddlename = model_state.spouseMiddlename,
               spouseLastname = model_state.spouseLastname,
               spouseSuffix = model_state.spouseSuffix,
               spouseGender = model_state.spouseGender,
               spouseBirthDate = model_state.spouseBirthDate,
               spouseAge = model_state.spouseAge,
               dependents = model_state.dependents,
               spouseEmployer = model_state.spouseEmployer,
               spouseOccupation = model_state.spouseOccupation,
               spouseMonthlyIncome = model_state.spouseMonthlyIncome,
               spouseNumber = model_state.spouseNumber,
               Present_Address = model_state.Present_Address,
               Present_Province = model_state.Present_Province,
               Present_ProvinceID = model_state.Present_ProvinceID,
               Present_City = model_state.Present_City,
               Present_CityID = model_state.Present_CityID,
               Present_Barangay = model_state.Present_Barangay,
               Present_BarangayID = model_state.Present_BarangayID,
               Present_Country = model_state.Present_Country,
               Present_Zipcode = model_state.Present_Zipcode,
               Present_Ownership = model_state.Present_Ownership,
               Present_OwnershipID = model_state.Present_OwnershipID,
               Present_Years = model_state.Present_Years,
               Present_Months = model_state.Present_Months,
               Present_LengthOfStay = model_state.Present_LengthOfStay,
               Present_Telephone = model_state.Present_Telephone,
               Permanent_Address = model_state.Permanent_Address,
               Permanent_Province = model_state.Permanent_Province,
               Permanent_ProvinceID = model_state.Permanent_ProvinceID,
               Permanent_City = model_state.Permanent_City,
               Permanent_CityID = model_state.Permanent_CityID,
               Permanent_Barangay = model_state.Permanent_Barangay,
               Permanent_BarangayID = model_state.Permanent_BarangayID,
               Permanent_Country = model_state.Permanent_Country,
               Permanent_Zipcode = model_state.Permanent_Zipcode,
               Permanent_Ownership = model_state.Permanent_Ownership,
               Permanent_OwnershipID = model_state.Permanent_OwnershipID,
               Permanent_Years = model_state.Permanent_Years,
               Permanent_Months = model_state.Permanent_Months,
               Permanent_LengthOfStay = model_state.Permanent_LengthOfStay,
               Permanent_Telephone = model_state.Permanent_Telephone,
               dateHired = model_state.dateHired,
               tenure = model_state.tenure,
               employeeNumber = model_state.employeeNumber,
               rank = model_state.rank,
               department = model_state.department,
               monthlyAllowance = model_state.monthlyAllowance,
               occupation = model_state.occupation,
               natureOfWork = model_state.natureOfWork,
               natureOfWorkID = model_state.natureOfWorkID,
               sourceOfIncomeOthers = model_state.sourceOfIncomeOthers,
               monthlyIncomeOthers = model_state.monthlyIncomeOthers,
               Employer_Address = model_state.Employer_Address,
               Employer_Province = model_state.Employer_Province,
               Employer_ProvinceID = model_state.Employer_ProvinceID,
               Employer_City = model_state.Employer_City,
               Employer_CityID = model_state.Employer_CityID,
               Employer_Barangay = model_state.Employer_Barangay,
               Employer_BarangayID = model_state.Employer_BarangayID,
               Employer_Country = model_state.Employer_Country,
               Employer_Zipcode = model_state.Employer_Zipcode,
               Employer_Telephone = model_state.Employer_Telephone,
               employee_reference = conn.Query<string>(@"SELECT IntRecord FROM LRWEB_V1_CLIENT_EMPLOYEE WHERE Tokens=@tokens", new { tokens = Session["Token"].ToString() }).Single(),
               dt2 = dt2
           }).Single();

            using (var client = new HttpClient())
            {
                var appno = "0";
                var cifkey = model_state.CIFno;
                var appname = "LRWEB";
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                var authToken = Encoding.ASCII.GetBytes($"{centraluname}:{centralpword}");
                client.BaseAddress = new Uri(centraluri);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

                var content = new MultipartFormDataContent();
                content.Add(new StringContent(cifkey), "mnemonic");
                content.Add(new StringContent(appno), "application_number");
                content.Add(new StringContent(appname), "app_name");
                string address = "save_reserve_cifkey";
                var response = client.PostAsync(address, content).Result;
                var contents = response.Content.ReadAsStringAsync().Result;
                //JObject res = JObject.Parse(contents);
                try
                {
                    string para = "mnemonic: " + cifkey + ", application_number: " + appno + ", app_name: " + appname;
                    var strquery = "INSERT INTO API_LOGS (application_number,api,message,params,Date) values ('0','save_reserve_cifkey','" + contents + "','" + para + "','" + dt2 + "')";
                    conn.Execute(strquery);
                }
                catch (Exception e)
                {

                }
                
                //return Json(contents, JsonRequestBehavior.AllowGet);
            }

            UploadImage(model_state.attachment1, obj.ToString(), "A1");
            var path1 = obj.ToString() + "-A1" + Path.GetExtension(model_state.attachment1.FileName);
            UploadImage(model_state.attachment2, obj.ToString(), "A2");
            var path2 = obj.ToString() + "-A2" + Path.GetExtension(model_state.attachment2.FileName);
            UploadImage(model_state.attachment3, obj.ToString(), "A3");
            var path3 = obj.ToString() + "-A3" + Path.GetExtension(model_state.attachment3.FileName);
            UploadImage(model_state.attachment4, obj.ToString(), "B1");
            var path4 = obj.ToString() + "-B1" + Path.GetExtension(model_state.attachment4.FileName);
            UploadImage(model_state.attachment5, obj.ToString(), "B2");
            var path5 = obj.ToString() + "-B2" + Path.GetExtension(model_state.attachment5.FileName);
            UploadImage(model_state.attachment6, obj.ToString(), "C1");
            var path6 = obj.ToString() + "-C1" + Path.GetExtension(model_state.attachment6.FileName);
            UploadPDF(model_state.attachment7, obj.ToString(), "D1");
            var path7 = obj.ToString() + "-D1.pdf";
            UploadPDF(model_state.attachment8, obj.ToString(), "D2");
            var path8 = obj.ToString() + "-D2.pdf";
            UploadPDF(model_state.attachment9, obj.ToString(), "E1");
            var path9 = obj.ToString() + "-E1.pdf";
            UploadPDF(model_state.attachment10, obj.ToString(), "F1");
            var path10 = obj.ToString() + "-F1.pdf";
            //var path7 = "";
            //var path8 = "";
            conn.Execute("Insert into LRWEB_V1_CUSTOMER_APPLICATION (referenceNumber,CIFno,netIncome,loanAmount,loanTerms,loanPurpose,loanPurposeID,loanPurposeOthers,groupCode,creditOption,creditOptionID,nameToDisplay,accountNumber,bankName,attachment1,attachment2,attachment3,attachment4,attachment5,attachment6,attachment7,attachment8,attachment9,attachment10,legalID,nameOnID,documentName,documentNameID,issueAuth,issueAuthID,issueDate,expirationDate,sol_id,finacle_sol_id,dateCreated,netPay,otherLoans,OTPSend) values " +
                "(@referenceNumber,@CIFno,@netIncome,@loanAmount,@loanTerms,@loanPurpose,@loanPurposeID,@loanPurposeOthers,@groupCode,@creditOption,@creditOptionID,@nameToDisplay,@accountNumber,@bankName,@attachment1,@attachment2,@attachment3,@attachment4,@attachment5,@attachment6,@attachment7,@attachment8,@attachment9,@attachment10,@legalID,@nameOnID,@documentName,@documentNameID,@issueAuth,@issueAuthID,@issueDate,@expirationDate,@sol_id,@finacle_sol_id,@dt2,@netPay,@otherLoans,@OTPSend)", new
                {
                    OTPSend = model_state.OTPSend,
                    referenceNumber = obj.ToString(),
                    CIFno = model_state.CIFno,
                    netIncome = model_state.netIncome,
                    netPay = model_state.netPay,
                    otherLoans = model_state.otherLoans,
                    loanAmount = model_state.loanAmount,
                    loanTerms = model_state.loanTerms,
                    loanPurpose = model_state.loanPurpose,
                    loanPurposeID = model_state.loanPurposeID,
                    loanPurposeOthers = model_state.loanPurposeOthers,
                    groupCode = Session["GroupCode"].ToString(),
                    creditOption = model_state.creditOption,
                    creditOptionID = model_state.creditOptionID,
                    nameToDisplay = model_state.nameToDisplay,
                    accountNumber = model_state.accountNumber,
                    bankName = model_state.bankName,
                    attachment1 = path1,
                    attachment2 = path2,
                    attachment3 = path3,
                    attachment4 = path4,
                    attachment5 = path5,
                    attachment6 = path6,
                    attachment7 = path7,
                    attachment8 = path8,
                    attachment9 = path9,
                    attachment10 = path10,
                    legalID = model_state.legalID,
                    nameOnID = model_state.nameOnID,
                    documentName = model_state.documentName,
                    documentNameID = model_state.documentNameID,
                    issueAuth = model_state.issueAuth,
                    issueAuthID = model_state.issueAuthID,
                    issueDate = model_state.issueDate,
                    expirationDate = model_state.expirationDate,
                    sol_id = conn.Query<string>("select sol_id from LRWEB_V1_CLIENTS where groupCode = @groupCode", new
                    {
                        groupCode = Session["GroupCode"].ToString()
                    }).Single(),
                    finacle_sol_id = conn.Query<string>("select finacle_sol_id from LRWEB_V1_CLIENTS where groupCode = @groupCode", new
                    {
                        groupCode = Session["GroupCode"].ToString()
                    }).Single(),
                    dt2 = dt2
                });
            conn.Execute("UPDATE LRWEB_V1_CLIENT_EMPLOYEE SET SubmitStatus='1' WHERE Tokens=@tokens", new
            {
                tokens = Session["Token"].ToString()
            });

            return obj.ToString();
        }

        public string EditEmployeeData(_EMPLOYEE_DATA model_state)
        {
            _EMPLOYEE_DATA extract_data = new _EMPLOYEE_DATA();

            //Uri MyUrl = Request.Url;
            //string token = MyUrl.Query.ToString().Substring(MyUrl.Query.ToString().LastIndexOf('?') + 1);

            string strquery = @"UPDATE LRWEB_V1_CUSTOMER_INFO SET " +
            "salutation = '" + model_state.salutation + "'," +
            "firstName = '" + model_state.firstName + "'," +
            "middleName = '" + model_state.middleName + "'," +
            "lastName = '" + model_state.lastName + "'," +
            "suffix = '" + model_state.suffix + "'," +
            "birthPlace = '" + model_state.birthPlace + "'," +
            "birthDate = '" + model_state.birthDate + "'," +
            "age = '" + model_state.age + "'," +
            "gender = '" + model_state.gender + "'," +
            "religion = '" + model_state.religion + "'," +
            "citizenship = '" + model_state.citizenship + "'," +
            "civilStatus = '" + model_state.civilStatus + "'," +
            "civilStatusID = '" + model_state.civilStatusID + "'," +
            "TIN = '" + model_state.TIN + "'," +
            "mothersMaidenName = '" + model_state.mothersMaidenName + "'," +
            "SSS = '" + model_state.SSS + "'," +
            "GSIS = '" + model_state.GSIS + "'," +
            "spouseSalutation = '" + model_state.spouseSalutation + "'," +
            "spouseFirstname = '" + model_state.spouseFirstname + "'," +
            "spouseMiddlename = '" + model_state.spouseMiddlename + "'," +
            "spouseLastname = '" + model_state.spouseLastname + "'," +
            "spouseSuffix = '" + model_state.spouseSuffix + "'," +
            "spouseGender = '" + model_state.spouseGender + "'," +
            "spouseBirthDate = '" + model_state.spouseBirthDate + "'," +
            "spouseAge = '" + model_state.spouseAge + "'," +
            "dependents = '" + model_state.dependents + "'," +
            "spouseEmployer = '" + model_state.spouseEmployer + "'," +
            "spouseOccupation = '" + model_state.spouseOccupation + "'," +
            "spouseMonthlyIncome = '" + model_state.spouseMonthlyIncome + "'," +
            "spouseNumber = '" + model_state.spouseNumber + "'," +
            "Present_Address = '" + model_state.Present_Address + "'," +
            "Present_Province = '" + model_state.Present_Province + "'," +
            "Present_ProvinceID = '" + model_state.Present_ProvinceID + "'," +
            "Present_City = '" + model_state.Present_City + "'," +
            "Present_CityID = '" + model_state.Present_CityID + "'," +
            "Present_Barangay = '" + model_state.Present_Barangay + "'," +
            "Present_BarangayID = '" + model_state.Present_BarangayID + "'," +
            "Present_Country = '" + model_state.Present_Country + "'," +
            "Present_Zipcode = '" + model_state.Present_Zipcode + "'," +
            "Present_Ownership = '" + model_state.Present_Ownership + "'," +
            "Present_OwnershipID = '" + model_state.Present_OwnershipID + "'," +
            "Present_Years = '" + model_state.Present_Years + "'," +
            "Present_Months = '" + model_state.Present_Months + "'," +
            "Present_LengthOfStay = '" + model_state.Present_LengthOfStay + "'," +
            "Present_Telephone = '" + model_state.Present_Telephone + "'," +
            "Permanent_Address = '" + model_state.Permanent_Address + "'," +
            "Permanent_Province = '" + model_state.Permanent_Province + "'," +
            "Permanent_ProvinceID = '" + model_state.Permanent_ProvinceID + "'," +
            "Permanent_City = '" + model_state.Permanent_City + "'," +
            "Permanent_CityID = '" + model_state.Permanent_CityID + "'," +
            "Permanent_Barangay = '" + model_state.Permanent_Barangay + "'," +
            "Permanent_BarangayID = '" + model_state.Permanent_BarangayID + "'," +
            "Permanent_Country = '" + model_state.Permanent_Country + "'," +
            "Permanent_Zipcode = '" + model_state.Permanent_Zipcode + "'," +
            "Permanent_Ownership = '" + model_state.Permanent_Ownership + "'," +
            "Permanent_OwnershipID = '" + model_state.Permanent_OwnershipID + "'," +
            "Permanent_Years = '" + model_state.Permanent_Years + "'," +
            "Permanent_Months = '" + model_state.Permanent_Months + "'," +
            "Permanent_LengthOfStay = '" + model_state.Permanent_LengthOfStay + "'," +
            "Permanent_Telephone = '" + model_state.Permanent_Telephone + "'," +
            "dateHired = '" + model_state.dateHired + "'," +
            "tenure = '" + model_state.tenure + "'," +
            "employeeNumber = '" + model_state.employeeNumber + "'," +
            "rank = '" + model_state.rank + "'," +
            "department = '" + model_state.department + "'," +
            "monthlyAllowance = '" + model_state.monthlyAllowance + "'," +
            "occupation = '" + model_state.occupation + "'," +
            "natureOfWork = '" + model_state.natureOfWork + "'," +
            "natureOfWorkID = '" + model_state.natureOfWorkID + "'," +
            "sourceOfIncomeOthers = '" + model_state.sourceOfIncomeOthers + "'," +
            "monthlyIncomeOthers = '" + model_state.monthlyIncomeOthers + "'," +
            "Employer_Address = '" + model_state.Employer_Address + "'," +
            "Employer_Province = '" + model_state.Employer_Province + "'," +
            "Employer_ProvinceID = '" + model_state.Employer_ProvinceID + "'," +
            "Employer_City = '" + model_state.Employer_City + "'," +
            "Employer_CityID = '" + model_state.Employer_CityID + "'," +
            "Employer_Barangay = '" + model_state.Employer_Barangay + "'," +
            "Employer_BarangayID = '" + model_state.Employer_BarangayID + "'," +
            "Employer_Country = '" + model_state.Employer_Country + "'," +
            "Employer_Zipcode = '" + model_state.Employer_Zipcode + "'," +
            "Employer_Telephone = '" + model_state.Employer_Telephone + "'" +
            "where ID = (select referenceNumber from LRWEB_V1_CUSTOMER_APPLICATION where application_number = '" + Session["appno"].ToString() + "'); select referenceNumber from LRWEB_V1_CUSTOMER_APPLICATION where application_number = '" + Session["appno"].ToString() + "'";
            var obj = conn.Query<int>(strquery).Single();



            UploadImage(model_state.attachment1, obj.ToString(), "A1");
            var path1 = obj.ToString() + "-A1" + Path.GetExtension(model_state.attachment1.FileName);
            UploadImage(model_state.attachment2, obj.ToString(), "A2");
            var path2 = obj.ToString() + "-A2" + Path.GetExtension(model_state.attachment2.FileName);
            UploadImage(model_state.attachment3, obj.ToString(), "A3");
            var path3 = obj.ToString() + "-A3" + Path.GetExtension(model_state.attachment3.FileName);
            UploadImage(model_state.attachment4, obj.ToString(), "B1");
            var path4 = obj.ToString() + "-B1" + Path.GetExtension(model_state.attachment4.FileName);
            UploadImage(model_state.attachment5, obj.ToString(), "B2");
            var path5 = obj.ToString() + "-B2" + Path.GetExtension(model_state.attachment5.FileName);
            UploadImage(model_state.attachment6, obj.ToString(), "C1");
            var path6 = obj.ToString() + "-C1" + Path.GetExtension(model_state.attachment6.FileName);
            UploadPDF(model_state.attachment7, obj.ToString(), "D1");
            var path7 = obj.ToString() + "-D1.pdf";
            UploadPDF(model_state.attachment8, obj.ToString(), "D2");
            var path8 = obj.ToString() + "-D2.pdf";
            UploadPDF(model_state.attachment9, obj.ToString(), "E1");
            var path9 = obj.ToString() + "-E1.pdf";
            UploadPDF(model_state.attachment10, obj.ToString(), "F1");
            var path10 = obj.ToString() + "-F1.pdf";
            //var path7 = "";
            //var path8 = "";
            conn.Execute(@"Update LRWEB_V1_CUSTOMER_APPLICATION set 
                    CIFno = @CIFno,
                    netIncome = @netIncome,
                    netPay = @netPay,
                    otherLoans = @otherLoans,
                    OTPSend = @OTPSend,
                    loanAmount = @loanAmount,
                    loanTerms = @loanTerms,
                    loanPurpose = @loanPurpose,
                    loanPurposeID = @loanPurposeID,
                    loanPurposeOthers = @loanPurposeOthers,
                    groupCode = @groupCode,
                    creditOption = @creditOption,
                    creditOptionID = @creditOptionID,
                    nameToDisplay = @nameToDisplay,
                    accountNumber = @accountNumber,
                    bankName = @bankName,
                    attachment1 = @attachment1,
                    attachment2 = @attachment2,
                    attachment3 = @attachment3,
                    attachment4 = @attachment4,
                    attachment5 = @attachment5,
                    attachment6 = @attachment6,
                    attachment7 = @attachment7,
                    attachment8 = @attachment8,
                    attachment9 = @attachment9,
                    legalID = @legalID,
                    nameOnID = @nameOnID,
                    documentName = @documentName,
                    documentNameID = @documentNameID,
                    issueAuth = @issueAuth,
                    issueAuthID = @issueAuthID,
                    issueDate = @issueDate,
                    expirationDate = @expirationDate,
                    status = 'REVERIFY'
                    where referenceNumber = @referenceNumber", new
            {
                referenceNumber = obj.ToString(),
                CIFno = model_state.CIFno,
                netIncome = model_state.netIncome,
                netPay = model_state.netPay,
                otherLoans = model_state.otherLoans,
                OTPSend = model_state.OTPSend,
                loanAmount = model_state.loanAmount,
                loanTerms = model_state.loanTerms,
                loanPurpose = model_state.loanPurpose,
                loanPurposeID = model_state.loanPurposeID,
                loanPurposeOthers = model_state.loanPurposeOthers,
                groupCode = Session["GroupCode"].ToString(),
                creditOption = model_state.creditOption,
                creditOptionID = model_state.creditOptionID,
                nameToDisplay = model_state.nameToDisplay,
                accountNumber = model_state.accountNumber,
                bankName = model_state.bankName,
                attachment1 = path1,
                attachment2 = path2,
                attachment3 = path3,
                attachment4 = path4,
                attachment5 = path5,
                attachment6 = path6,
                attachment7 = path7,
                attachment8 = path8,
                attachment9 = path9,
                legalID = model_state.legalID,
                nameOnID = model_state.nameOnID,
                documentName = model_state.documentName,
                documentNameID = model_state.documentNameID,
                issueAuth = model_state.issueAuth,
                issueAuthID = model_state.issueAuthID,
                issueDate = model_state.issueDate,
                expirationDate = model_state.expirationDate,
                sol_id = conn.Query<string>("select sol_id from LRWEB_V1_CLIENTS where groupCode = @groupCode", new
                {
                    groupCode = Session["GroupCode"].ToString()
                }).Single(),
                finacle_sol_id = conn.Query<string>("select finacle_sol_id from LRWEB_V1_CLIENTS where groupCode = @groupCode", new
                {
                    groupCode = Session["GroupCode"].ToString()
                }).Single()
            });
            conn.Execute("UPDATE LRWEB_V1_CLIENT_EMPLOYEE SET SubmitStatus='1' WHERE Tokens=@tokens", new
            {
                tokens = Session["Token"].ToString()
            });

            return obj.ToString();
        }


        public string GenerateQueryString(_EMPLOYEE_DATA model_state)
        {
            
            var myquerystring = "";
            return myquerystring;
        }


        public void UploadImage(HttpPostedFileBase image, string intrecord, string imgtype)
        {
            HttpPostedFileBase file = image;
            string path = Server.MapPath(ConfigurationManager.ConnectionStrings["ID_IMAGE_STORAGE"].ToString());
            if (image != null)
            {
                WebImage img = new WebImage(image.InputStream);
                if (img.Width >= img.Height)
                {
                    if (img.Width > 800)
                    {
                        double heightsize = img.Width - img.Height;
                        double temp = heightsize / (img.Width / 800);
                        heightsize = 800 - temp;
                        img.Resize(800, Int32.Parse(Math.Round(heightsize).ToString()));
                    }
                }
                else
                {
                    if (img.Height > 800)
                    {
                        double heightsize = img.Height - img.Width;
                        double temp = heightsize / (img.Height / 800);
                        heightsize = 800 - temp;
                        img.Resize(Int32.Parse(Math.Round(heightsize).ToString()), 800);
                    }
                }

                string fileName = Path.GetFileName(image.FileName);
                img.Save(path + intrecord + "-" + imgtype + Path.GetExtension(image.FileName));
            }




        }

        public void UploadPDF(HttpPostedFileBase pdf, string intrecord, string imgtype)
        {
            HttpPostedFileBase file = pdf;
            string path = Server.MapPath(ConfigurationManager.ConnectionStrings["ID_IMAGE_STORAGE"].ToString());
            file.SaveAs(path + intrecord + "-" + imgtype + ".pdf");
        }

        [HttpGet]
        public JsonResult GetProvince()
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
        public JsonResult getIssueAuth()
        {
            using (var client = new HttpClient())
            {
                authtoken = token;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                client.BaseAddress = new Uri(lookup);
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("authtoken", authtoken);
                var response = client.GetAsync("09698665-6aa1-4e06-80bd-ea2227ccb7bc").Result;

                var contents = response.Content.ReadAsStringAsync().Result;
                JObject res = JObject.Parse(contents);
                return Json(res["data"].ToString(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult getOwnership()
        {
            using (var client = new HttpClient())
            {
                authtoken = token;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                client.BaseAddress = new Uri(lookup);
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("authtoken", authtoken);
                var response = client.GetAsync("63789a49-fba2-4ac6-91a3-173b0c2578db").Result;

                var contents = response.Content.ReadAsStringAsync().Result;
                JObject res = JObject.Parse(contents);
                return Json(res["data"].ToString(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult getCivilStatus()
        {
            using (var client = new HttpClient())
            {
                authtoken = token;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                client.BaseAddress = new Uri(lookup);
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("authtoken", authtoken);
                var response = client.GetAsync("c9ce10db-f067-4c87-91df-7b289b2a5e39").Result;

                var contents = response.Content.ReadAsStringAsync().Result;
                JObject res = JObject.Parse(contents);
                return Json(res["data"].ToString(), JsonRequestBehavior.AllowGet);
            }
        }
        
        [HttpGet]
        public JsonResult getSourceOfIncome()
        {
            using (var client = new HttpClient())
            {
                authtoken = token;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                client.BaseAddress = new Uri(lookup);
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("authtoken", authtoken);
                var response = client.GetAsync("cabd75be-19c9-4e65-879f-adc361f57877").Result;

                var contents = response.Content.ReadAsStringAsync().Result;
                JObject res = JObject.Parse(contents);
                return Json(res["data"].ToString(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult getDocumentName()
        {
            using (var client = new HttpClient())
            {
                authtoken = token;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                client.BaseAddress = new Uri(lookup);
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("authtoken", authtoken);
                var response = client.GetAsync("ce922ecb-1ba5-49c8-9b22-3b3ddc671125").Result;

                var contents = response.Content.ReadAsStringAsync().Result;
                JObject res = JObject.Parse(contents);
                return Json(res["data"].ToString(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult getLoanPurpose()
        {
            using (var client = new HttpClient())
            {
                authtoken = token;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                client.BaseAddress = new Uri(lookup);
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("authtoken", authtoken);
                var response = client.GetAsync("d515b249-cedb-4361-b6db-152bde24515c").Result;

                var contents = response.Content.ReadAsStringAsync().Result;
                JObject res = JObject.Parse(contents);
                return Json(res["data"].ToString(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult getNationality()
        {
            using (var client = new HttpClient())
            {
                authtoken = token;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                client.BaseAddress = new Uri(lookup);
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("authtoken", authtoken);
                var response = client.GetAsync("dd567703-588b-4e9d-88bf-7cb354dc42b2").Result;

                var contents = response.Content.ReadAsStringAsync().Result;
                JObject res = JObject.Parse(contents);
                return Json(res["data"].ToString(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult getNatureOfWork()
        {
            using (var client = new HttpClient())
            {
                authtoken = token;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                client.BaseAddress = new Uri(lookup);
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("authtoken", authtoken);
                var response = client.GetAsync("fb3808b9-5adc-4cfd-9adc-42a84fa2e53c").Result;

                var contents = response.Content.ReadAsStringAsync().Result;
                JObject res = JObject.Parse(contents);
                return Json(res["data"].ToString(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult getCreditType()
        {
            using (var client = new HttpClient())
            {
                authtoken = token;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                client.BaseAddress = new Uri(lookup);
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("authtoken", authtoken);
                var response = client.GetAsync("feb13a9e-6e9a-4511-8faa-81af9a144a99").Result;

                var contents = response.Content.ReadAsStringAsync().Result;
                JObject res = JObject.Parse(contents);
                return Json(res["data"].ToString(), JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult getCIF(DateTime birthdate, string lastname, string firstname, string middlename)
        {
            var app_origin = "LR WEB";
            DateTime serverTime = DateTime.Now;
            TimeZoneInfo tst = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
            DateTime dt2 = TimeZoneInfo.ConvertTime(serverTime, TimeZoneInfo.Local, tst);
            var adate = birthdate.ToString("MM/dd/yyyy");
            //var queryString = "call usp_bfk_get_new_cif ('" + adate + "','" + firstname + "','" + middlename + "','" + lastname + "')";
            //var cust_id = AELcon.Query<string>(queryString);
            try
            {
                using (var client = new HttpClient())
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    //ServicePointManager.Expect100Continue = true;
                    //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                    var authToken = Encoding.ASCII.GetBytes($"{centraluname}:{centralpword}");
                    client.BaseAddress = new Uri(centraluri);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(adate), "birth_date");
                    content.Add(new StringContent(firstname), "first_name");
                    content.Add(new StringContent(middlename), "middle_name");
                    content.Add(new StringContent(lastname), "last_name");
                    content.Add(new StringContent(app_origin), "app_origin");
                    string address = "getMnemonic";
                    var response = client.PostAsync(address, content).Result;
                    var contents = response.Content.ReadAsStringAsync().Result;
                    try
                    {
                        string para = "";
                        var strquery = "INSERT INTO API_LOGS (application_number,api,message,params,Date) values ('','getMnemonic','" + contents + "','" + para + "','" + dt2 + "')";
                        conn.Execute(strquery);
                    }
                    catch (Exception e)
                    {

                    }

                    return Json(contents, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                string para = "";
                string error = e.ToString();
                error = error.Replace("'", "");
                error = error.Replace("--->", "");
                error = error.Replace("System.AggregateException:", "");
                error = error.Replace("System.Net.Http.", "");
                error = error.Replace("System.Net", "");
                error = error.Substring(0, Math.Min(300, error.Length));
                var strquery = "INSERT INTO API_LOGS (application_number,api,message,params,Date) values ('','getMnemonic','" + error + "','" + para + "','" + dt2 + "')";
                conn.Execute(strquery);
                //throw;
                return Json(e.ToString(), JsonRequestBehavior.AllowGet);
            }

            //return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getCIF2(DateTime birthdate, string lastname, string firstname, string middlename)
        {
            var app_origin = "LR WEB";
            DateTime serverTime = DateTime.Now;
            TimeZoneInfo tst = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
            DateTime dt2 = TimeZoneInfo.ConvertTime(serverTime, TimeZoneInfo.Local, tst);
            var adate = birthdate.ToString("MM/dd/yyyy");
            //var queryString = "call usp_bfk_get_new_cif ('" + adate + "','" + firstname + "','" + middlename + "','" + lastname + "')";
            //var cust_id = AELcon.Query<string>(queryString);
            try
            {
                using (var client = new HttpClient())
                {
                    //ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    var authToken = Encoding.ASCII.GetBytes($"{centraluname}:{centralpword}");
                    client.BaseAddress = new Uri(centraluri);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(adate), "birth_date");
                    content.Add(new StringContent(firstname), "first_name");
                    content.Add(new StringContent(middlename), "middle_name");
                    content.Add(new StringContent(lastname), "last_name");
                    content.Add(new StringContent(app_origin), "app_origin");
                    string address = "getMnemonic";
                    var response = client.PostAsync(address, content).Result;
                    var contents = response.Content.ReadAsStringAsync().Result;
                    try
                    {
                        string para = "";
                        var strquery = "INSERT INTO API_LOGS (application_number,api,message,params,Date) values ('','getMnemonic','" + contents + "','" + para + "','" + dt2 + "')";
                        conn.Execute(strquery);
                    }
                    catch (Exception e)
                    {

                    }

                    return Json(contents, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                string para = "";
                string error = e.ToString();
                error = error.Replace("'", "");
                error = error.Replace("--->", "");
                error = error.Replace("System.AggregateException:", "");
                error = error.Replace("System.Net.Http.", "");
                error = error.Replace("System.Net", "");
                error = error.Substring(0, Math.Min(300, error.Length));
                var strquery = "INSERT INTO API_LOGS (application_number,api,message,params,Date) values ('','getMnemonic','" + error + "','" + para + "','" + dt2 + "')";
                conn.Execute(strquery);
                //throw;
                return Json(e.ToString(), JsonRequestBehavior.AllowGet);
            }

            //return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getCIF3(DateTime birthdate, string lastname, string firstname, string middlename)
        {
            var app_origin = "LR WEB";
            DateTime serverTime = DateTime.Now;
            TimeZoneInfo tst = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
            DateTime dt2 = TimeZoneInfo.ConvertTime(serverTime, TimeZoneInfo.Local, tst);
            var adate = birthdate.ToString("MM/dd/yyyy");
            //var queryString = "call usp_bfk_get_new_cif ('" + adate + "','" + firstname + "','" + middlename + "','" + lastname + "')";
            //var cust_id = AELcon.Query<string>(queryString);
            try
            {
                using (var client = new HttpClient())
                {
                    ServicePointManager.Expect100Continue = true;
                    //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    var authToken = Encoding.ASCII.GetBytes($"{centraluname}:{centralpword}");
                    client.BaseAddress = new Uri(centraluri);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(adate), "birth_date");
                    content.Add(new StringContent(firstname), "first_name");
                    content.Add(new StringContent(middlename), "middle_name");
                    content.Add(new StringContent(lastname), "last_name");
                    content.Add(new StringContent(app_origin), "app_origin");
                    string address = "getMnemonic";
                    var response = client.PostAsync(address, content).Result;
                    var contents = response.Content.ReadAsStringAsync().Result;
                    try
                    {
                        string para = "";
                        var strquery = "INSERT INTO API_LOGS (application_number,api,message,params,Date) values ('','getMnemonic','" + contents + "','" + para + "','" + dt2 + "')";
                        conn.Execute(strquery);
                    }
                    catch (Exception e)
                    {

                    }

                    return Json(contents, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                string para = "";
                string error = e.ToString();
                error = error.Replace("'", "");
                error = error.Replace("--->", "");
                error = error.Replace("System.AggregateException:", "");
                error = error.Replace("System.Net.Http.", "");
                error = error.Replace("System.Net", "");
                error = error.Substring(0, Math.Min(300, error.Length));
                var strquery = "INSERT INTO API_LOGS (application_number,api,message,params,Date) values ('','getMnemonic','" + error + "','" + para + "','" + dt2 + "')";
                conn.Execute(strquery);
                //throw;
                return Json(e.ToString(), JsonRequestBehavior.AllowGet);
            }

            //return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getCIF4(DateTime birthdate, string lastname, string firstname, string middlename)
        {
            var app_origin = "LR WEB";
            DateTime serverTime = DateTime.Now;
            TimeZoneInfo tst = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
            DateTime dt2 = TimeZoneInfo.ConvertTime(serverTime, TimeZoneInfo.Local, tst);
            var adate = birthdate.ToString("MM/dd/yyyy");
            //var queryString = "call usp_bfk_get_new_cif ('" + adate + "','" + firstname + "','" + middlename + "','" + lastname + "')";
            //var cust_id = AELcon.Query<string>(queryString);
            try
            {
                using (var client = new HttpClient())
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    var authToken = Encoding.ASCII.GetBytes($"{centraluname}:{centralpword}");
                    client.BaseAddress = new Uri(centraluri);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(adate), "birth_date");
                    content.Add(new StringContent(firstname), "first_name");
                    content.Add(new StringContent(middlename), "middle_name");
                    content.Add(new StringContent(lastname), "last_name");
                    content.Add(new StringContent(app_origin), "app_origin");
                    string address = "getMnemonic";
                    var response = client.PostAsync(address, content).Result;
                    var contents = response.Content.ReadAsStringAsync().Result;
                    try
                    {
                        string para = "";
                        var strquery = "INSERT INTO API_LOGS (application_number,api,message,params,Date) values ('','getMnemonic','" + contents + "','" + para + "','" + dt2 + "')";
                        conn.Execute(strquery);
                    }
                    catch (Exception e)
                    {

                    }

                    return Json(contents, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                string para = "";
                string error = e.ToString();
                error = error.Replace("'", "");
                error = error.Replace("--->", "");
                error = error.Replace("System.AggregateException:", "");
                error = error.Replace("System.Net.Http.", "");
                error = error.Replace("System.Net", "");
                error = error.Substring(0, Math.Min(300, error.Length));
                var strquery = "INSERT INTO API_LOGS (application_number,api,message,params,Date) values ('','getMnemonic','" + error + "','" + para + "','" + dt2 + "')";
                conn.Execute(strquery);
                //throw;
                return Json(e.ToString(), JsonRequestBehavior.AllowGet);
            }

            //return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getCIF5(DateTime birthdate, string lastname, string firstname, string middlename)
        {
            var app_origin = "LR WEB";
            DateTime serverTime = DateTime.Now;
            TimeZoneInfo tst = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
            DateTime dt2 = TimeZoneInfo.ConvertTime(serverTime, TimeZoneInfo.Local, tst);
            var adate = birthdate.ToString("MM/dd/yyyy");
            //var queryString = "call usp_bfk_get_new_cif ('" + adate + "','" + firstname + "','" + middlename + "','" + lastname + "')";
            //var cust_id = AELcon.Query<string>(queryString);
            try
            {
                
                using (var client = new HttpClient())
                {
                    //ServicePointManager.Expect100Continue = true;
                    //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                    //System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    var authToken = Encoding.ASCII.GetBytes($"{centraluname}:{centralpword}");
                    client.BaseAddress = new Uri(centraluri);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(adate), "birth_date");
                    content.Add(new StringContent(firstname), "first_name");
                    content.Add(new StringContent(middlename), "middle_name");
                    content.Add(new StringContent(lastname), "last_name");
                    content.Add(new StringContent(app_origin), "app_origin");
                    string address = "getMnemonic";
                    var response = client.PostAsync(address, content).Result;
                    var contents = response.Content.ReadAsStringAsync().Result;
                    try
                    {
                        string para = "";
                        var strquery = "INSERT INTO API_LOGS (application_number,api,message,params,Date) values ('','getMnemonic','" + contents + "','" + para + "','" + dt2 + "')";
                        conn.Execute(strquery);
                    }
                    catch (Exception e)
                    {

                    }

                    return Json(contents, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                string para = "";
                string error = e.ToString();
                error = error.Replace("'", "");
                error = error.Replace("--->", "");
                error = error.Replace("System.AggregateException:", "");
                error = error.Replace("System.Net.Http.", "");
                error = error.Replace("System.Net", "");
                error = error.Substring(0, Math.Min(300, error.Length));
                var strquery = "INSERT INTO API_LOGS (application_number,api,message,params,Date) values ('','getMnemonic','" + error + "','" + para + "','" + dt2 + "')";
                conn.Execute(strquery);
                //throw;
                return Json(e.ToString(), JsonRequestBehavior.AllowGet);
            }

            //return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult requestOTP()
        {
            DateTime serverTime = DateTime.Now;
            TimeZoneInfo tst = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
            DateTime dt2 = TimeZoneInfo.ConvertTime(serverTime, TimeZoneInfo.Local, tst);
            var dt = dt2.AddMinutes(5).ToString("yyyyMMddHHmm");
            string employeeID = Session["EmployeeID"].ToString();
            var otpcode = GenerateStringCode();
            conn.Execute("UPDATE LRWEB_V1_CLIENT_EMPLOYEE SET OTP ='" + otpcode + "', OTPExpiration = '" + dt + "', sent_status = '00'  WHERE IntRecord = '" + employeeID + "'");
            var email = conn.Query<string>(@"select email from LRWEB_V1_CLIENT_EMPLOYEE where IntRecord = "+ employeeID + "", new
            {
                employeeID = employeeID
            }).Single();


            try
            {
                var strquery = "INSERT INTO LRWEB_V1_OTP (OTP,email,application_number,Date) values ('" + otpcode + "','" + email + "','new app','" + dt2 + "')";
                conn.Execute(strquery);
            }
            catch (Exception)
            {


            }

            return Json(new { message = "An OTP has been sent to your company email address." }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult checkOTP(string otp)
        {
            string employeeID = Session["EmployeeID"].ToString();
            var checkOTP = conn.Query<int>(@"SELECT COUNT(*) FROM 
            [LRWEB].[dbo].[LRWEB_V1_CLIENT_EMPLOYEE] WHERE IntRecord=@employeeID and OTP=@OTP", new
            {
                employeeID = employeeID,
                OTP = otp
            }).Single();
            var email = conn.Query<string>(@"select email from LRWEB_V1_CLIENT_EMPLOYEE where IntRecord = " + employeeID + "", new
            {
                employeeID = employeeID
            }).Single();
            if (checkOTP != 0)
            {
                return Json(new { message = "valid", email = email }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { message = "invalid", email = email }, JsonRequestBehavior.AllowGet);
            }
        }

        class auth
        {
            public string username { get; set; }
            public string password { get; set; }
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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LRWEB_V1_CLIENT_SIDE_T24.Areas.LRForms.Models
{
    public class _EMPLOYMENT_DATA
    {

        [MaxLength(20, ErrorMessage = "Maximum of 20 characters allowed")]
        public string SSS { get; set; }

        [MaxLength(20, ErrorMessage = "Maximum of 20 characters allowed")]
        public string GSIS { get; set; }

        [Required(ErrorMessage = "TIN is required")]
        [MinLength(9, ErrorMessage = "Minimum of 9 characters")]
        [MaxLength(12, ErrorMessage = "Maximum of 12 characters")]
        public string TIN { get; set; }

        //[Required(ErrorMessage = "SSS Number is Required")]
        //[MaxLength(100, ErrorMessage = "Maximum of 100 Characters")]
        public string Beneficial_Owners { get; set; }

        [Required(ErrorMessage = "Monthly transaction is required")]
        [MaxLength(50, ErrorMessage = "Maximum of 50 characters")]
        public string Monthly_Transaction { get; set; }

        [Required(ErrorMessage = "Source of funds is required")]
        [MaxLength(50, ErrorMessage = "Maximum of 50 characters")]
        public string Source_Funds { get; set; }

        [Required(ErrorMessage = "Business/Work nature is required")]
        [MaxLength(50, ErrorMessage = "Maximum of 50 characters")]
        public string NatureWorkBusiness { get; set; }

        [Required(ErrorMessage = "Employer/Business name is required")]
        [MaxLength(80, ErrorMessage = "Maximum of 80 characters")]
        public string EmployerBusiness { get; set; }

        [Required(ErrorMessage = "Designation is required")]
        [MaxLength(80, ErrorMessage = "Maximum of 80 characters")]
        public string Designation { get; set; }

        public string BusinessPhoneNo { get; set; }
        public string FaxNo { get; set; }

        [Required(ErrorMessage ="Business address is required")]
        [MaxLength(50,ErrorMessage = "Maximum of 50 character are allowed")]
        public string Business_Address { get; set; }
        [Required(ErrorMessage = "Business country is required")]
        public string Business_Country { get; set; }
        [Required(ErrorMessage = "Business city is required")]
        public string Business_City { get; set; }
        [Required(ErrorMessage = "Business state is required")]
        public string Business_State { get; set; }
        [Required(ErrorMessage = "Business postal code is required")]
        public string Business_ZipCode { get; set; }

        public string OverSeas_Address { get; set; }
        public string OverSeas_City { get; set; }
        public string OverSeas_State { get; set; }
        public string OverSeas_Country { get; set; }
        public string OverSeas_PostalCode { get; set; }
        public string OverSeas_Tax_ID_No { get; set; }

        [Required(ErrorMessage = "Overseas country code is required")]
        public string Overseas_CountryCode { get; set; }

        [Required(ErrorMessage = "Overseas area code is required")]
        public string Overseas_AreaCode { get; set; }

        [Required(ErrorMessage = "Overseas phone number is required")]
        public string Overseas_PhoneNo { get; set; }

        [Required(ErrorMessage = "Passport # is required")]
        [MaxLength(20, ErrorMessage = "Maximum of 20 characters")] 
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and numbers allowed")]
        public string PasspportNo { get; set; }
        public string ExpiryDate { get; set; }
        public string PlaceIssue { get; set; }
        public string OthersID { get; set; }
        public string CUSTID { get; set; }
        public string ACCOUNTNO { get; set; }
        public string GroupCode { get; set; }
        public string ApprovalStatus { get; set; }

        public HttpPostedFileBase imageA1 { get; set; }
        public HttpPostedFileBase imageA2 { get; set; }
        public HttpPostedFileBase imageB1 { get; set; }
        public HttpPostedFileBase imageB2 { get; set; }
        public HttpPostedFileBase imageC1 { get; set; }
        public HttpPostedFileBase imageC2 { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LRWEB_V1_CLIENT_SIDE_T24.Areas.LRForms.Models
{
    public class _EPAY_CLIENTS
    {
        public string ID { get; set; }
        public string AED { get; set; }
        public string clientName { get; set; }
        public string addressLine { get; set; }
        public string barangay { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string barangayId { get; set; }
        public string provinceId { get; set; }
        public string cityId { get; set; }
        public string mobileNumber { get; set; }
        public string officeNumber { get; set; }
        public string emailAddress { get; set; }
        public string classification { get; set; }
        public string structure { get; set; }
        public string accountNumber { get; set; }
        public string customerID { get; set; }
        public string csbBranch { get; set; }
        public string schemeCode { get; set; }
        public string interestRate { get; set; }
        public string sol_id { get; set; }
        public string finacle_sol_id { get; set; }
        public string bankCharge { get; set; }
        public string groupCode { get; set; }
        public string admin_id { get; set; }
        public string emailFormat { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
        public string encode_by { get; set; }
        public string dateCreated { get; set; }
        public string dateUpdated { get; set; }
        public string dateApproved { get; set; }
        public string creditRatio { get; set; }
        //public string ID { get; set; }
        ////public string CompanyID { get; set; }

        //[Required(ErrorMessage = "Company name is required")]
        //[StringLength(50, ErrorMessage = "Max 50 characters")]
        //public string clientName { get; set; }

        //[Required(ErrorMessage = "Company Email is required")]
        //[StringLength(50, ErrorMessage = "Max 50 characters")]
        //[EmailAddress(ErrorMessage = "Invalid email address")]
        //public string emailAddress { get; set; }

        //[Required(ErrorMessage = "Employer ID is required")]
        //[StringLength(50, ErrorMessage = "Max 50 characters")]
        //public string EmployerID { get; set; }

        //[Required(ErrorMessage = "Customer ID is required")]
        //[StringLength(50, ErrorMessage = "Max 50 characters")]
        //public string CustomerID { get; set; }

        //[Required(ErrorMessage = "Account # is required")]
        //[MinLength(12, ErrorMessage = "12 characters")]
        //[MaxLength(12, ErrorMessage = "12 characters")]
        //public string AccountNo { get; set; }

        //[Required(ErrorMessage = "Account type is required")]
        //[StringLength(50, ErrorMessage = "Max 50 characters")]
        //public string AccountType { get; set; }


        //public string MaintainingBranch { get; set; }

        //[Required(ErrorMessage = "Source Funds is Required")]
        //[StringLength(50, ErrorMessage = "Max 50 characters")]
        //public string SourceFunds { get; set; }

        //[Required(ErrorMessage = "Group code is required")]
        //[StringLength(50, ErrorMessage = "Max 50 characters")]
        //public string GroupCode { get; set; }

        //[Required(ErrorMessage = "RM code is required")]
        //[StringLength(50, ErrorMessage = "Max 50 characters")]
        //public string RMCode { get; set; }

        //[Required(ErrorMessage = "Card type is required")]
        //[StringLength(50, ErrorMessage = "Max 50 characters")]
        //public string CardType { get; set; }

        //[Required(ErrorMessage = "Card Delivery Sol Id is required")]
        //[StringLength(50, ErrorMessage = "Max 50 characters")]
        //public string ServicingBranch { get; set; }

        //[Required(ErrorMessage = "Card Design type is required")]
        //[StringLength(50, ErrorMessage = "Max 50 characters")]
        //public string CardDesignType { get; set; }

        //[Required(ErrorMessage = "Picture size is required")]
        //[StringLength(50, ErrorMessage = "Max 50 characters")]
        //public string PictureSize { get; set; }

        //public string ApplicationDate { get; set; }

        //[Required(ErrorMessage = "Site name is required")]
        //[StringLength(50, ErrorMessage = "Max 50 characters")]
        //public string SiteName { get; set; }

        //[Required(ErrorMessage = "No street is required")]
        //[StringLength(100, ErrorMessage = "Max 100 characters")]
        //public string NoStreet { get; set; }

        //[Required(ErrorMessage = "Distrct/Barangay is required")]
        //[StringLength(50, ErrorMessage = "Max 50 characters")]
        //public string DistrictBarangay { get; set; }

        //[Required(ErrorMessage = "City/Province is required")]
        //[StringLength(50, ErrorMessage = "Max 50 characters")]
        //public string CityProvince { get; set; }

        //[Required(ErrorMessage = "Zip code is required")]
        //[StringLength(50, ErrorMessage = "Max 50 characters")]
        //public string ZipCode { get; set; }

        //[Required(ErrorMessage = "Phone # is required")]
        //[MinLength(7, ErrorMessage = "Minimum of 7 Digits")]
        //[MaxLength(11, ErrorMessage = "Maximum of 11 Digits")]
        //public string phonenumber { get; set; }


        //public string status { get; set; }
        //public string remarks { get; set; }
        //public string branches { get; set; }
        //public string encode_by { get; set; }
        //public string admin_id { get; set; }

        //[Required(ErrorMessage = "Business Address is required")]
        //[StringLength(200, ErrorMessage = "Max 200 characters")]
        //public string BusinessAddress { get; set; }


    }
}
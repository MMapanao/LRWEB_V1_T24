using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LRWEB_V1_ADMIN_T24.Areas.Admin.Models
{
    public class _EPAY_V2_CLIENTS
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
        public string sol_id { get; set; }
        public string finacle_sol_id { get; set; }
        public string csbBranch { get; set; }
        public string schemeCode { get; set; }
        public string agencyCode { get; set; }
        public string groupCode { get; set; }
        public string admin_id { get; set; }
        public string emailFormat { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
        public string encode_by { get; set; }
        public string dateCreated { get; set; }
        public string dateUpdated { get; set; }
        public string dateApproved { get; set; }
        public string contentMark { get; set; }
        public string creditRatio { get; set; }
        // Beyond this code is not my naming convention
        //public string CompanyID { get; set; }

        //[Required(ErrorMessage ="Company Name is Required")]
        //[StringLength(50, ErrorMessage = "Maximum 50 characters")]
        //public string CompanyName { get; set; }

        //[Required(ErrorMessage ="Company Email is Required")]
        //[StringLength(50, ErrorMessage = "Maximum of 50 characters")]
        //[EmailAddress(ErrorMessage = "Invalid email address")]
        //public string CompanyEmail { get; set; }

        //[StringLength(50, ErrorMessage = "Max 50 characters")]
        //public string EmployerID { get; set; }


        //[StringLength(50, ErrorMessage = "Max 50 characters")]
        //public string CustomerID { get; set; }


        //[MinLength(12, ErrorMessage = "12 characters")]
        //[MaxLength(12, ErrorMessage = "12 characters")]
        //public string AccountNo { get; set; }


        //public string AccountType { get; set; }

        //public string MaintainingBranch { get; set; }


        //[StringLength(50, ErrorMessage = "Max 50 characters")]
        //public string SourceFunds { get; set; }

        //[Required(ErrorMessage = "Group Code is Required")]
        //[MinLength(3, ErrorMessage = "Minimum of 3 characters")]
        //[MaxLength(5, ErrorMessage = "Maximum of 5 characters")]
        //public string GroupCode { get; set; }


        //public string RMCode { get; set; }

        ////[Required(ErrorMessage = "Card Type is Required")]
        ////[StringLength(50, ErrorMessage = "Max 50 characters")]
        //public string CARD_TYPE { get; set; }


        //public string ServicingBranch { get; set; }

        //[Required(ErrorMessage = "Card Design Type is Required")]
        //[StringLength(50, ErrorMessage = "Max 50 characters")]
        //public string CardDesignType { get; set; }

        //[Required(ErrorMessage = "Picture Size is Required")]
        //[StringLength(50, ErrorMessage = "Max 50 characters")]
        //public string PictureSize { get; set; }

        //public string ApplicationDate { get; set; }


        //[StringLength(50, ErrorMessage = "Max 50 characters")]
        //public string SiteName { get; set; }

        //[Required(ErrorMessage = "No Street is Required")]
        //[StringLength(100, ErrorMessage = "Max 100 characters")]
        //public string NoStreet { get; set; }

        //[Required(ErrorMessage = "Distrct/Barangay is Required")]
        //[StringLength(50, ErrorMessage = "Max 50 characters")]
        //public string DistrictBarangay { get; set; }

        //[Required(ErrorMessage = "City/Province is Required")]
        //[StringLength(50, ErrorMessage = "Max 50 characters")]
        //public string CityProvince { get; set; }

        //[Required(ErrorMessage = "Zip code is Required")]
        //[StringLength(50, ErrorMessage = "Max 50 characters")]
        //public string ZipCode { get; set; }

        //[Required(ErrorMessage = "Phone # is Required")]
        //[RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        //public string phonenumber { get; set; }


        //public string status { get; set; }
        //public string remarks { get; set; }
        //public string branches { get; set; }
        //public string encode_by { get; set; }
        //public string admin_id { get; set; }

        //[Required(ErrorMessage = "Business address is required")]
        //[StringLength(200, ErrorMessage = "Maximum of 200 characters")]
        //public string BusinessAddress { get; set; }
        //public string Province { get; set; }
        //public string City { get; set; }

        //[Required(ErrorMessage = "Branch is Required")]
        //public List<string> CardDeliverySol { get; set; }


        //public string contentMark { get; set; }
    }
}
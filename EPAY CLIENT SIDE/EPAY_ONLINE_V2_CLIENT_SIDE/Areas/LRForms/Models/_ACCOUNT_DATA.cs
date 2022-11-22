using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LRWEB_V1_CLIENT_SIDE_T24.Areas.LRForms.Models
{
    public class _ACCOUNT_DATA
    {
        //----------------------------------------------------

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(50, ErrorMessage = "Maximum of 50 characters")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email_Address { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        public string ContactNumber { get; set; }

        //public string PhoneBrand { get; set; }
        //public string PhoneModel { get; set; }

        [Required(ErrorMessage = "Birth date is required")]
        [MaxLength(10, ErrorMessage = "Maximum of 10 characters")]
        public string BirthDate { get; set; }

        [Required(ErrorMessage = "Birth place is required")]
        [MaxLength(100, ErrorMessage = "Maximum of 100 characters")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Use letters only please")]
        public string BirthPlace { get; set; }

        [Required(ErrorMessage = "Nationality is required")]
        [MaxLength(50, ErrorMessage = "Maximum of 50 characters")]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        [MaxLength(10, ErrorMessage = "Maximum of 10 characters")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Civil staus is required")]
        [MaxLength(15, ErrorMessage = "Maximum of 15 characters")]
        public string Civil_Status { get; set; }

        public string SpouseName { get; set; }

        [Required(ErrorMessage = "Mother's Maiden name is required")]
        [MaxLength(80, ErrorMessage = "Maximum of 80 characters")]
        [RegularExpression(@"^[0-9a-zA-Z ]+$", ErrorMessage = "Special Characters are not Allowed")]
        public string Mother_MaidenName { get; set; }

        [Required(ErrorMessage = "Name on card is required")]
        [MaxLength(80, ErrorMessage = "Maximum of 19 characters allowed")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Use letters only please")]
        public string card_name { get; set; }

        [Required(ErrorMessage = "SMS Alert is required")]
        [MaxLength(3, ErrorMessage = "Maximum of 3 characters allowed")]
        public string SMS_alert { get; set; }

        [Required(ErrorMessage = "Getgo features is required")]
        [MaxLength(3, ErrorMessage = "Maximum of 3 characters allowed")]
        public string getgo_features { get; set; }

        //---------------------------------
    }
}
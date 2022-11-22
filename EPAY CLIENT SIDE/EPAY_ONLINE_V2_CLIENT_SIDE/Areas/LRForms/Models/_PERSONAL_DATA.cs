using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LRWEB_V1_CLIENT_SIDE_T24.Areas.LRForms.Models
{
    public class _PERSONAL_DATA
    {
        public string INTRECORD { get; set; }
        [Required(ErrorMessage = "Salutation is required")]
        [MaxLength(50, ErrorMessage = "Maximum of 50 characters")]
        public string Salutation { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(50, ErrorMessage = "Maximum of 50 characters")]
        [RegularExpression(@"^[0-9a-zA-Z ]+$", ErrorMessage = "Special Characters are not Allowed")]
        public string lastname { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [MaxLength(50, ErrorMessage = "Maximum of 50 characters")]
        [RegularExpression(@"^[0-9a-zA-Z ]+$", ErrorMessage = "Special Characters are not Allowed")]
        public string firstname { get; set; }


        [MaxLength(50, ErrorMessage = "Maximum of 50 characters")]
        [RegularExpression(@"^[0-9a-zA-Z ]+$", ErrorMessage = "Special Characters are not Allowed")]
        public string middlename { get; set; }

        [Required(ErrorMessage = "Present address is required")]
        [MaxLength(200, ErrorMessage = "Maximum of 200 characters")]
        [RegularExpression(@"^[0-9a-zA-Z ]+$", ErrorMessage = "Special Characters are not Allowed")]
        public string Present_Address { get; set; }

        [Required(ErrorMessage = "Present city is required")]
        [MaxLength(50, ErrorMessage = "Maximum of 50 Characters")]
        public string Present_City { get; set; }

        [Required(ErrorMessage = "Present state is required")]
        [MaxLength(50, ErrorMessage = "Maximum of 50 Characters")]
        public string Present_State { get; set; }

        [Required(ErrorMessage = "Zip code is required")]
        [MaxLength(50, ErrorMessage = "Maximum of 50 Characters")]
        public string Present_ZipCode { get; set; }

        [Required(ErrorMessage = "Present country is required")]
        [MaxLength(50, ErrorMessage = "Maximum of 50 Characters")]
        public string Present_Country { get; set; }

        [Required(ErrorMessage = "Permanent address is required")]
        [MaxLength(200, ErrorMessage = "Maximum of 200 Characters")]
        [RegularExpression(@"^[0-9a-zA-Z ]+$", ErrorMessage = "Special Characters are not Allowed")]
        public string Permanent_Address { get; set; }

        [Required(ErrorMessage = "Permanent city is required")]
        [MaxLength(50, ErrorMessage = "Maximum of 50 Characters")]
        public string Permanent_City { get; set; }

        [Required(ErrorMessage = "Permanent state is required")]
        [MaxLength(50, ErrorMessage = "Maximum of 50 Characters")]
        public string Permanent_State { get; set; }

        [Required(ErrorMessage = "Permanent zip code is required")]
        [MaxLength(50, ErrorMessage = "Maximum of 50 Characters")]
        public string Permanent_ZipCode { get; set; }

        [Required(ErrorMessage = "Permanent country is required")]
        [MaxLength(50, ErrorMessage = "Maximum of 50 Characters")]
        public string Permanent_Country { get; set; }
    }
}
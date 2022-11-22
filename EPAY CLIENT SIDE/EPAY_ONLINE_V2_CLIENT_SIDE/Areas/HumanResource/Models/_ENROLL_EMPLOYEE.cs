using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LRWEB_V1_CLIENT_SIDE_T24.Areas.HumanResource.Models
{
    public class _ENROLL_EMPLOYEE
    {
        [Required(ErrorMessage ="Scheme Code is required")]
        public string scheme_code { get; set; }

        [Required(ErrorMessage ="Last name is required")]
        [MaxLength(50,ErrorMessage = "Maximum of 50 characters")]
        public string lastname { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [MaxLength(50, ErrorMessage = "Maximum of 50 characters")]
        public string firstname { get; set; }

        [MaxLength(50, ErrorMessage = "Maximum of 50 characters")]
        public string middlename { get; set; }

        [Required(ErrorMessage = "Contact Number is required")]
        [RegularExpression(@"^\(?([0-9]{5})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid mobile number")]
        public string contactnumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string email_address { get; set; }

        [Required(ErrorMessage = "Branch delivery is required")]
        public string branch_delivery { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LRWEB_V1_ADMIN_T24.Areas.Admin.Models
{
    public class CLIENT_ACCESS
    {
        [Required(ErrorMessage = "Group code is Required")] 
        public string groupCode { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string lastName { get; set; }
 
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string middleName { get; set; }

        [Required(ErrorMessage = "Mobile # is Required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        public string mobileNumber { get; set; }

        [Required(ErrorMessage = "Email address is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string email { get; set; }


    }
}
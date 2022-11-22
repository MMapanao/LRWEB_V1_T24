using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LRWEB_V1_ADMIN_T24.Areas.Admin.Models
{
    public class _SUPER_ADMIN
    {
      public string IntRecord { get; set; }
      [Required(ErrorMessage ="First name is Required")]
      public string firstname { get; set; }

      [Required(ErrorMessage ="Last name is Required")]      
      public string lastname { get; set; }


      public string middlename { get; set; }

        [Required(ErrorMessage = "Mobile # is Required")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        public string contactnumber { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }
        public string password { get; set; }

        public string status { get; set; }

        [Required(ErrorMessage = "Access Level is Required")]
        public string accesslevel { get; set; }


        public string sent_status { get; set; }
 
    }
}
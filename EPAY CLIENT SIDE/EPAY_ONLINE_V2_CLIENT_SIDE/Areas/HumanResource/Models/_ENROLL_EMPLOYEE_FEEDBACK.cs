using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LRWEB_V1_CLIENT_SIDE_T24.Areas.HumanResource.Models
{
    public class _ENROLL_EMPLOYEE_FEEDBACK
    {
        public string scheme_code { get; set; }
        public string lastname { get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
        public string contactnumber { get; set; }
        public string email_address { get; set; }

        public string scheme_code_msg { get; set; }
        public string lastname_msg { get; set; }
        public string firstname_msg { get; set; }
        public string middlename_msg { get; set; }
        public string contactnumber_msg { get; set; }
        public string email_address_msg { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LRWEB_V1_ADMIN_T24.Areas.Admin.Models
{
    public class _Login
    {
        public string IntRecord { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string middlename { get; set; }
        public string contactnumber { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string accesslevel { get; set; }
        public string OTPCode { get; set; }
        public string OTPDateTime { get; set; }
        public string sent_status { get; set; }
        public int attempt { get; set; }
        public string newPassword { get; set; }
        public string passwordUpdated { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        public int daysgone { get; set; }
    }
}
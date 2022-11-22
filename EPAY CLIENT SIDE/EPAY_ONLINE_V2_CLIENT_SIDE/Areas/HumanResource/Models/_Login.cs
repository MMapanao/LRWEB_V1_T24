using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LRWEB_V1_CLIENT_SIDE_T24.Areas.HumanResource.Models
{
    public class _Login
    {
        public string ID { get; set; }
        public string groupCode { get; set; }
        public string clientName { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string contactNumber { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string tempPassword { get; set; }
        public int attempt { get; set; }
        public string sent_status { get; set; }
        public string status { get; set; }
        public string newPassword { get; set; }
        public string dateCreated { get; set; }
        public string dateUpdated { get; set; }
        public string OTPCode { get; set; }
        public string OTPDateTime { get; set; }
    }
}
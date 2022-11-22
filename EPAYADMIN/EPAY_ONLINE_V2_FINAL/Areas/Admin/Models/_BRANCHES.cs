using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LRWEB_V1_ADMIN_T24.Areas.Admin.Models
{
    public class _BRANCHES
    {
        public string IntRecord { get; set; }
        public string CompanyID { get; set; }
        [Required(ErrorMessage = "Site Name is Required")]
        public string SiteName { get; set; }

        [Required(ErrorMessage = "Street is Required")]
        public string Street { get; set; }
        public string District { get; set; }
        public string City { get; set; }

        [Required(ErrorMessage = "Zip Code is Required")]
        public string ZipCode { get; set; }
        public string ServicingCode { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        [Required(ErrorMessage = "Group Code is Required")]
        public string GroupCode { get; set; }
    }
}
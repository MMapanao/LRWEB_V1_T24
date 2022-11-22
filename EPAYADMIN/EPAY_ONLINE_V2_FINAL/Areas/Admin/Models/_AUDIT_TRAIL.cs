using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LRWEB_V1_ADMIN_T24.Areas.Admin.Models
{
    public class _AUDIT_TRAIL
    {
        public string ID { get; set; }
        public string userName { get; set; }
        public string action { get; set; }
        public string dateCreated { get; set; }
    }
}
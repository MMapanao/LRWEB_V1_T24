using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LRWEB_V1_CLIENT_SIDE_T24.Areas.HumanResource.LogCode.LogsModel
{
    public class _LOGS
    {
        public string UserCode { get; set; }
        public string GroupCode { get; set; }
        public string email { get; set; }
        public _ACTIONS Action { get; set; }
    }
}
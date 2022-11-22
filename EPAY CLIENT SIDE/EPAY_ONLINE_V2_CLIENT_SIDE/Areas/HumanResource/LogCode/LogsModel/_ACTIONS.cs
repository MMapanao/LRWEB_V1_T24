using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LRWEB_V1_CLIENT_SIDE_T24.Areas.HumanResource.LogCode.LogsModel
{
    public class _ACTIONS
    {
        public string Event { get; set; }
        public string Module { get; set; }
        public string ID { get; set; }
        public string time { get; set; }
        public string Before { get; set; }
        public string After { get; set; }
        public string DeclineReason { get; set; }
        public List<string> EnrolledID { get; set; }
    }
}
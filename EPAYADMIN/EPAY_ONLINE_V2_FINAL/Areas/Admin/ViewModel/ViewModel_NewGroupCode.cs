
using BusinessObjectEntities.BankAlerts.Entity;
using LRWEB_V1_ADMIN_T24.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LRWEB_V1_ADMIN_T24.Areas.Admin.ViewModel
{
    public class ViewModel_NewGroupCode
    {
        public List<_FIN_PROFIT_CTR> vm_fpc { get; set; }
        public List<_FIN_CUST_GRP_CODE_CMS> vm_groupcode { get; set; }
        public List<_FIN_CUST_GRP_CODE_CMS> vm_CardType { get; set; }
        public List<BankAlerts_Province_Entity> vm_getlocations { get; set; }
        public List<_EPAY_V2_CLIENTS> vm_EPAY_V2_CLIENTS { get; set; }
        public List<_SCHEME_CODE> vm_scheme_code { get; set; }
        public List<_INFO_TABLE> vm_info_table { get; set; }

        public List<_BRANCHES> vm_branches { get; set; }
        public List<_EPAY_V2_CLIENT_ACCESS_ID> vm_access_id { get; set; }
        public List<_EPAY_V2_CLIENTS_CARDDELIVERYSOL> vm_EPAY_V2_CLIENTS_CARDDELIVERYSOL { get; set; }
    }
}
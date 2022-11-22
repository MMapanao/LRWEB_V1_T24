using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LRWEB_V1_CLIENT_SIDE_T24.Areas.LRForms.Models;
using BusinessObjectEntities.BankAlerts.Entity;

namespace LRWEB_V1_CLIENT_SIDE_T24.Areas.LRForms.ViewModel
{
    public class _ViewModel_Form
    {
        public List<_LOCATION> vm_city { get; set;}
        public List<BankAlerts_Province_Entity> vm_loc { get; set; }
        public List<BankAlerts_Country_Entity> vm_country { get; set; }
        public _EPAY_V2_CLIENT_EMPLOYEE vm_EPAY_V2_CLIENT_EMPLOYEE { get; set; }
        public _CLIENT_INFO vm_client_info { get; set; }
        public _EPAY_CLIENTS vm_clients { get; set; }

        public string city { get; set; }
        public string state { get; set; } 

        public List<BankAlerts_NatureofWork_Entity> vm_naturework { get; set; }
        public List<BankAlerts_Occupation_Entity> vm_occupations { get; set; }
        public _EPAY_CLIENTS vm_EPAY_CLIENTS { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LRWEB_V1_CLIENT_SIDE_T24.Areas.LRForms.Models
{
    public class _CITY_PROVICES_LIST
    {
        public string code { get; set; }
        public string name { get; set; }
        public List<_CITY> city_list { get; set; } 
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LRWEB_V1_CLIENT_SIDE_T24.Areas.LRForms.Models
{
    public class _LRWEB_V1_AMORT_PREVIEW
    {
        public string term { get; set; }
        public decimal loan_amt { get; set; }
        public decimal principal { get; set; }
        public decimal interest { get; set; }
        public decimal monthly_amort { get; set; }
        public decimal OS_balance { get; set; }
    }
}
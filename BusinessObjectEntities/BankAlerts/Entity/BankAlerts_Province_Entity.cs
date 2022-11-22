using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectEntities.BankAlerts.Entity
{
    public class BankAlerts_Province_Entity
    {
        public string code { get; set; }
        public string name { get; set; }
        public List<BankAlerts_City_Entity> city_list { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectEntities.TeamBank.Entity
{
    public class TeamBank_CardDeliverySOL_Entity
    {
        public string IntRecord { get; set; }
        public string GroupCode { get; set; }
        public string BranchCode { get; set; }
        public List<TeamBank_FIN_PROFIT_CTR_Entity> List_FIN_PROFIT_CTR_Entity { get; set; }
    }
}

using BankAlertsDAL;
using BusinessObjectEntities.BankAlerts.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAlertsBL.Country
{
    public class CountryBL
    {
        DALMapper dalMapper = new DALMapper();
        public List<BankAlerts_Country_Entity> GET(SqlConnection conn)
        {
            var obj = dalMapper.countryDAL.GET(conn);
            return obj;
        }
    }
}

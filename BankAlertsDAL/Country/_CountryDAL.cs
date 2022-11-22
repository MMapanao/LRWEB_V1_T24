using BusinessObjectEntities.BankAlerts.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAlertsDAL.Country
{
    public class _CountryDAL
    {
        public List<BankAlerts_Country_Entity> GET(SqlConnection conn)
        {
            var obj = conn.Query<BankAlerts_Country_Entity>("Select * from PIS_COUNTRY").ToList();
            return obj;
        }
    }
}

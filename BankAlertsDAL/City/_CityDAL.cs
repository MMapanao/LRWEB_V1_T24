using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using BusinessObjectEntities.BankAlerts.Entity;

namespace BankAlertsDAL.City
{
    public class _CityDAL
    {
        public List<BankAlerts_City_Entity> Get(SqlConnection conn, string province_code)
        {
            var city = conn.Query<BankAlerts_City_Entity>(
                @"SELECT * FROM tblCity WHERE province_code=@code order by code asc", new
            {
                code = province_code.Trim()
            }).ToList();
            return city;
        }
    }
}

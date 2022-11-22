using BankAlertsDAL.City;
using BusinessObjectEntities.BankAlerts.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAlertsDAL.Province
{
    public class _ProvinceDAL
    {
        public List<BankAlerts_Province_Entity> Get(SqlConnection conn)
        {
            var obj_location = conn.Query<BankAlerts_Province_Entity>(@"
                SELECT * FROM tblProvince order by code asc").ToList();

            return obj_location;
        }
    }
}

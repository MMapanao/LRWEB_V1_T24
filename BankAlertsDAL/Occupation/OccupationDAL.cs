using BusinessObjectEntities.BankAlerts.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace BankAlertsDAL.Occupation
{
    public class OccupationDAL
    {
        public List<BankAlerts_Occupation_Entity> GET(MySqlConnection conn)
        {
            //var obj = conn.Query<BankAlerts_Occupation_Entity>(
            //    @"SELECT * FROM tblOccupation order by name ASC").ToList();
            //return obj;

            var obj = conn.Query<BankAlerts_Occupation_Entity>(
                @"call usp_bfk_rct_list()").ToList();
            return obj;

        }
    }
}

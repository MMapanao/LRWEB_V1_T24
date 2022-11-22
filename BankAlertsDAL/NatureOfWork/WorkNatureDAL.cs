using BusinessObjectEntities.BankAlerts.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAlertsDAL.NatureOfWork
{
    public class WorkNatureDAL
    {
        public List<BankAlerts_NatureofWork_Entity> GET(SqlConnection conn)
        {
            var obj_nature = conn.Query<BankAlerts_NatureofWork_Entity>(@"SELECT * FROM tblNatureofWork ORDER BY NatureOfWork ASC").ToList();
            return obj_nature;
        }
    }
}

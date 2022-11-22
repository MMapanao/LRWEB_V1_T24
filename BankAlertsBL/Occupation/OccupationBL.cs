using BankAlertsDAL;
using BusinessObjectEntities.BankAlerts.Entity;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace BankAlertsBL.Occupation
{
    public class OccupationBL
    {
        DALMapper dalMapper = new DALMapper();
        public List<BankAlerts_Occupation_Entity> GET(MySqlConnection conn)
        {
            var obj = dalMapper.occupationDAL.GET(conn);
            return obj;
        }
    }
}

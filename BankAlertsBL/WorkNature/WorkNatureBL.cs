using BankAlertsDAL;
using BusinessObjectEntities.BankAlerts.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAlertsBL.WorkNature
{
    public class WorkNatureBL
    {
        DALMapper dalMapper = new DALMapper();
        public List<BankAlerts_NatureofWork_Entity> GET(SqlConnection conn)
        {
            var obj_nature = dalMapper.workNatureDAL.GET(conn);
            return obj_nature;
        }
    }
}

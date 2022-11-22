using BankAlertsDAL.City;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using BusinessObjectEntities.BankAlerts.Entity;
using BankAlertsDAL;

namespace BankAlertsBL.ProvinceCity
{
    public class ProvinceBL
    {
 
        DALMapper dalMapper = new DALMapper();
        
        public List<BankAlerts_Province_Entity> Get(SqlConnection conn)
        {
            var obj_location = dalMapper.provinceDAL.Get(conn);
            List<BankAlerts_Province_Entity> result_location = new List<BankAlerts_Province_Entity>();
            if (obj_location != null)
            {
                foreach (var row in obj_location)
                {
                    BankAlerts_Province_Entity loc = new BankAlerts_Province_Entity();
                    loc.code = row.code.Trim();
                    loc.name = row.name;
                    loc.city_list = dalMapper.cityDAL.Get(conn, row.code.Trim());
                    result_location.Add(loc);
                }
            }
            return result_location;
        }


    }
}

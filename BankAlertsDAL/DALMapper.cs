using BankAlertsDAL.City;
using BankAlertsDAL.Country;
using BankAlertsDAL.NatureOfWork;
using BankAlertsDAL.Occupation;
using BankAlertsDAL.Province;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAlertsDAL
{
    public class DALMapper
    {
       public _CityDAL cityDAL = new _CityDAL();
       public _ProvinceDAL provinceDAL = new _ProvinceDAL();
        public _CountryDAL countryDAL = new _CountryDAL();
        public WorkNatureDAL workNatureDAL = new WorkNatureDAL();
        public OccupationDAL occupationDAL = new OccupationDAL();
    }
}

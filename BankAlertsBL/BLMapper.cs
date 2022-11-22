using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAlertsBL;
using BankAlertsBL.ProvinceCity;
using BankAlertsBL.Country;
using BankAlertsBL.WorkNature;
using BankAlertsBL.Occupation;

namespace BankAlertsBL
{
    public class BLMapper
    {
        public ProvinceBL provinceBL = new ProvinceBL();
        public CountryBL countryBL = new CountryBL();
        public WorkNatureBL workNatureBL = new WorkNatureBL();
        public OccupationBL occupationBL = new OccupationBL();
    }
}

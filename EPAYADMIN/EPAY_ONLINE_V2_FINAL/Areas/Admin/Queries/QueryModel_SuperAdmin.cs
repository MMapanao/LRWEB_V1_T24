using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LRWEB_V1_ADMIN_T24.Areas.Admin.Queries
{
    public class QueryModel_SuperAdmin
    {
        public static string GetAllAdmin()
        {
            string myquery = @"SELECT   *
                              FROM [LRWEB].[dbo].[LRWEB_v1_ADMIN_CRED] WHERE status<>'DELETED' and IntRecord<>'1000000'";

            return myquery;
        }
    }
}
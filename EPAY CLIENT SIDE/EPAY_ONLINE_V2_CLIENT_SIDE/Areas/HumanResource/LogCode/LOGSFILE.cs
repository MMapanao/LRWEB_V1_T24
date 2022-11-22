using LRWEB_V1_CLIENT_SIDE_T24.Areas.HumanResource.LogCode.LogsModel;
 
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
 

namespace LRWEB_V1_CLIENT_SIDE_T24.Areas.HumanResource.LogCode
{
    public class LOGSFILE
    {
        ENCRYPTIONS enc = new ENCRYPTIONS();
        
 

        public void CheckLogFiles()
        {
            string logpathFolder = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["LogFilePath"].ToString() + DateTime.Now.ToString("yyyy-MM-dd") + ".json");
            if (!File.Exists(logpathFolder))
            {
                File.Create(logpathFolder);
                
            }
        }

        public void ActionLog(_LOGS logs)
        {
            var logpathFolder = (HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["LogFilePath"].ToString() + DateTime.Now.ToString("yyyy-MM-dd") + ".json"));
            if (!File.Exists(logpathFolder))
            {
   
				
				//File.WriteAllText(logpathFolder,"[" + JsonConvert.SerializeObject(logs));
				using (StreamWriter outputFile = new StreamWriter(logpathFolder,true))
				{
					outputFile.WriteLine("[" + JsonConvert.SerializeObject(logs));
				}
			}
			else
            {
                File.AppendAllText(logpathFolder, "," + Environment.NewLine +JsonConvert.SerializeObject(logs));
            }
        }
        
 
        
    }
}
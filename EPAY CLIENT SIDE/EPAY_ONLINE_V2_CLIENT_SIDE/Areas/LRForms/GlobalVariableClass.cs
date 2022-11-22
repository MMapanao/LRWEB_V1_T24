using Dapper;
using LRWEB_V1_CLIENT_SIDE_T24.Areas.HumanResource.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace LRWEB_V1_CLIENT_SIDE_T24.Areas.LRForms
{
    public class GlobalVariableClass
    {
        public static string FolderName = ConfigurationManager.ConnectionStrings["FolderName"].ToString();
        public static string PDFGetHostName()
        {
            return ConfigurationManager.ConnectionStrings["PDFHost"].ToString();
        }

        public static string IFNULLresponseEmpty(string obj)
        {
            if(obj == null)
            {
                obj = "";
            }
            return obj;
        }
        public static string DownloadMyPDFForm()
        {
            return ConfigurationManager.ConnectionStrings["DownloadPDF"].ToString();
        }

        public static bool Authenticated(string myticketsession, string mycredsession)
        {
            bool result = true;
            string[] arrcred = Base64Decode(Base64Decode(mycredsession)).Split(',');
            //intrecord,groupcode,lastname,firstname,middlename,contactno,email,checker,maker,companyname
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TMBANKconnection"].ToString());
            DateTime dateTime = DateTime.UtcNow.Date;
            var obj = conn.Query<_Login>("Select * from EPAY_V2_AUTH where EmployeeEmail=@email and token=@token and timeexpiration >= @timenow and timestamp <= @timenow", new
            {
                email = arrcred[6],
                token = myticketsession,
                timenow = DateTime.Now.ToString("yyyyMMddHHmmss")
            }).ToList();
            int x = 0;
            if (obj != null)
            {

                foreach (var row in obj)
                {
                    x++;
                }
            }
            if (x == 0)
            {

                result = false;
            }
            return result;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        //string to Base64string to ENCRYPT TO MD5
        public static string EncryptBCrypt(string key)
        {
            key = Base64Encode(key);
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(key));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("Un10nb@nKEp@y0nL1nEVeRs10nTw0P0inTZ3r0#OwnTheFuture"));
            }
            key = strBuilder.ToString();
            return key;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LRWEB_V1_ADMIN_T24.Areas.Admin.Queries
{
    public class QueryModel_EnrollClients
    {
        public static string _EPAY_V2_CLIENTS_QUERY(string query_params)
        {
            string myquery = @"SELECT TOP 100 * FROM LRWEB_V1_CLIENTS 
                WHERE status <> 'DELETE' " + query_params;
            return myquery;
        }

        public static string _EPAY_V2_CLIENT_INSERT()
        {
            string myquery = @"INSERT INTO LRWEB_V1_CLIENTS(
            AED,
            clientName,
            addressLine,
            barangay,
            province,
            city,
            barangayId,
            provinceId,
            cityId,
            mobileNumber,
            officeNumber,
            emailAddress,
            classification,
            structure,
            accountNumber,
            customerID,
            sol_id,
            finacle_sol_id,
            csbBranch,
            schemeCode,
            agencyCode,
            creditRatio,
            groupCode,
            status,
            admin_id,
            emailFormat,
            dateCreated
            )
            values
            (@AED,
            @clientName,
            @addressLine,
            @barangay,
            @province,
            @city,
            @barangayId,
            @provinceId,
            @cityId,
            @mobileNumber,
            @officeNumber,
            @emailAddress,
            @classification,
            @structure,
            @accountNumber,
            @customerID,
            @sol_id,
            @finacle_sol_id,
            @csbBranch,
            @schemeCode,
            @agencyCode,
            @creditRatio,
            @groupCode,
            'PENDING',
            @admin_id,
            @emailFormat,
            getdate());
            SELECT CAST(SCOPE_IDENTITY() as int)";

            return myquery;
        }

        public static string _EPAY_V2_CLIENT_UPDATE()
        {
            string myquery = @"UPDATE LRWEB_V1_CLIENTS SET 
            AED=@AED,
            clientName=@clientName,
            addressLine=@addressLine,
            barangay=@barangay,
            province=@province,
            city=@city,
            barangayId=@barangayId,
            provinceId=@provinceId,
            cityId=@cityId,
            mobileNumber=@mobileNumber,
            officeNumber=@officeNumber,
            emailAddress=@emailAddress,
            classification=@classification,
            structure=@structure,
            accountNumber=@accountNumber,
            customerID=@customerID,
            csbBranch=@csbBranch,
            sol_id=@sol_id,
            finacle_sol_id=@finacle_sol_id,
            schemeCode=@schemeCode,
            creditRatio=@creditRatio,
            groupCode=@groupCode,
            dateUpdated=getdate(),
            emailFormat=@emailFormat,
            status='PENDING'
            WHERE groupCode=@GroupCode";
            return myquery;
        }

    }
}
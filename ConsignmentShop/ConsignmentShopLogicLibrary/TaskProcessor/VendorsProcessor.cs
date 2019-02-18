using ConsignmentShopLibrary;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsignmentShopLogicLibrary.DataAccess.DataAccess;
namespace ConsignmentShopLogicLibrary.TaskProcessor
{
    public class VendorsProcessor
    {
        #region Default Connectionstring name
        /// <summary>
        /// Default ConnectionString name
        /// </summary>
        const string connName = "LocalDB"; 
        #endregion

        public static int InsertVendor(IVendor vendor,string connectionName=connName)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@FirstName", vendor.FirstName);
            param.Add("@LastName", vendor.LastName);
            param.Add("@Comission", vendor.Comission);
            return  CmdExecute("uspInsertVendor", param,connectionName);
        }

        public static int UpdatingVendor(IVendor vendor, string connectionName = connName)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@VendorId", vendor.VendorId);
            param.Add("@FirstName", vendor.FirstName);
            param.Add("@LastName", vendor.LastName);
            param.Add("@Comission", vendor.Comission);
            return CmdExecute("uspUpdateVendor", param, connectionName);
        }

        public static int DeletingVendor(IVendor vendor, string connectionName = connName)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@VendorId", vendor.VendorId);
    
            return CmdExecute("uspDeleteVendor", param, connectionName);
        }


        public static IEnumerable<T> GetVendors<T>(IVendor vendor=null,string connectionName= connName) where T: IVendor
        {
            DynamicParameters param = new DynamicParameters();
            
                if(!string.IsNullOrWhiteSpace(vendor?.FirstName))
                {
                    param.Add("@FirstName", vendor.FirstName);
                }
                if(!string.IsNullOrWhiteSpace(vendor?.LastName))
                {
                    param.Add("@LastName", vendor.LastName);
                }

                if (vendor?.Comission>0)
                {
                    param.Add("@Comission", vendor.Comission);
                }
       
            return  GetList<T>("uspSelectAllVendor",connectionName,param);
        }

    }
}

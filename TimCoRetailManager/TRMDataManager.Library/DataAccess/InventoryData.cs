using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDataManager.Library.Internal.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Library.DataAccess
{
   public class InventoryData
    {
       public async Task<List<InventoryModel>> GetInventory()
        {
            using (SqlDataAccess sql=new SqlDataAccess())
            {
                var output = await sql.LoadData<InventoryModel, dynamic>("dbo.spInventory_GetAll",
                    new { }, "TRMData")
                    .ConfigureAwait(false);
                return output;
            }
        } 

        public async Task SaveInventoryRecord(InventoryModel item)
        {
            using (SqlDataAccess sql =new SqlDataAccess())
            {
                await sql.SaveData("dbo.spInventory_Insert", item, "TRMData")
                    .ConfigureAwait(false);
            }
        }
    }
}

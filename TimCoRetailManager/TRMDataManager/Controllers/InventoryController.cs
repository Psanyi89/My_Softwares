using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TRMDataManager.Library.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Controllers
{
    [Authorize]
    public class InventoryController : ApiController
    {
        [Authorize(Roles = "Admin,Manager")]
        public async Task<List<InventoryModel>> Get()
        {
            InventoryData data = new InventoryData();
            return await data.GetInventory().ConfigureAwait(false);
        }

        [Authorize(Roles = "Admin")]
        public async Task Post(InventoryModel item)
        {
            InventoryData data = new InventoryData();
             await data.SaveInventoryRecord(item).ConfigureAwait(false);
        }
    }
}

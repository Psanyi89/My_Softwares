using Microsoft.AspNet.Identity;
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
    public class SaleController : ApiController
    {
        [Authorize(Roles = "Cashier")]
        public async Task Post(SaleModel sale)
        {
            SaleData data = new SaleData();
            string userId = RequestContext.Principal.Identity.GetUserId();
            await data.SaveSale(sale, userId);
        }
        [Authorize(Roles = "Admin,Manager")]
        [Route("GetSalesReport")]
        public async Task<List<SaleReportModel>> GetSalesReport()
        {
            if (RequestContext.Principal.IsInRole("Admin"))
            {

            }
            else if(RequestContext.Principal.IsInRole("Manager"))
            {

            }
            else
            {

            }
            SaleData data = new SaleData();
            return await data.GetSaleReport().ConfigureAwait(false);
        }
     
    }
}

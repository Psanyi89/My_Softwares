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
    public class ProductController : ApiController
    {
        public async Task<List<ProductModel>> Get()
        {
            ProductData data = new ProductData();

            return await data.GetProducts().ConfigureAwait(false) ;
        }
    }
}

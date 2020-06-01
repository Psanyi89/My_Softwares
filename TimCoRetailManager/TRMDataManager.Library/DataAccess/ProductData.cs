using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDataManager.Library.Internal.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Library.DataAccess
{
    public class ProductData
    {
        public async Task<List<ProductModel>> GetProducts()
        {
            SqlDataAccess sql = new SqlDataAccess();
       
            var output =await sql.LoadData<ProductModel, dynamic>
                ("dbo.spProduct_GetAll", new { }, "TRMData")
                .ConfigureAwait(false);
            return output;
        }

        public async Task<ProductModel>  GetProductById(int productId)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output = await sql.LoadData<ProductModel, dynamic>
                ("dbo.spProduct_GetById", new { Id= productId }, "TRMData")
                .ConfigureAwait(false);
            return output.FirstOrDefault();
        }
    }
}

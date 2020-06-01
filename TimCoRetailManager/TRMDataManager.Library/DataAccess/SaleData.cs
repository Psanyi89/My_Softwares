using Squirrel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDataManager.Library.Internal.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Library.DataAccess
{
    public class SaleData
    {
        public async Task SaveSale(SaleModel saleInfo, string cashierId)
        {
            // TODO: Make it Solid/DRY/Better
            // Start filling in the models we will save to the database
            // Fill in the available info
           
            List<SaleDetailDBModel> saleDetails = new List<SaleDetailDBModel>();
                ProductData products = new ProductData();
            var taxRate = ConfigHelper.GetTaxRate()/100;
            foreach (var item in saleInfo.SaleDetails)
            {
               var detail = new SaleDetailDBModel
                {
                    ProductId=item.ProductId,
                    Quantity=item.Quantity
                };

                // Get the information about this product
                var productInfo = await products.GetProductById(detail.ProductId);
                if (productInfo==null)
                {
                    throw new Exception($"the product id of {detail.ProductId} couldn't found in the database.");
                }
                detail.PurchasePrice = (productInfo.RetailPrice * detail.Quantity);
                if (productInfo.IsTaxable)
                {
                    detail.Tax = (detail.PurchasePrice * taxRate);
                }

                saleDetails.Add(detail);

            }
            // Create the sale model
            SaleDBModel sale = new SaleDBModel
            {
              SubTotal=saleDetails.Sum(x=>x.PurchasePrice),
              Tax=saleDetails.Sum(x=>x.Tax),             
              CashierId=cashierId
            };
            sale.Total = sale.SubTotal + sale.Tax;

            // Save the sale model
            SqlDataAccess sql = new SqlDataAccess();

           await sql.SaveData("dbo.spSale_Insert", sale, "TRMData").ConfigureAwait(false);

            // Get the ID from the sale model
          sale.Id=  (await sql.LoadData<int,dynamic>("dbo.spSale_Lookup",
                new {CashierId=sale.CashierId, SaleDate=sale.SaleDate },
                "TRMData").ConfigureAwait(false)).FirstOrDefault();
            // Finish filling in the sale detail models

            foreach (var item in saleDetails)
            {
                item.SaleId =sale.Id;
                // Save the sale detail models
                await sql.SaveData("dbo.spSaleDetail_Insert",item,"TRMData");
            }

        }
        //public async Task<List<ProductModel>> GetProducts()
        //{
        //    SqlDataAccess sql = new SqlDataAccess();

        //    var output = await sql.LoadData<ProductModel, dynamic>
        //        ("dbo.spProduct_GetAll", new { }, "TRMData")
        //        .ConfigureAwait(false);
        //    return output;
        //}
    }
}

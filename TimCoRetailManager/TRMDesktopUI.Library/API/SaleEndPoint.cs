using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TRMDesktopUI.Library.Model;

namespace TRMDesktopUI.Library.API
{
    public class SaleEndPoint : ISaleEndPoint
    {
        private IAPIHelper _aPIHelper;
        public SaleEndPoint(IAPIHelper aPIHelper)
        {
            _aPIHelper = aPIHelper;
        }

        public async Task PostSaleAsync(SaleModel sale)
        {
            using (HttpResponseMessage response = await _aPIHelper.ApiClient.PostAsJsonAsync("/api/sale", sale))
            {
                if (response.IsSuccessStatusCode)
                {
                    // TODO: Log Successfull call?
                  
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        //public async Task<List<ProductModel>> GetAll()
        //{
        //    using (HttpResponseMessage response = await _aPIHelper.ApiClient.GetAsync("/api/product"))
        //    {
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var result = await response.Content.ReadAsAsync<List<ProductModel>>()
        //                .ConfigureAwait(false);
        //            return result;
        //        }
        //        else
        //        {
        //            throw new Exception(response.ReasonPhrase);
        //        }
        //    }
        //}
    }
}

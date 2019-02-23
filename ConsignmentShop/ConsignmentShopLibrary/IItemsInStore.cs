using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsignmentShopLibrary
{
    public interface IItemsInStore
    {
         int StoreId { get; set; }
         int ItemId { get; set; }
    }
}

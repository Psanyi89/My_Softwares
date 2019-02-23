using ConsignmentShopLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsignmentShopUI.Models
{
    public class ItemsInStore : IItemsInStore
    {
        public int StoreId { get; set ; }
        public int ItemId { get; set ; }
    }
}

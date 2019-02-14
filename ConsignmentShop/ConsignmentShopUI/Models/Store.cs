using ConsignmentShopLibrary;
using System.Collections.Generic;

namespace ConsignmentShopUI.Models
{
    public class Store : IStore
    {
        public int StoreId { get; set; }
        public string Name { get; set; }
        public List<Vendor> Vendors { get; set; }
        public List<Item> Items { get; set; }

        public Store()
        {
            Vendors = new List<Vendor>();
            Items = new List<Item>();
        }

    }
}

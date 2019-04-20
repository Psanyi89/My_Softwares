using ConsignmentShopLibrary;

namespace ConsigmentShopReactiveUI.Models
{
    public class ItemsInStore : IItemsInStore
    {
        public int StoreId { get; set; }
        public int ItemId { get; set; }
    }
}

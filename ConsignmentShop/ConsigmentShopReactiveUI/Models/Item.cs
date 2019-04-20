using ConsignmentShopLibrary;

namespace ConsigmentShopReactiveUI.Models
{
    public class Item :IItem
    {
        public int ItemId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public bool? Sold { get; set; }
        public bool? PaymentDistributed { get; set; }
        public int? Owner { get; set; }

    }
}

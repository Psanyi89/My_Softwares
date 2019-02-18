using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsignmentShopLibrary
{
  public  interface IItem
    {
        int ItemId { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        decimal Price { get; set; }
        bool Sold { get; set; }
        bool PaymentDistributed { get; set; }
        int Owner { get; set; }
    }
}

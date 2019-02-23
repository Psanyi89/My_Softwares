using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsignmentShopLibrary
{
  public interface IVendor
    {
        int VendorId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        int? Comission { get; set; }
    }
}

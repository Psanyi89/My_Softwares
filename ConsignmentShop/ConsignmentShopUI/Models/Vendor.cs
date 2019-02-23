using ConsignmentShopLibrary;

namespace ConsignmentShopUI.Models
{
    public class Vendor : IVendor
    {
        public int VendorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Comission { get; set; }

        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }
    }
}

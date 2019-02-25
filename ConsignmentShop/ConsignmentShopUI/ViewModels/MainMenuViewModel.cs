using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsignmentShopUI.ViewModels
{
   public class MainMenuViewModel :Conductor<object>.Collection.OneActive
    {
        public MainMenuViewModel()
        {
            ActivateItem(new ShopViewModel());
        }
        public void AddItem()
        {
            ActivateItem(new AddNewItemViewModel());
        }
        public void AddVendor()
        {
            ActivateItem(new AddVendorsViewModel());
        }
        public void AddStore()
        {
            ActivateItem(new AddItemToStoreViewModel());
        }
        public void OpenShop()
        {
            ActivateItem(new ShopViewModel());
        }
    }
}

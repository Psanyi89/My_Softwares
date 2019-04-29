using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDesktopUI.Library.API;
using TRMDesktopUI.Library.Model;

namespace TRMWPFDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        private IProductEndpoint _productEndpoint;
        public SalesViewModel(IProductEndpoint productEndpoint)
        {
            _productEndpoint = productEndpoint; 
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            var producList = await _productEndpoint.GetAll();
            Products = new BindingList<ProductModel>(producList);
        }

        private BindingList<ProductModel> _products;
        private BindingList<ProductModel> _cart;
        private int itemQuantity;

        public BindingList<ProductModel> Products
        {
            get { return _products; }
            set { _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        public BindingList<ProductModel> Cart
        {
            get { return _cart; }
            set { _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        public string SubTotal
        {
            // TODO - Replace with calculation
            get { return "$0.00"; }
          
        }

        public string Tax
        {
            // TODO - Replace with calculation
            get { return "$0.00"; }

        }

        public string Total
        {
            // TODO - Replace with calculation
            get { return "$0.00"; }

        }

        public int ItemQuantity
        {
            get { return itemQuantity; }
            set { itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
            }
        }

        public bool CanAddToCart
        {
            // Make sure something is selected
            // Make sure there is an item quantity
            get { return false; }
        }
        public void AddToCart()
        {

        }

        public bool CanRemoveFromCart
        {
            // Make sure something is selected
            // Make sure there is an item quantity
            get { return false; }
        }

        public void RemoveFromCart()
        {

        }

        public bool CanCheckOut
        {
            // Make sure something is in the cart
        
            get { return false; }
        }

        public void CheckOut()
        {

        }

    }
}

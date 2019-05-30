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
        private BindingList<CartItemModel> _cart=new BindingList<CartItemModel>();
        private int itemQuantity=1;

        public BindingList<ProductModel> Products
        {
            get { return _products; }
            set { _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        private ProductModel _selectedProduct;

        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value;
                NotifyOfPropertyChange(()=>SelectedProduct);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        public BindingList<CartItemModel> Cart
        {
            get { return _cart; }
            set { _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        public string SubTotal
        {
            get { 
                        decimal subTotal = 0;
                    foreach (var item in Cart)
	                {
                    subTotal += item.Product.RetailPrice * item.QuantityInCart;
	                }

                        return subTotal.ToString("C"); 
            }               
          
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
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        public bool CanAddToCart
        {
            // Make sure something is selected
            // Make sure there is an item quantity
            get { return ItemQuantity>0 && SelectedProduct?.QuantityInStock>=ItemQuantity; }
        }
        public void AddToCart()
        {
            CartItemModel existingItem = Cart.FirstOrDefault(x => x.Product == SelectedProduct);
            if (existingItem!=null)
            {
                existingItem.QuantityInCart += ItemQuantity;
               //// HACK - There should be a better way of refreshing the cart display
                //Cart.Remove(existingItem);
                //Cart.Add(existingItem);

            }
            else
            {

            CartItemModel cartItem = new CartItemModel
            {
                Product=SelectedProduct,
                QuantityInCart=ItemQuantity
            };
            Cart.Add(cartItem);
            }
            SelectedProduct.QuantityInStock -= ItemQuantity;
            ItemQuantity = 1;
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Cart);
            NotifyOfPropertyChange(() => existingItem);
        }

        public bool CanRemoveFromCart
        {
            // Make sure something is selected
            // Make sure there is an item quantity
            get { return false; }
        }

        public void RemoveFromCart()
        {
            NotifyOfPropertyChange(() => SubTotal);
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

using AutoMapper;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using TRMDesktopUI.Library.API;
using TRMDesktopUI.Library.Helpers;
using TRMDesktopUI.Library.Model;
using TRMWPFDesktopUI.Models;

namespace TRMWPFDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        private IProductEndpoint _productEndpoint;
        private IConfigHelper _configHelper;
        private readonly ISaleEndPoint _saleEndPoint;
        private readonly IMapper _mapper;

        public SalesViewModel(IProductEndpoint productEndpoint, IConfigHelper configHelper,
            ISaleEndPoint saleEndPoint,IMapper mapper)
        {
            _productEndpoint = productEndpoint;
            _configHelper = configHelper;
            _saleEndPoint = saleEndPoint;
            _mapper = mapper;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            var producList = await _productEndpoint.GetAll();
            var products = _mapper.Map<List<ProductDisplayModel>>(producList);
            Products = new BindingList<ProductDisplayModel>(products);
        }

        private BindingList<ProductDisplayModel> _products;
        private BindingList<CartItemDisplayModel> _cart=new BindingList<CartItemDisplayModel>();
        private int itemQuantity=1;

        public BindingList<ProductDisplayModel> Products
        {
            get { return _products; }
            set { _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        private ProductDisplayModel _selectedProduct;

        public ProductDisplayModel SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value;
                NotifyOfPropertyChange(()=>SelectedProduct);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        private async Task ResetSalesViewModel()
        {
            Cart = new BindingList<CartItemDisplayModel>();
            // TODO Add clearing the selectedCareItem if it does not do it itself
            await LoadProducts();
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanCheckOut);
        }

        private CartItemDisplayModel _selectedCartItem;

        public CartItemDisplayModel SelectedCartItem
        {
            get { return _selectedCartItem; }
            set 
            { 
                _selectedCartItem = value;
                NotifyOfPropertyChange(() => SelectedCartItem);
                NotifyOfPropertyChange(() => CanRemoveFromCart);
            }
        }


        public BindingList<CartItemDisplayModel> Cart
        {
            get { return _cart; }
            set { _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        public string SubTotal
        {
            get
            { 
             return CalculateSubTotal().ToString("C"); 
            }               
          
        }
        private decimal CalculateSubTotal()
        {
            decimal subTotal = 0;
            foreach (var item in Cart)
            {
                subTotal += item.Product.RetailPrice * item.QuantityInCart;
            }
            return subTotal;
        }
        private decimal CalculateTax()
        {
            decimal taxAmount = 0;
            decimal taxRate = _configHelper.GetTaxRate()/100;

           taxAmount= Cart
                .Where(x => x.Product.IsTaxable)
                .Sum(x => x.Product.RetailPrice * x.QuantityInCart * taxRate);
            //foreach (var item in Cart)
            //{
            //    if (item.Product.IsTaxable)
            //    {

            //        taxAmount += (item.Product.RetailPrice * item.QuantityInCart * taxRate);
            //    }

            //}
            return taxAmount;
        }
        public string Tax
        {
            // TODO - Replace with calculation
            get {

              return CalculateTax().ToString("C");
            }

        }

        public string Total
        {
         
            get
            {
                return (CalculateTax() + CalculateSubTotal()).ToString();
            }

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
            CartItemDisplayModel existingItem = Cart.FirstOrDefault(x => x.Product == SelectedProduct);
            if (existingItem!=null)
            {
                existingItem.QuantityInCart += ItemQuantity;
            }
            else
            {

            CartItemDisplayModel cartItem = new CartItemDisplayModel
            {
                Product=SelectedProduct,
                QuantityInCart=ItemQuantity
            };
            Cart.Add(cartItem);
            }
            SelectedProduct.QuantityInStock -= ItemQuantity;
            ItemQuantity = 1;
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => Cart);
            NotifyOfPropertyChange(() => existingItem);
            NotifyOfPropertyChange(() => CanCheckOut);
        }

        public bool CanRemoveFromCart
        {
            // Make sure something is selected
            // Make sure there is an item quantity
            get
            {
                bool output = false;
                if (SelectedCartItem!=null && SelectedCartItem?.QuantityInCart > 0)
                {
                    output = true;
                }
                return output;
            }
        }

        public void RemoveFromCart()
        {
            int amountToRemove = ItemQuantity <= SelectedCartItem.QuantityInCart ? ItemQuantity : SelectedCartItem.QuantityInCart;
            SelectedCartItem.Product.QuantityInStock += amountToRemove;
            if (SelectedCartItem.QuantityInCart> amountToRemove)
            {
                SelectedCartItem.QuantityInCart-=amountToRemove;
            }
            else
            {
                Cart.Remove(SelectedCartItem);
            }
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanCheckOut);
            NotifyOfPropertyChange(() => CanAddToCart);
        }

        public bool CanCheckOut
        {
            // Make sure something is in the cart
        
            get 
            {
                bool output = false;
                if (Cart.Count>0)
                {
                    output = true;
                }
                return output;
            }
        }

        public async Task CheckOut()
        {
            // create salemodel and post to the API
            SaleModel sale = new SaleModel();
            foreach (var item in Cart)
            {
                sale.SaleDetails.Add(new SaleDetailModel
                    {
                        ProductId = item.Product.Id,
                        Quantity = item.QuantityInCart
                    });
            }
           await _saleEndPoint.PostSaleAsync(sale).ConfigureAwait(false);

           await ResetSalesViewModel();
        }

    }
}

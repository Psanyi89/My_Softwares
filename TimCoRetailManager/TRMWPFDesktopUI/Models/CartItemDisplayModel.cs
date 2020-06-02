using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRMWPFDesktopUI.Models
{
   public class CartItemDisplayModel : PropertyChangedBase

    {
        public ProductDisplayModel Product { get; set; }

        private int _quantityInCart;
        public int QuantityInCart
        {
            get { return _quantityInCart; }
            set
            {
                if (value!=_quantityInCart)
                {
                    _quantityInCart = value;
                    NotifyOfPropertyChange(() => QuantityInCart);
                    NotifyOfPropertyChange(() => DisplayText);
                }
            }
        }
        public string DisplayText
        {
            get
            {
                return $"{Product.ProductName} ({QuantityInCart})";
            }
        }

        
    }
}

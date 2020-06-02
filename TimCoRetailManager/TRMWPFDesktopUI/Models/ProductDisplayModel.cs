﻿using Caliburn.Micro;
using System.Windows.Controls;

namespace TRMWPFDesktopUI.Models
{
    public class ProductDisplayModel :PropertyChangedBase
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal RetailPrice { get; set; }

        private int _quantityInStock;
        public int QuantityInStock 
        {
            get { return _quantityInStock; }

            set
            {
                if (value !=_quantityInStock)
                {
                    _quantityInStock = value;
                    NotifyOfPropertyChange(() => QuantityInStock);
                }
            }
        }
        public bool IsTaxable { get; set; }
    }
}
using Caliburn.Micro;
using ConsignmentShopUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsignmentShopLogicLibrary.TaskProcessor;
using System.ComponentModel.DataAnnotations;

namespace ConsignmentShopUI.ViewModels
{
  public class AddNewItemViewModel : Conductor<object>
    {
        private BindableCollection<Item> _items;
        private BindableCollection<Vendor> _vendors;
        private Vendor _selectedVendor;
        private Item _selectedItem;
        private string _title;
        private string _description;
        private decimal _price;
        private bool _sold;

        public AddNewItemViewModel()
        {
            Items = new BindableCollection<Item>(ItemsProcessor.GetItems<Item>());
            Vendors = new BindableCollection<Vendor>(VendorsProcessor.GetVendors<Vendor>());
        }

        public BindableCollection<Item> Items
        {
            get { return _items; }
            set { _items = value;
                NotifyOfPropertyChange(() => Items);
            }
        }

        public BindableCollection<Vendor> Vendors
        {
            get { return _vendors; }
            set
            {
                _vendors = value;
                NotifyOfPropertyChange(() => Vendors);
            }
        }

        public Vendor SelectedVendor
        {
            get { return _selectedVendor; }
            set
            {
                _selectedVendor = value;
                NotifyOfPropertyChange(() => SelectedVendor);
            }
        }

        public Item SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                NotifyOfPropertyChange(() => SelectedItem);
            }
        }

        [Required(ErrorMessage ="{0} must not be empty!")]
        public string Title
        {
            get { return _title; }
            set {
                _title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        public string Description
        {
            get { return _description; }
            set {
                _description = value;
                NotifyOfPropertyChange(() => Description);
            }
        }

        [Required(ErrorMessage ="{0} must not be empty!")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price
        {
            get { return _price; }
            set
            {
                _price = value;
                NotifyOfPropertyChange(() => Price);
            }
        }

        public bool Sold
        {
            get { return _sold; }
            set
            {
                _sold = value;
                NotifyOfPropertyChange(() => Sold);
            }
        }


    }
}

using Caliburn.Micro;
using ConsignmentShopUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsignmentShopLogicLibrary.TaskProcessor;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ConsignmentShopUI.ViewModels
{
  public class AddNewItemViewModel : Conductor<object>, IDataErrorInfo
    {
        private BindableCollection<Item> _items;
        private BindableCollection<Vendor> _vendors;
        private Vendor _selectedVendor;
        private Item _selectedItem;
        private string _title;
        private string _description;
        private decimal? _price;
        private bool? _sold;
        private int? _owner;
        private bool? _paymentDistributed;

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

        [Required(ErrorMessage ="An Owner must be selected!")]
        public Vendor SelectedVendor
        {
            get { return _selectedVendor; }
            set
            {
                _selectedVendor = value;
                if (value != null)
                {
                 
                Owner = value.VendorId;
                }
             
                NotifyOfPropertyChange(() => SelectedVendor);
               
            }
        }

        public Item SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                if(value!=null)
                {
                    Title = value.Title;
                    Description = value.Description;
                    Price = value.Price;
                    Sold = value.Sold;
                    PaymentDistributed = value.PaymentDistributed;
                    SelectedVendor = Vendors.Where(x => x.VendorId == value.Owner).FirstOrDefault();

                }

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
                NotifyOfPropertyChange(() => SelectedItem.Title);
            }
        }

        public string Description
        {
            get { return _description; }
            set {
                _description = value;
                NotifyOfPropertyChange(() => Description);
                NotifyOfPropertyChange(() => SelectedItem.Description);
            }
        }

        [Required(ErrorMessage ="{0} must not be empty!")]
        [Range(1,(double)decimal.MaxValue,ErrorMessage ="Cannot be Zero")]
        public decimal? Price
        {
            get { return _price; }
            set
            {
                _price = value;
                NotifyOfPropertyChange(() => Price);
                NotifyOfPropertyChange(() => SelectedItem.Price);
            }
        }

        public bool? Sold
        {
            get { return _sold; }
            set
            {
                _sold = value;
                NotifyOfPropertyChange(() => Sold);
                NotifyOfPropertyChange(() => SelectedItem.Sold);
            }
        }

        public bool? PaymentDistributed
        {
            get { return _paymentDistributed; }
            set {

                _paymentDistributed = value;
                NotifyOfPropertyChange(() => PaymentDistributed);
                NotifyOfPropertyChange(() => SelectedItem.PaymentDistributed);
            }
        }

        public int? Owner
        {
            get { return _owner; }
            set {
                _owner = value;
                NotifyOfPropertyChange(() => Owner);
                NotifyOfPropertyChange(() => SelectedItem.Owner);
            }
        }

        #region IDataErrorInfo interface implementations

        // check for general model error    
        /// <summary>
        /// WPF not using this so it's null
        /// </summary>
        public string Error => null;

        // check for property errors   
        /// <summary>
        /// checks property data annotation
        /// </summary>
        /// <param name="columnName">Name of property</param>
        /// <returns>Data annotation message</returns>
        public string this[string columnName]
        {
            get
            {
                List<ValidationResult> validationResults = new List<ValidationResult>();

                if (Validator.TryValidateProperty(
                        GetType().GetProperty(columnName).GetValue(this)
                        , new ValidationContext(this)
                        {
                            MemberName = columnName
                        }
                        , validationResults))
                {
                    return null;
                }

                return validationResults.First().ErrorMessage;
            }
        }
        #endregion

        public void AddItem()
        {
            Item addedItem = new Item
            {
                Title = Title,
                Description = Description,
                Price = Price,
                Sold = Sold,
                PaymentDistributed = PaymentDistributed,
                Owner = Owner
            };
            Items.Add(addedItem);          
            ItemsProcessor.InsertItem(addedItem);        
            Reset();
        }

        public void UpdateItem()
        {
            if (SelectedItem==null)
            {
                return;
            }
            Item updateItem = Items.Where(x => x.ItemId == SelectedItem.ItemId).FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(Title))
            {
                updateItem.Title = Title;
            }
            updateItem.Description = Description;
            if ( Price > 0)
            {
                updateItem.Price = Price;
            }
            updateItem.Sold = Sold;
            updateItem.PaymentDistributed = PaymentDistributed;
            if (SelectedVendor != null)
            {
                updateItem.Owner = Owner;
            }
            ItemsProcessor.UpdateItem(updateItem);
            Items.Refresh();
            Reset();
        }

        public void DeleteItem()
        {
            if(SelectedItem==null)
            {
                return;
            }
            Item deletedItem = Items.Where(x => x.ItemId == SelectedItem.ItemId).FirstOrDefault();
            try
            {
                ItemsProcessor.DeleteItem(deletedItem);
                Items.Remove(deletedItem);
            }
            catch (AggregateException)
            {
                var dialogViewModel = IoC.Get<DialogViewModel>();
                dialogViewModel.Title = "Error";
                dialogViewModel.Message = "Item cannot be deleted because it's added to a store";

                IWindowManager manager = new WindowManager();
                manager.ShowDialog(dialogViewModel);
            }
            Items.Refresh();
            Reset();
        }

        public void SearchItem()
        {
            Item searchedItem = new Item
            {
                Title=Title,
                Description=Description,
                Price=Price,
                Sold=Sold,
                PaymentDistributed=PaymentDistributed,
                 Owner=Owner
            };

            Items = new BindableCollection<Item>(ItemsProcessor.GetItems<Item>(searchedItem));
        }

        public void Reset()
        {
            Title = string.Empty;
            Description = string.Empty;
            Price = null;
            Sold = null;
            Owner = null;
            PaymentDistributed = null;
            SelectedVendor = null;
            SelectedItem = null;
        }
    }
}

using Caliburn.Micro;
using ConsignmentShopLogicLibrary.TaskProcessor;
using ConsignmentShopUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;

namespace ConsignmentShopUI.ViewModels
{
    public class AddItemToStoreViewModel : Conductor<object>, IDataErrorInfo
    {
        private BindableCollection<Item> _items;
        private BindableCollection<Store> _store;
        private BindableCollection<Vendor> _vendors;
        private Vendor _selectedVendor;
        private Item _selectedItem;
        private string _title;
        private string _description;
        private decimal? _price;
        private bool? _sold;
        private int? _owner;
        private bool? _paymentDistributed;
        private string _name;
        private Store _selectedStore;
        private BindableCollection<Item> _storeItems;
        private Item _selectedStoreItem;

        public AddItemToStoreViewModel()
        {
            Items = new BindableCollection<Item>(ItemsProcessor.GetItems<Item>());
            Stores = new BindableCollection<Store>(StoreProcessor.GetStores<Store>());
            Vendors = new BindableCollection<Vendor>(VendorsProcessor.GetVendors<Vendor>());
            StoreItems = new BindableCollection<Item>();
        }

        public BindableCollection<Item> Items
        {
            get => _items;
            set
            {
                _items = value;
                NotifyOfPropertyChange(() => Items);
            }
        }

        public BindableCollection<Store> Stores
        {
            get => _store;
            set
            {
                _store = value;
                NotifyOfPropertyChange(() => Stores);
            }
        }

        public BindableCollection<Vendor> Vendors
        {
            get => _vendors;
            set
            {
                _vendors = value;
                NotifyOfPropertyChange(() => Vendors);
            }
        }


        public Item SelectedStoreItem
        {
            get => _selectedStoreItem;
            set
            {
                _selectedStoreItem = value;
                if (value != null)
                {
                    Title = value.Title;
                    Description = value.Description;
                    Price = value.Price;
                    Sold = value.Sold;
                    PaymentDistributed = value.PaymentDistributed;
                    SelectedVendor = Vendors.Where(x => x.VendorId == value.Owner).FirstOrDefault();

                }
                NotifyOfPropertyChange(() => SelectedStoreItem);
            }
        }


        [Required(ErrorMessage = "An Owner must be selected!")]
        public Vendor SelectedVendor
        {
            get => _selectedVendor;
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

        public Store SelectedStore
        {
            get => _selectedStore;
            set
            {
                _selectedStore = value;

                if (SelectedStore != null)
                {
                    List<ItemsInStore> inStores = StoreProcessor.GetItemsInStores<ItemsInStore>(SelectedStore.StoreId).ToList();
                    StoreItems = new BindableCollection<Item>(from x in inStores
                                                              join y in Items on x.ItemId equals y.ItemId
                                                              select new Item
                                                              {
                                                                  Title = y.Title,
                                                                  Description = y.Description,
                                                                  ItemId = y.ItemId,
                                                                  Owner = y.Owner,
                                                                  PaymentDistributed = y.PaymentDistributed,
                                                                  Sold = y.Sold,
                                                                  Price = y.Price
                                                              });
                    Name = value.Name;
                }
                NotifyOfPropertyChange(() => SelectedStore);

            }
        }

        public BindableCollection<Item> StoreItems
        {
            get => _storeItems;
            set
            {
                _storeItems = value;
                NotifyOfPropertyChange(() => StoreItems);
                NotifyOfPropertyChange(() => SelectedStore);
            }
        }

        [Required(ErrorMessage = "Store name required")]
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
                NotifyOfPropertyChange(() => SelectedStore.Name);
            }
        }

        [Required(ErrorMessage = "An Item must be selected!")]
        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                if (value != null)
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

        [Required(ErrorMessage = "{0} must not be empty!")]
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyOfPropertyChange(() => Title);
                NotifyOfPropertyChange(() => SelectedItem.Title);
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                NotifyOfPropertyChange(() => Description);
                NotifyOfPropertyChange(() => SelectedItem.Description);
            }
        }

        [Required(ErrorMessage = "{0} must not be empty!")]
        [Range(1, (double)decimal.MaxValue, ErrorMessage = "Cannot be Zero")]
        public decimal? Price
        {
            get => _price;
            set
            {
                _price = value;
                NotifyOfPropertyChange(() => Price);
                NotifyOfPropertyChange(() => SelectedItem.Price);
            }
        }

        public bool? Sold
        {
            get => _sold;
            set
            {
                _sold = value;
                NotifyOfPropertyChange(() => Sold);
                NotifyOfPropertyChange(() => SelectedItem.Sold);
            }
        }

        public bool? PaymentDistributed
        {
            get => _paymentDistributed;
            set
            {

                _paymentDistributed = value;
                NotifyOfPropertyChange(() => PaymentDistributed);
                NotifyOfPropertyChange(() => SelectedItem.PaymentDistributed);
            }
        }

        public int? Owner
        {
            get => _owner;
            set
            {
                _owner = value;
                NotifyOfPropertyChange(() => Owner);
                NotifyOfPropertyChange(() => SelectedItem.Owner);
            }
        }

        public void AddItem()
        {
            if (SelectedItem != null && Name != null)
            {
                try
                {

                    StoreItems.Add(SelectedItem);

                    StoreProcessor.InsertStore(Name, SelectedItem.ItemId);
                }
                catch (AggregateException)
                {

                    MessageBox.Show("Item already added to store", "Store Processor", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }

            }
            Reset();
        }

        public void Reset()
        {
            StoreItems = null;
            SelectedStore = null;
            SelectedStoreItem = null;
            Name = null;
            Title = string.Empty;
            Description = string.Empty;
            Price = null;
            Sold = null;
            Owner = null;
            PaymentDistributed = null;
            SelectedVendor = null;
            SelectedItem = null;
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

    }
}

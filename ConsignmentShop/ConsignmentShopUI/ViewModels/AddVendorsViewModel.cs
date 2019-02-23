using Caliburn.Micro;
using static ConsignmentShopLogicLibrary.TaskProcessor.VendorsProcessor;
using ConsignmentShopUI.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ConsignmentShopUI.ViewModels
{

    public class AddVendorsViewModel : Conductor<object>, IDataErrorInfo
    {
        private int? _comission;
        private string _firstName;
        private string _lastName;
        private BindableCollection<Vendor> _vendors;
        private Vendor _selectedVendor;

        public AddVendorsViewModel()
        {
            Vendors = new BindableCollection<Vendor>(GetVendors<Vendor>());
        }

        public Vendor SelectedVendor
        {
            get => _selectedVendor;
            set
            {
                _selectedVendor = value;
                if (value != null)
                {
                    FirstName = value.FirstName;
                    LastName = value.LastName;
                    Comission = (int)value.Comission;
                }
                NotifyOfPropertyChange(() => SelectedVendor);
                NotifyOfPropertyChange(() => Vendors);
            }
        }

        [Required(ErrorMessage = "{0} must not be empty")]
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;

                NotifyOfPropertyChange(() => LastName);
                NotifyOfPropertyChange(() => SelectedVendor.LastName);
            }
        }
        [Required(ErrorMessage ="Comission rate is required")]
        [Range(1, 100, ErrorMessage = "{0} should be between 1 and 100")]
        public int? Comission
        {
            get => _comission;
            set
            {
                _comission = value;

                NotifyOfPropertyChange(() => Comission);
                NotifyOfPropertyChange(() => SelectedVendor.Comission);
            }
        }

        [Required(ErrorMessage = "{0} must not be empty")]
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                NotifyOfPropertyChange(() => FirstName);
                NotifyOfPropertyChange(() => SelectedVendor.FirstName);

            }
        }

        #region Vendors BindableCollection
        /// <summary>
        /// BindableCollection of VendorModel
        /// </summary>
        public BindableCollection<Vendor> Vendors
        {
            get => _vendors;
            set
            {
                _vendors = value;
                NotifyOfPropertyChange(() => Vendors);
            }
        }
        #endregion

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

        #region Updates selected vendor properties from Vendors Bindablecollection
        /// <summary>
        /// Updates selected vendor properties from Vendors Bindablecollection
        /// </summary>
        public void UpdateVendor()
        {
            if (SelectedVendor == null)
            {
                return;
            }
            Vendor updatedPerson = Vendors.Where(x => x.VendorId == SelectedVendor.VendorId).FirstOrDefault();
            if (updatedPerson == null)
            {
                return;
            }
            if (!string.IsNullOrWhiteSpace(FirstName))
            {
                updatedPerson.FirstName = FirstName;
            }
            if (!string.IsNullOrWhiteSpace(LastName))
            {
                updatedPerson.LastName = LastName;
            }
            if (Comission != 0)
            {
                updatedPerson.Comission = Comission;
            }
            Vendors.Refresh();
            UpdatingVendor(updatedPerson);
            Reset();
        }
        #endregion

        #region Delete Vendor from Vendors Bindablecollection
        /// <summary>
        /// Deletes vendor from Vendors BindableCollection
        /// </summary>
        public void DeleteVendor()
        {
            if (SelectedVendor == null)
            {
                return;
            }
            Vendor deletedVendor = Vendors.
               Where(x => x.VendorId == SelectedVendor.VendorId).
               FirstOrDefault();
            if (deletedVendor == null)
            {
                return;
            }
            Vendors.Remove(deletedVendor);
            DeletingVendor(deletedVendor);
            Reset();
        }
        #endregion

        #region Adds new vendor to Vendors BindableCollection
        /// <summary>
        /// Add new Vendor to Vendors BindableCollection
        /// </summary>
        public void AddVendor()
        {
            Vendor updatedVendor = Vendors.Where(x => x.FirstName == FirstName && x.LastName == LastName).FirstOrDefault();
            Vendor addedVendor = new Vendor();
            if (updatedVendor == null)
            {
             addedVendor = new Vendor
            {
                VendorId = Vendors.Count+1,
                FirstName = FirstName,
                LastName = LastName,
                Comission = Comission
            };

            }
            else
            {
                updatedVendor.Comission = Comission;
            }
            Vendors.Add(addedVendor??updatedVendor);
            InsertVendor(addedVendor??updatedVendor);
            Reset();
        }
        #endregion

        #region Search in Vendors
        public void SearchVendor()
        {
            Vendor searchedVendors = new Vendor
            {
                FirstName = FirstName,
                LastName = LastName,
                Comission =Comission
            };
            Vendors =new BindableCollection<Vendor>(GetVendors<Vendor>(searchedVendors));

        }
        #endregion

        #region Resets textboxes content
        /// <summary>
        /// Resets textboxes content
        /// </summary>
        public void Reset()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Comission = null;
            SelectedVendor = null;
        }
        #endregion
    }
}

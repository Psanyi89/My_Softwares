using ConsigmentShopReactiveUI.Models;
using ConsignmentShopLogicLibrary.TaskProcessor;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Helpers;
using System;
using System.Collections.ObjectModel;

namespace ConsigmentShopReactiveUI
{
    public class AddVendorsViewModel : ReactiveObject, IRoutableViewModel, ISupportsValidation
    {
        public string UrlPathSegment => "Vendors";
        public IScreen HostScreen { get; }

        // Initialize validation context that manages reactive validations.
        public ValidationContext ValidationContext { get; } = new ValidationContext();

        // Declare a separate validator for complex rule.
        public ValidationHelper ComplexRule { get; }

        // Declare a separate validator for comission rule.
        public ValidationHelper ComissionRule { get; }

        [Reactive] public int? Comission { get; set; }
        [Reactive] public string FirstName { get; set; }
        [Reactive] public string LastName { get; set; }
        [Reactive] public Vendor SelectedVendor { get; set; }
        private readonly ReadOnlyObservableCollection<Vendor> _derived;
        public ReadOnlyObservableCollection<Vendor> Derived => _derived;
        public ObservableCollectionExtended<Vendor> Vendors { get; }
        public AddVendorsViewModel()
        {
            Vendors =new ObservableCollectionExtended<Vendor>(VendorsProcessor.GetVendors<Vendor>());
        }
    }

}

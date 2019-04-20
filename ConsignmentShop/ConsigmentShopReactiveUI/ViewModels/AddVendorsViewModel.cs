using ConsigmentShopReactiveUI.Models;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Helpers;
using System;

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
        private readonly SourceList<Vendor> _vendors = new SourceList<Vendor>();
        public IObservable<IChangeSet<Vendor>> Vendors()
        {
            return _vendors.Connect();
        }

        public AddVendorsViewModel()
        {

        }
    }

}

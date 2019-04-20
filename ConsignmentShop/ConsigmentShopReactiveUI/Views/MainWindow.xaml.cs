using ReactiveUI;
using System.Reactive.Disposables;

namespace ConsigmentShopReactiveUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainWindowViewModel();
            
            this.WhenActivated(disposable => 
            {
                this.OneWayBind(ViewModel, vm => vm.Router, v => v.ActiveItem.Router)
                .DisposeWith(disposable);
                this.BindCommand(ViewModel, vm => vm.OpenShop, v => v.OpenShopButton)
                .DisposeWith(disposable);
                this.BindCommand(ViewModel, vm => vm.AddVendor, v => v.AddVendorButton)
               .DisposeWith(disposable);
                this.BindCommand(ViewModel, vm => vm.AddItem, v => v.AddItemButton)
            .DisposeWith(disposable);
                this.BindCommand(ViewModel, vm => vm.AddStore, v => v.AddStoreButton)
           .DisposeWith(disposable);
                this.BindCommand(ViewModel, vm => vm.GoBack, v => v.GoBackButton)
           .DisposeWith(disposable);
            });
        }

     
    }
}

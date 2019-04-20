using ReactiveUI;
using System.Reactive.Disposables;

namespace ConsigmentShopReactiveUI
{
    /// <summary>
    /// Interaction logic for ShopView.xaml
    /// </summary>
    public partial class ShopView : ReactiveUserControl<ShopViewModel>
    {
        public ShopView()
        {
            InitializeComponent();
            ViewModel = new ShopViewModel();
            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel, vm => vm.UrlPathSegment, v => v.AddToCart.Content)
                .DisposeWith(disposable);
            });
        }
    }
}

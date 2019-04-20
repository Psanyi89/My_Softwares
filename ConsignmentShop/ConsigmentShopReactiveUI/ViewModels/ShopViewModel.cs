using ReactiveUI;
using Splat;

namespace ConsigmentShopReactiveUI
{
    public class ShopViewModel : ReactiveObject,IRoutableViewModel
    {
        public string UrlPathSegment => "Add to cart";

        public IScreen HostScreen { get; }
        public ShopViewModel()
        {
            // HostScreen = screen ?? Locator.Current.GetService<IScreen>();

        }
    }
}
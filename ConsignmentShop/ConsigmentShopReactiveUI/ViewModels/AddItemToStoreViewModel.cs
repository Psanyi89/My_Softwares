using ReactiveUI;

namespace ConsigmentShopReactiveUI.ViewModels
{
    public class AddItemToStoreViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment => "Store";

        public IScreen HostScreen { get; }
        public AddItemToStoreViewModel()
        {

        }
    }
}

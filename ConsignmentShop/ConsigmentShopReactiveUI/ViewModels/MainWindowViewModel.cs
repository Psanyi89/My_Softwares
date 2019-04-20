using ConsigmentShopReactiveUI.ViewModels;
using ReactiveUI;
using System.Reactive;

namespace ConsigmentShopReactiveUI
{
    public class MainWindowViewModel : ReactiveObject, IScreen
    {
        public RoutingState Router { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> OpenShop { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> AddVendor { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> AddItem { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> AddStore { get; }
        // The command that navigates a user back.
        public ReactiveCommand<Unit, Unit> GoBack { get; }

        public MainWindowViewModel()
        {
            Router = new RoutingState();
            //Locator.CurrentMutable.Register(() => new ShopViewModel(),
            //    typeof(IViewFor<ShopViewModel>));
            OpenShop = ReactiveCommand.CreateFromObservable(()
                => Router.Navigate.Execute(new ShopViewModel()));
            AddVendor = ReactiveCommand.CreateFromObservable(()
                => Router.Navigate.Execute(new AddVendorsViewModel()));
            AddItem = ReactiveCommand.CreateFromObservable(()
                  => Router.Navigate.Execute(new AddNewItemViewModel()));
            AddStore = ReactiveCommand.CreateFromObservable(()
                 => Router.Navigate.Execute(new AddItemToStoreViewModel()));
            GoBack = Router.NavigateBack;
        }
    }
}

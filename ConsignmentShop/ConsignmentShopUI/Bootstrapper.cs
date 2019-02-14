using Caliburn.Micro;
using ConsignmentShopUI.ViewModels;
using System.Windows;

namespace ConsignmentShopUI
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainMenuViewModel>();
        }
    }
}

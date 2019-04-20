using ReactiveUI;

namespace ConsigmentShopReactiveUI
{
    /// <summary>
    /// Interaction logic for AddVendorsView.xaml
    /// </summary>
    public partial class AddVendorsView : ReactiveUserControl<AddVendorsViewModel>
    {
        public AddVendorsView()
        {
            InitializeComponent();
            ViewModel = new AddVendorsViewModel();
        }
    }
}
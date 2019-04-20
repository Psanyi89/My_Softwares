using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ConsignmentShopReactiveUI.Views
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : ReactiveWindow<ViewModels.MainViewModel>
    {
        public MainWindowView()
        {
            InitializeComponent();
          ViewModel = new ViewModels.MainViewModel();

            this.WhenActivated(disposableRegisted => {
                this.OneWayBind(viewmodel)
            });
        }
    }
}

using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsigmentShopReactiveUI.ViewModels
{
    public class AddNewItemViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment => "Add New Item";

        public IScreen HostScreen { get; }
        public AddNewItemViewModel()
        {

        }
    }
}

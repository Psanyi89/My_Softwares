using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsigmentShopReactiveUI.ViewModels
{
    public class DialogViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment => "Dialog";

        public IScreen HostScreen { get; }
        public DialogViewModel()
        {

        }
    }
}

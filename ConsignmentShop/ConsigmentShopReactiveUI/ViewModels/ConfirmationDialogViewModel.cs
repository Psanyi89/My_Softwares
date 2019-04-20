using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsigmentShopReactiveUI.ViewModels
{
    public class ConfirmationDialogViewModel : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment => "Confirmation Dialog";

        public IScreen HostScreen { get; }
        public ConfirmationDialogViewModel()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsigmentShopReactiveUI.Models
{
    public enum DialogResult
    {
        Yes,
        No
    }
    public enum DialogType
    {
        None,
        Question,
        Warning,
        Information,
        Error
    }
}

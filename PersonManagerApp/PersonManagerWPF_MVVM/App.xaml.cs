using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
[assembly: log4net.Config.XmlConfigurator(ConfigFileExtension = ".config", ConfigFile = "log4net.config", Watch = true)]

namespace PersonManagerWPF_MVVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
    }
}

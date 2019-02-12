using System.Runtime.CompilerServices;
[assembly: log4net.Config.XmlConfigurator(ConfigFileExtension = ".config", ConfigFile = "App.config", Watch = true)]
namespace DataLayerLogic
{
    public class LogHelper
    {
        public static log4net.ILog GetLogger([CallerFilePath] string filename = "")
        {
            return log4net.LogManager.GetLogger(filename);
        }
    }
}

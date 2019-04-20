using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsignmentShopLogicLibrary;
using ConsignmentShopLogicLibrary.Helpers;

namespace HelperApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Encrypt.DecryptString($@"{ConfigurationManager.ConnectionStrings["LocalDB"].ConnectionString}", "Szeress amíg lehet"));
            Console.ReadKey();
        }
    }
}

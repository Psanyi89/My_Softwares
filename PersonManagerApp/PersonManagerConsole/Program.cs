using DataLayerLogic;
using PersonEntities;
using System;
using System.Collections.Generic;

[assembly: log4net.Config.XmlConfigurator(ConfigFileExtension = ".config", ConfigFile ="App.config", Watch = true)]
namespace PersonManagerConsole
{
    internal class Program
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();

        private static void Main(string[] args)
        {
            ////while (true)
            ////{
            //    log.Debug("Lowest level");
            //    log.Info("Second level ");
            //    log.Warn("Third Level");
            //    var i = 0;
            //    try
            //    {
            //        var x = 10 / i;
            //    }
            //    catch (DivideByZeroException ex)
            //    {

            //        log.Error($"{ex.Message}{Environment.NewLine}{ex.StackTrace}");
            //    }
            //    log.Fatal("Highest level");
            ////}

            //IPersonManager pm = new DLLFacade().GetPersonManagerMemory();
            //IPersonManager pm = new DLLFacade().GetPersonManagerTxt();
            //IPersonManager pm = new DLLFacade().GetPersonManagerCSV();
            //IPersonManager pm = new DLLFacade().GetPersonManagerXml();
            //IPersonManager pm = new DLLFacade().GetPersonManagerToml();
            //IPersonManager pm = new DLLFacade().GetPersonManagerJson();
            //People people = new People
            //{
            //    Persons = pm.GetPersons(),
            //};
            //List<Person> people =ReadFileTypes.ReadTxtFileToList<Person>("FakeDB",true);
            //people.ForEach(x => Console.WriteLine(x.ToString()));
            //people.CreateToml("Test");
            //pm.GetPersons().ForEach(x => Console.WriteLine($"{x.ToString()}"));
            //Console.WriteLine();
            ////Create
            //Person bob = pm.AddPerson(new Person { Name = "Bob", DateOfBirth = Convert.ToDateTime("1950.01.20"), Email = "test@.test.com" });
            //pm.GetPersons().ForEach(x => Console.WriteLine(x.ToString()));
            //// Search
            //pm.SearchResult("james", null, null).ForEach(x => Console.WriteLine(x.ToString()));
            //pm.SearchResult(null,Convert.ToDateTime("1950.10.10"), null).ForEach(x => Console.WriteLine(x.ToString()));
            //pm.SearchResult(null, null, "us").ForEach(x => Console.WriteLine(x.ToString()));
            ////Update
            //Console.WriteLine();
            //bob.Name = "Jhonny";
            //pm.UpdatePerson(bob);
            //pm.GetPersons().ForEach(x => Console.WriteLine(x));
            //// Delete
            //Console.WriteLine();
            //pm.DeletePerson(bob);
            //pm.GetPersons().ForEach(x => Console.WriteLine(x));
            Console.ReadKey();
        }
    }
}

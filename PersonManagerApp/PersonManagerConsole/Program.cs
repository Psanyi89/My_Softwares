using DataLayerLogic;
using PersonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManagerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
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

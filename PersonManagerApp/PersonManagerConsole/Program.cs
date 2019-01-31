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
            //   IPersonManager pm= new DLLFacade().GetPersonManagerMemory();
            IPersonManager pm = new DLLFacade().GetPersonManagerTxt();
            pm.GetPersons().ForEach(x => Console.WriteLine($"{x.Id} {x.Name} {x.DateOfBirth.ToShortDateString()} {x.Email}"));
            Console.WriteLine();
        
           //Create
           //Person bob= pm.AddPerson(new Person { Name = "Bob" });
           // pm.GetPersons().ForEach(x => Console.WriteLine(x));
           // // Read
           // pm.GetPersons();
           // //Update
           // Console.WriteLine();
           // bob.Name = "Jhonny";
           // pm.UpdatePerson(bob);
           // pm.GetPersons().ForEach(x => Console.WriteLine(x));
           // // Delete
           // Console.WriteLine();
           // //pm.DeletePerson();
           // pm.GetPersons().ForEach(x => Console.WriteLine(x));
            Console.ReadKey();
        }
    }
}

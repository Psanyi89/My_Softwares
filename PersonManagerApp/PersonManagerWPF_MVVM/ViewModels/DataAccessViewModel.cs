using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using DataLayerLogic;
using PersonEntities;

namespace PersonManagerWPF_MVVM.ViewModels
{
    public class DataAccessViewModel : Conductor<object>
    {

        public BindableCollection<Person> People { get; set; }
        private IPersonManager pm = new DLLFacade().GetPersonManagerMemory();

        public DataAccessViewModel()
        {
            People = new BindableCollection<Person>(pm.GetPersons());
        }

        public void AddPerson()
        {
            Random rnd = new Random();
            var myList = pm.GetPersons();
            int index = rnd.Next(1,100);
            var person =myList.Where(x=>x.Id==index ).First();
          var addedPerson=  pm.AddPerson(person);
            People.Add(addedPerson);

        }

        public void RemovePerson()
        {
            var deletePerson = People.LastOrDefault();
            if (deletePerson!=null)
            {

            var result=pm.DeletePerson(deletePerson);
            result=true ? People.Remove(deletePerson):throw new NullReferenceException("Person not exists");
        }
            else
            {
               MessageBox.Show("List empty");
            }
            }
    }
}

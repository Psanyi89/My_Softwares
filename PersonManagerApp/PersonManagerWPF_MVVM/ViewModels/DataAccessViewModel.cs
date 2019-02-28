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
        private IPersonManager _personManager;
        

        public BindableCollection<Person> People { get; set; }
     

        public DataAccessViewModel()
        {
            _personManager =DLLFacade.CreateManager(AccessType.Memory);
            People = new BindableCollection<Person>(_personManager.GetPersons());
        }

        public void AddPerson()
        {
            Random rnd = new Random();
            var myList = _personManager.GetPersons();
            int index = rnd.Next(1,100);
            var person =myList.Where(x=>x.Id==index ).First();
          var addedPerson=  _personManager.AddPerson(person);
            People.Add(addedPerson);

        }

        public void RemovePerson()
        {
            var deletePerson = People.LastOrDefault();
            if (deletePerson!=null)
            {

            var result=_personManager.DeletePerson(deletePerson);
            result=true ? People.Remove(deletePerson):throw new NullReferenceException("Person not exists");
        }
            else
            {
               MessageBox.Show("List empty");
            }
            }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonEntities;
using static DataLayerLogic.Managers.CommonMSSqlPersonManager;
namespace DataLayerLogic.Managers
{
    class PersonManagerLocalDB : IPersonManager
    {
        public Person AddPerson(Person person)
        {
           return SqlCommonAddIPerson(person);
        }

        public bool DeletePerson(Person person)
        {
            return SqlCommonDeletePerson(person);
        }

        public List<Person> GetPersons()
        {
            return SqlCommonGetPersons<Person>().ToList();
        }

        public List<Person> SearchResult(string name = null, DateTime? dateOfBirth = null, string email = null)
        {
            return SqlCommonSearchResult<Person>(name,dateOfBirth,email).ToList();
        }

        public Person UpdatePerson(Person person)
        {
        return SqlCommonUpdatePerson(person);
          
        }
    }
}

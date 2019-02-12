using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonEntities;

namespace DataLayerLogic.Managers
{
   internal  class PersonManagerBinary : IPersonManager
    {
        private readonly string filePath = Path.Combine("Resources", "FakeDB.bin") ;
        private List<Person> BinaryDatabase = new List<Person>();
        public PersonManagerBinary()
        {
            var people = ReadFileTypes.ReadBinaryFile<List<Person>>(filePath);
            people.ForEach(x => AddPerson(x));
        }
        public Person AddPerson(Person person)
        {
            bool wasnull = false;
          var addedPerson= CommonPersonManager.CommonAddPerson(person,BinaryDatabase,ref wasnull);
            if (wasnull)
            {
                addedPerson.CreateBinarty(filePath,true);
            }
            return addedPerson;
        }

        public List<Person> GetPersons()
        {
           return new List<Person>(BinaryDatabase);
        }

        public Person UpdatePerson(Person person)
        {
            var updatedPerson=CommonPersonManager.CommonUpdatePerson(person,BinaryDatabase);
            BinaryDatabase.CreateBinarty(filePath);
            return updatedPerson;
        }

        public bool DeletePerson(Person person)
        {
          var result= CommonPersonManager.CommonDeleteIPerson(person,BinaryDatabase);
            BinaryDatabase.CreateBinarty(filePath);
            return result;
        }

        public List<Person> SearchResult(string name = null, DateTime? dateOfBirth = null, string email = null)
        {
          return CommonPersonManager.CommonSearch(BinaryDatabase,name,dateOfBirth,email).ToList();
        }

    }
}

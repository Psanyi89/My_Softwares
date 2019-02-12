using Nett;
using PersonEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerLogic.Managers
{
    internal class PersonManagerToml: IPersonManager
    {
        private readonly List<Person> TomlDataBase = new List<Person>();
        private readonly string filePath = Path.Combine("Resources", "FakeDB");
      
        #region TomlDataBase Constructor
        /// <summary>
        /// Toml database constructor
        /// </summary>
        public PersonManagerToml()
        {
            var people = ReadFileTypes.ReadTomlToList<People>(filePath);
          
            people.Persons.ForEach(x =>AddPerson(x));
          
        }
        #endregion
      
        #region Create new Person
        /// <summary>
        /// Add a person to the Xml database
        /// </summary>
        /// <param name="person">person object</param>
        /// <returns></returns>
        public Person AddPerson(Person person)
        {          
                bool wasnull = false;
            Person addedPerson = CommonPersonManager.CommonAddPerson(person, TomlDataBase, ref wasnull);
                if (wasnull)
                {
                    People people = new People
                    {
                        Persons = TomlDataBase
                    };
                    people.CreateToml(filePath);
                }
                return addedPerson;
        }
        
    
        #endregion

        #region Read Database

        /// <summary>
        /// returns the people stored in Xml database
        /// </summary>
        /// <returns>returns that the task was successful or not</returns>
        public List<Person> GetPersons()
        {
            return new List<Person>(TomlDataBase);
        }
        #endregion

        #region Update Person

        /// <summary>
        /// You can update an exiting person's properties except its id
        /// </summary>
        /// <param name="person">person id need to be valid</param>
        /// <returns></returns>
        public Person UpdatePerson(Person person)
        {

            Person updatePerson = CommonPersonManager.CommonUpdatePerson(person, TomlDataBase);
            People people = new People
            {
                Persons = TomlDataBase
            };
            people.CreateToml(filePath);
            return updatePerson;
        }
        #endregion

        #region Delete Person

        /// <summary>
        /// You can delete an existing person from the Xml database
        /// </summary>
        /// <param name="p">person id need to be valid</param>
        /// <returns></returns>
        public bool DeletePerson(Person person)
        {
            bool result = CommonPersonManager.CommonDeleteIPerson(person, TomlDataBase);
                    People people = new People
                    {
                        Persons = TomlDataBase
                    };
                    people.CreateToml(filePath);
                    return result;          
        }
        #endregion

        #region Search query in database
        /// <summary>
        /// Search the database for records that meets our parameters
        /// </summary>
        /// <param name="name">Checks that any record contains this name</param>
        /// <param name="dateOfBirth">looks for records from this year</param>
        /// <param name="email">checks that any record contains this email</param>
        /// <returns>returns the list of people whos passed the filtering</returns>
        public List<Person> SearchResult(string name = null, DateTime? dateOfBirth = null, string email = null)
        {
            List<Person> result = GetPersons();
            return CommonPersonManager.CommonSearch(result, name, dateOfBirth, email).ToList();
        }
        #endregion

    }
}

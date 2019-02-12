using PersonEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataLayerLogic.Managers
{
    internal class PersonManagerCSV : IPersonManager
    {
        private readonly string filePath = Path.Combine("Resources", "FakeDB.csv");
        private readonly List<Person> CSVDataBase = new List<Person>();

        #region Constructor
        /// <summary>
        /// PersonManagerCSV Constructor
        /// </summary>
        public PersonManagerCSV()
        {
            List<Person> people = ReadFileTypes.ReadCSVToList<Person>(filePath).ToList();
            foreach (Person item in people)
            {
                AddPerson(item);
            }

        }
        #endregion

        #region Create new Person
        /// <summary>
        /// Add a person to the CSV database
        /// </summary>
        /// <param name="person">person object</param>
        /// <returns>Returns the added Person</returns>
        public Person AddPerson(Person person)
        {
            bool wasnull = false;
            Person addedPerson = CommonPersonManager.CommonAddPerson(person, CSVDataBase,ref wasnull);
            if (wasnull)
            {
                CSVDataBase.CreateCSV(filePath);
            }
            return addedPerson;
        }
        #endregion

        #region Read Database

        /// <summary>
        /// returns the people stored in the CSV database
        /// </summary>
        /// <returns>Returns a List<Person> from the database</returns>
        public List<Person> GetPersons()
        {
            return new List<Person>(CSVDataBase);
        }
        #endregion

        #region Update Person

        /// <summary>
        /// You can update a person properties except its id
        /// </summary>
        /// <param name="person">person id need to be valid</param>
        /// <returns>Returns the updated Person</returns>
        public Person UpdatePerson(Person person)
        {
            Person selectedPerson = CommonPersonManager.CommonUpdatePerson(person, CSVDataBase);
                CSVDataBase.CreateCSV(filePath);
                return selectedPerson;
        }
        #endregion

        #region Delete Person

        /// <summary>
        /// You can delete an existing person from the CSV database
        /// </summary>
        /// <param name="p">person id need to be valid</param>
        /// <returns></returns>
        public bool DeletePerson(Person person)
        {
            bool result = CommonPersonManager.CommonDeleteIPerson(person, CSVDataBase);
            CSVDataBase.CreateCSV(filePath);
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

using PersonEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataLayerLogic.Managers
{
    internal class PersonManagerJson : IPersonManager
    {
        private readonly List<Person> JsonDataBase = new List<Person>();
        private readonly string filePath = Path.Combine("Resources", "FakeDB.json");

        #region Constructor
        /// <summary>
        /// PersonManagerJson Constructor
        /// </summary>
        public PersonManagerJson()
        {
            List<Person> people = ReadFileTypes.ReadJsonToList<Person>(filePath).ToList();
            people.ForEach(x => AddPerson(x));
        }
        #endregion

        #region Create new Person
        /// <summary>
        /// Add a person to the Jsondatabase
        /// </summary>
        /// <param name="person">person object</param>
        /// <returns>Returns the added person object</returns>
        public Person AddPerson(Person person)
        {
            bool wasnull = false;
            Person addedPerson = CommonPersonManager.CommonAddPerson(person, JsonDataBase, ref wasnull);
            if (wasnull)
            {
                JsonDataBase.CreateJson(filePath);
            }
            return addedPerson;
        }
        #endregion

        #region Read Database

        /// <summary>
        /// returns the people stored in the database
        /// </summary>
        /// <returns>Returns the list of People stored in Json database</returns>
        public List<Person> GetPersons()
        {
            return new List<Person>(JsonDataBase);
        }
        #endregion

        #region Update Person

        /// <summary>
        /// You can update a person properties except its id
        /// </summary>
        /// <param name="person">person id need to be valid</param>
        /// <returns>Returns the updated person</returns>
        public Person UpdatePerson(Person person)
        {
            Person updatedPerson = CommonPersonManager.CommonUpdatePerson(person, JsonDataBase);
            JsonDataBase.CreateJson(filePath);
            return updatedPerson;
        }
        #endregion

        #region Delete Person

        /// <summary>
        /// You can delete a person from the database
        /// </summary>
        /// <param name="p">person id need to be valid</param>
        /// <returns>Returns that the process was successful or not</returns>
        public bool DeletePerson(Person person)
        {
            bool result = CommonPersonManager.CommonDeleteIPerson(person, JsonDataBase);
            JsonDataBase.CreateJson(filePath);
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

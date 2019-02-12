using MongoDB.Driver;
using PersonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using static DataLayerLogic.Managers.CommonPersonManager;
namespace DataLayerLogic.Managers
{
    internal class PersonManagerFakeDB : IPersonManager
    {
        private readonly List<Person> _personFakeDB = new List<Person>();

        #region Constructor
        /// <summary>
        /// PersonManagerFakeDB Constructor
        /// </summary>
        public PersonManagerFakeDB()
        {
            #region  Create FakeDB in Memory using NBuilder and Faker.Net

            ICollection<Person> people = GenerateIPersonCollection<Person>(100);
            #endregion

            foreach (Person item in people)
            {
                AddPerson(item);
            }

        }
        #endregion

        #region Create new Person
        /// <summary>
        /// Add a person to the fake database
        /// </summary>
        /// <param name="person">person object</param>
        /// <returns></returns>
        public Person AddPerson(Person person)
        {
            bool wasnull = false;
           return CommonAddPerson(person, _personFakeDB, ref wasnull);
            
        }

        #endregion

        #region Read Database

        /// <summary>
        /// returns the people stored in the database
        /// </summary>
        /// <returns>Returns the list of people stored in database</returns>
        public List<Person> GetPersons()
        {
            return new List<Person>(_personFakeDB);
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
            return CommonUpdatePerson(person, _personFakeDB);
        }
        #endregion

        #region Delete Person

        /// <summary>
        /// You can delete a person from the database
        /// </summary>
        /// <param name="p">person id need to be valid</param>
        /// <returns>Returns that the process was successful or not</returns>
        public bool DeletePerson(Person p)
        {
            return CommonDeleteIPerson(p, _personFakeDB);
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
            return CommonSearch(result, name, dateOfBirth, email).ToList();
        }
        #endregion

    }
}

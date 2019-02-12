using PersonEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataLayerLogic.Managers
{
    internal class TxtPersonManager : IPersonManager
    {
        private readonly List<Person> TxtDBdatabase = new List<Person>();
        private readonly string filePath =Path.Combine("Resources","FakeDB.txt");

        #region Constructor
        /// <summary>
        /// TxtPersonManager Constructor
        /// </summary>
        public TxtPersonManager()
        {
            List<Person> people = ReadFileTypes.ReadTxtFileToList<Person>(filePath, true).ToList();
            foreach (Person item in people)
            {
                AddPerson(item);
            }
        }
        #endregion

        #region Create new Person
        /// <summary>
        /// Add a person to the Txt database
        /// </summary>
        /// <param name="person">person object</param>
        /// <returns></returns>
        public Person AddPerson(Person person)
        {

            bool wasnull = false;
            Person addedPerson = CommonPersonManager.CommonAddPerson(person, TxtDBdatabase,ref wasnull);
            if (wasnull == true)
            {
                TxtDBdatabase.CreateTxt(filePath);
            }

            return addedPerson;
        }
        #endregion

        #region Read Database

        /// <summary>
        /// returns the people stored in the Txt database
        /// </summary>
        /// <returns></returns>
        public List<Person> GetPersons()
        {
            return new List<Person>(TxtDBdatabase);

        }
        #endregion

        #region Update Person

        /// <summary>
        /// You can update a person properties except its id
        /// </summary>
        /// <param name="person">person id need to be valid</param>
        /// <returns></returns>
        public Person UpdatePerson(Person person)
        {
            Person updatedperson = CommonPersonManager.CommonUpdatePerson(person, TxtDBdatabase);
                TxtDBdatabase.CreateTxt(filePath);
            return updatedperson;
        }
        #endregion

        #region Delete Person

        /// <summary>
        /// You can delete an existing person from the Txt database
        /// </summary>
        /// <param name="p">person id need to be valid</param>
        /// <returns></returns>
        public bool DeletePerson(Person person)
        {
            bool result = CommonPersonManager.CommonDeleteIPerson(person, TxtDBdatabase);
            TxtDBdatabase.CreateTxt(filePath);
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

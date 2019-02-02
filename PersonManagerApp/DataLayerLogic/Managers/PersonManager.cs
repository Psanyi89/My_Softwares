using Faker;
using FizzWare.NBuilder;
using MongoDB.Driver;
using PersonEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataLayerLogic.Managers
{
    internal class PersonManagerFakeDB : IPersonManager
    {
        private int _id = 1;
        private readonly string filePath = "FakeDB";
        private readonly List<Person> _personFakeDB = new List<Person>();
        
        #region Constructor
        /// <summary>
        /// PersonManagerFakeDB Constructor
        /// </summary>
        public PersonManagerFakeDB()
        {
            #region  Create FakeDB in Memory using NBuilder and Faker.Net

            IList<Person> people = Builder<Person>.CreateListOfSize(100).All().
                With(x => x.Name = Name.FullName())
                .With(x => x.Email = Internet.Email())
                .With(x => x.DateOfBirth = DateTime.Today.AddDays(-RandomNumber.Next(5000, 30000)).Date)
                .Build();
            #endregion

            foreach (Person item in people.ToArray())
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
           
            Person addedPerson;
            _personFakeDB.Add(addedPerson = new Person
            {
                Name = person.Name,
                DateOfBirth = person.DateOfBirth,
                Email = person.Email,
                Id = _id++
            });
            return addedPerson;
        }

        #endregion

        #region Read Database

        /// <summary>
        /// returns the people stored in the database
        /// </summary>
        /// <returns></returns>
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
        /// <returns></returns>
        public Person UpdatePerson(Person person)
        {
            Person personFromList = _personFakeDB.FirstOrDefault(x => x.Id == person.Id);
            if (personFromList != null)
            {
                personFromList.Name = person.Name;
                return personFromList;
            }
            return null;
        }
        #endregion

        #region Delete Person

        /// <summary>
        /// You can delete a person from the database
        /// </summary>
        /// <param name="p">person id need to be valid</param>
        /// <returns></returns>
        public bool DeletePerson(Person p)
        {
            if (p == null)
            {
                throw new InvalidDataException("Person cannot be null");
            }
            return _personFakeDB.RemoveAll(x => x.Id == p.Id) > 0;

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
            bool validname = !string.IsNullOrWhiteSpace(name);
            bool validEmail = !string.IsNullOrWhiteSpace(email);
            bool validDate = dateOfBirth > DateTime.MinValue;

            if (!validname && !validDate && !validEmail)
            {
                return result;
            }
            if (validname)
            {
                result = result.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();
            }
            if (validDate)
            {
                result = result.Where(x => x.DateOfBirth.Year == dateOfBirth.Value.Year).ToList();
            }
            if (validEmail)
            {
                result = result.Where(x => x.Email.ToLower().Contains(email.ToLower())).ToList();
            }
            return result;
        }
        #endregion

    }
}

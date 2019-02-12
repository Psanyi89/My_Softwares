using Faker;
using FizzWare.NBuilder;
using PersonEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayerLogic.Managers
{
    internal class CommonPersonManager
    {
        #region Generate a number of ICollection<IPerson>
        /// <summary>
        /// Generates a ranmdom ICollection<IPerson> (using Faker and NBuilder)
        /// </summary>
        /// <typeparam name="T">Type IPerson type</typeparam>
        /// <param name="limit">number of record in the ICollection</param>
        /// <returns>Returns a number of IPerson type objects in a ICollection</returns>
        public static ICollection<T> GenerateIPersonCollection<T>(int limit) where T : IPerson
        {
            ICollection<T> people = Builder<T>.CreateListOfSize(limit).All().
               With(x => x.Name = Name.FullName())
               .With(x => x.Email = Internet.Email())
               .With(x => x.DateOfBirth = DateTime.Today.AddDays(-RandomNumber.Next(5000, 30000)).Date)
               .Build();
            return people;
        }
        #endregion

        #region Create new IPerson
        /// <summary>
        /// Adds an IPerson object to an ICollection<IPerson>
        /// </summary>
        /// <typeparam name="T">IPerson inherited class</typeparam>
        /// <param name="person">IPerson object</param>
        /// <param name="people">ICollection<IPerson></param>
        /// <returns>Returns an IPerson object</returns>
        public static T CommonAddPerson<T>(T person, ICollection<T> people, ref bool wasnull) where T : IPerson, new()
        {
            if (EqualityComparer<T>.Default.Equals(person, default(T)) || !(person.DateOfBirth > DateTime.MinValue && person.DateOfBirth < DateTime.MaxValue))
            {
                throw new ArgumentNullException("Person cannot be null");
            }

            T addedPerson = new T();
            if (person.Id == 0)
            {
                addedPerson.Id = people.Count == 0 ? 1 : people.Max(x=>x.Id) + 1;
                wasnull = true;
            }
            else
            {
                addedPerson.Id = person.Id;
            }
            if (!string.IsNullOrWhiteSpace(person.Name))
            {
                addedPerson.Name = person.Name;
            }
            if (!string.IsNullOrWhiteSpace(person.Email))
            {
                addedPerson.Email = person.Email;
            }
            addedPerson.DateOfBirth = person.DateOfBirth;
            people.Add(addedPerson);
            return addedPerson;
        }

        #endregion

        #region Update IPerson

        /// <summary>
        /// Update selected IPerson object properties except id
        /// </summary>
        /// <typeparam name="T">IPerson type object type</typeparam>
        /// <param name="person">IPerson object</param>
        /// <param name="people">ICollection<IPerson></param>
        /// <returns> returns an IPerson object</returns>
        public static T CommonUpdatePerson<T>(T person, ICollection<T> people) where T : IPerson
        {
            if (EqualityComparer<T>.Default.Equals(person, default(T)) || !(person.DateOfBirth > DateTime.MinValue && person.DateOfBirth < DateTime.MaxValue))
            {
                throw new ArgumentNullException("Person cannot be null");
            }

            T updatedPerson = people.FirstOrDefault(x => x.Id == person.Id);
            if (EqualityComparer<T>.Default.Equals(updatedPerson, default(T)))
            {
                throw new ArgumentNullException("Selected person is not on the list");

            }
            if (!string.IsNullOrWhiteSpace(person.Name))
            {
                updatedPerson.Name = person.Name;
            }
            if (!string.IsNullOrWhiteSpace(person.Email))
            {
                updatedPerson.Email = person.Email;
            }
            if (person.DateOfBirth > DateTime.MinValue && person.DateOfBirth <= DateTime.Today)
            {
                updatedPerson.DateOfBirth = person.DateOfBirth;
            }
            return updatedPerson;
        }
        #endregion

        #region Delete IPerson
        public static bool CommonDeleteIPerson<T>(T person, ICollection<T> people) where T : IPerson
        {
            if (EqualityComparer<T>.Default.Equals(person, default(T)) || !(person.DateOfBirth < DateTime.MaxValue && person.DateOfBirth > DateTime.MinValue))
            {
                throw new ArgumentNullException("Empty Person model is not accepted.");
            }
            return people.Remove(person);
        }
        #endregion

        #region Search in ICollection<IPerson>
        /// <summary>
        /// Search for matching parameters in ICollection<IPerson> 
        /// </summary>
        /// <typeparam name="T"> IPerson type of type</typeparam>
        /// <param name="people">ICollection of IPerson type </param>
        /// <param name="name">(Optional)Searched name / fragment of a name</param>
        /// <param name="dateOfBirth">(Optional)Searched date</param>
        /// <param name="email">(Optional)Searched email / framgment of an email</param>
        /// <returns>Returns an ICollection<IPerson> whith the records what passed the filter</returns>
        public static ICollection<T> CommonSearch<T>(ICollection<T> people, string name = null, DateTime? dateOfBirth = null, string email = null) where T : IPerson
        {
            if (people.Count() == 0)
            {
                return null;
            }

            bool isValidName = !string.IsNullOrWhiteSpace(name);
            bool isValidDate = dateOfBirth > DateTime.MinValue && dateOfBirth < DateTime.MaxValue;
            bool isValidEmail = !string.IsNullOrWhiteSpace(email);
            if (!isValidName && !isValidDate && !isValidEmail)
            {
                return people;
            }
            if (isValidName)
            {
                people = people.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToCollection();
            }
            if (isValidDate)
            {
                people = people.Where(x => x.DateOfBirth.Year == dateOfBirth.Value.Year).ToCollection();
            }
            if (isValidEmail)
            {
                people = people.Where(x => x.Email.ToLower().Contains(email.ToLower())).ToCollection();
            }
            return people;
        }
        #endregion
    }
}

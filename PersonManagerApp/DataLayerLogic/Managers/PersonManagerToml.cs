using Nett;
using PersonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerLogic.Managers
{
    class PersonManagerToml: IPersonManager
    {
        private readonly List<Person> TomlDataBase = new List<Person>();
        private readonly string filePath = "FakeDB";
      
        #region TomlDataBase Constructor
        /// <summary>
        /// Toml database constructor
        /// </summary>
        public PersonManagerToml()
        {
            var people = Toml.ReadFile<People>($"{filePath}{Toml.FileExtension}");
          
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
          if(person!=null ||!(person.DateOfBirth>DateTime.MinValue))
            {
                bool wasnull = false;
                Person addedPerson = new Person();
                if (person.Id==null)
                {
                    addedPerson.Id = TomlDataBase.Count + 1;
                    wasnull = true;
                }
                else
                {
                    addedPerson.Id = person.Id;
                }
               if(!string.IsNullOrWhiteSpace(person.Name))
                {
                    addedPerson.Name = person.Name;
                }
               if(person.DateOfBirth!=DateTime.MinValue)
                {
                    addedPerson.DateOfBirth = person.DateOfBirth;
                }
                if (!string.IsNullOrWhiteSpace(person.Email))
                {
                    addedPerson.Email = person.Email;
                }
                TomlDataBase.Add(addedPerson);
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
            else
            {
                return null;
            }
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
            if (person ==null)
            {
                return null;
            }
            Person updatePerson = TomlDataBase.FirstOrDefault(x => x.Id == person.Id);
            if (updatePerson!=null)
            {
                if (!string.IsNullOrWhiteSpace(person.Name))
                {
                    updatePerson.Name = person.Name;
                }
                if (person.DateOfBirth!=DateTime.MinValue)
                {
                    updatePerson.DateOfBirth = person.DateOfBirth;
                }
                if (!string.IsNullOrWhiteSpace(person.Email))
                {
                    updatePerson.Email = person.Email;
                }
            }
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
            if (person == null)
            {
                return false;
            }
                if (person.Id!=null)
                {
                    int count = TomlDataBase.RemoveAll(x => x.Id == person.Id);
                    People people = new People
                    {
                        Persons = TomlDataBase
                    };
                    people.CreateToml(filePath);
                    return count > 0;
                }
            return false;
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
            if(validDate)
            {
                result = result.Where(x => x.DateOfBirth.Year == dateOfBirth.Value.Year).ToList();
            }
            if(validEmail)
            {
                result = result.Where(x => x.Email.ToLower().Contains(email.ToLower())).ToList();
            }
            return result;
        }
        #endregion

    }
}

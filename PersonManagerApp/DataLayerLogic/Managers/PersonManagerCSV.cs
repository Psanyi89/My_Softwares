using CsvHelper;
using PersonEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataLayerLogic.Managers
{
    public class PersonManagerCSV : IPersonManager
    {
        private readonly string filePath = "FakeDB.csv";
        private readonly List<Person> CSVDataBase = new List<Person>();

        #region Constructor
        /// <summary>
        /// PersonManagerCSV Constructor
        /// </summary>
        public PersonManagerCSV()
        {

            using (StreamReader reader = new StreamReader(filePath))
            using (CsvReader cs = new CsvReader(reader))
            {
                IEnumerable<Person> people = cs.GetRecords<Person>();
                foreach (Person item in people)
                {
                    AddPerson(item);
                }
            }
        }
        #endregion

        #region Create new Person
        /// <summary>
        /// Add a person to the CSV database
        /// </summary>
        /// <param name="person">person object</param>
        /// <returns></returns>
        public Person AddPerson(Person person)
        {
            if (person != null)
            {
                bool wasnull = false;
                Person addedPerson = new Person();
                if (person.Id == null)
                {
                    addedPerson.Id = CSVDataBase.Last().Id + 1;
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
                if (person.DateOfBirth != null)
                {
                    addedPerson.DateOfBirth = person.DateOfBirth;
                }

                if (!string.IsNullOrWhiteSpace(person.Email))
                {
                    addedPerson.Email = person.Email;
                }

                CSVDataBase.Add(addedPerson);

                if (wasnull)
                {
                    WriteDb();
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
        /// returns the people stored in the CSV database
        /// </summary>
        /// <returns></returns>
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
        /// <returns></returns>
        public Person UpdatePerson(Person person)
        {
            Person selectedPerson = CSVDataBase.FirstOrDefault(x => x.Id == person.Id);
            if (selectedPerson != null)
            {
                if (!string.IsNullOrWhiteSpace(person.Name))
                {
                    selectedPerson.Name = person.Name;
                }
                if (person.DateOfBirth != null)
                {
                    selectedPerson.DateOfBirth = person.DateOfBirth;
                }

                if (!string.IsNullOrWhiteSpace(person.Email))
                {
                    selectedPerson.Email = person.Email;
                }
                WriteDb();
                return selectedPerson;
            }
            else
            {
                return null;
            }
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
            if (person != null)
            {
                int count = CSVDataBase.RemoveAll(x => x.Id == person.Id);
                WriteDb();
                return count > 0;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Refresh CSV Database
        /// <summary>
        /// Writes CSVdatabase to CSV
        /// </summary>
        private void WriteDb()
        {
            using (TextWriter writer = new StreamWriter(filePath))
            using (CsvWriter csv = new CsvWriter(writer))
            {
                csv.Configuration.Delimiter = ";";
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.AutoMap<Person>();
                csv.WriteHeader<Person>();
                csv.NextRecord();
                csv.WriteRecords(CSVDataBase);
                writer.Flush();

            }
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

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
        private readonly string filePath = "FakeDB.txt";

        #region Constructor
        /// <summary>
        /// TxtPersonManager Constructor
        /// </summary>
        public TxtPersonManager()
        {
            IEnumerable<string> temp = File.ReadLines(filePath).Skip(1);
            IEnumerable<Person> people = from lines in temp
                                         let splitlines = lines.Split(',')
                                         where splitlines.Length == typeof(Person).GetProperties().Count()
                                         select new Person
                                         {
                                             Id = Convert.ToInt32(splitlines[0]),
                                             Name = splitlines[1],
                                             DateOfBirth = Convert.ToDateTime(splitlines[2]).Date,
                                             Email = splitlines[3]
                                         };
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
            Person addedPerson = new Person();

            if (person.Id == null)
            {
                addedPerson.Id = TxtDBdatabase.Last().Id + 1;
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

            TxtDBdatabase.Add(addedPerson);
            if (wasnull == true)
            {
                WriteDB();
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
            Person updatedperson = TxtDBdatabase.FirstOrDefault(x => x.Id == person.Id);
            if (updatedperson != null)
            {
                if (!string.IsNullOrWhiteSpace(person.Name))
                {
                    updatedperson.Name = person.Name;
                }
                if (person.DateOfBirth != null)
                {
                    updatedperson.DateOfBirth = person.DateOfBirth;

                }
                if (!string.IsNullOrWhiteSpace(person.Email))
                {

                    updatedperson.Email = person.Email;
                }
                WriteDB();
                return updatedperson;

            }
            return null;

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
            if (person == null)
            {
                return false;
            }
            int resutl = TxtDBdatabase.RemoveAll(x => x.Id == person.Id);
            WriteDB();
            return resutl > 0;
        }
        #endregion

        #region Refresh Txt Database
        /// <summary>
        /// Writes Txtdatabase to Txt
        /// </summary>
        public void WriteDB()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                string header = string.Empty;
                int i = 0;
                foreach (System.Reflection.PropertyInfo item in typeof(Person).GetProperties())
                {
                    if (i > 0 && i < typeof(Person).GetProperties().Count())
                    {
                        header += ",";
                    }
                    header += item.Name;
                    i++;
                }
                writer.WriteLine(header);
                foreach (Person item in TxtDBdatabase)
                {
                    writer.WriteLine($"{item.ToString()}");
                }
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

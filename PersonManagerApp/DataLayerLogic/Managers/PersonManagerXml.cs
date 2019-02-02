using PersonEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DataLayerLogic.Managers
{
    internal class PersonManagerXml : IPersonManager
    {
        private readonly List<Person> xmlDatabase = new List<Person>();
        private readonly string filePath = "FakeDB.xml";
     
        #region Constructor
        /// <summary>
        /// PersonManagerXml Costructor
        /// </summary>
        public PersonManagerXml()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Person>));

            using (StreamReader reader = new StreamReader(filePath))
            {
                List<Person> input = (List<Person>)serializer.Deserialize(reader);
                input.ForEach(x => AddPerson(x));
            }
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
            if (person != null)
            {
                bool wasnull = false;
                Person addedPerson = new Person();
                if (person.Id == null)
                {
                    addedPerson.Id = xmlDatabase.Last().Id + 1;
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

                addedPerson.DateOfBirth = person.DateOfBirth;

                if (!string.IsNullOrWhiteSpace(person.Email))
                {
                    addedPerson.Email = person.Email;
                }
                xmlDatabase.Add(addedPerson);
                if (wasnull == true)
                {
                    XDocument doc = XDocument.Load(filePath);
                    XElement xElement = new XElement("Person",
                        new XElement("Id", addedPerson.Id),
                        new XElement("Name", addedPerson.Name),
                        new XElement("DateOfBirth", addedPerson.DateOfBirth),
                        new XElement("Email", addedPerson.Email)
                        );
                    doc.Root.Add(xElement);
                    doc.Save(filePath);
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
            return new List<Person>(xmlDatabase);
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
            if (person == null)
            {
                return null;
            }
            else
            {
                if (person.Id == null)
                {
                    return null;
                }
                else
                {
                    Person updatedperson = xmlDatabase.FirstOrDefault(x => x.Id == person.Id);
                    if (updatedperson != null)
                    {
                        XDocument doc = XDocument.Load(filePath);
                        XElement update = doc.Descendants("Person").
                            Where(x =>
                            x.Element("Id").Value.Equals(person.Id.ToString())).
                            First();
                        if (!string.IsNullOrWhiteSpace(person.Name))
                        {
                            updatedperson.Name = person.Name;
                            update.SetElementValue("Name", person.Name);
                        }
                        if (person.DateOfBirth != null)
                        {
                            updatedperson.DateOfBirth = person.DateOfBirth;
                            update.SetElementValue("DateOfBirth", person.DateOfBirth);
                        }
                        if (!string.IsNullOrWhiteSpace(person.Email))
                        {
                            updatedperson.Email = person.Email;
                            update.SetElementValue("Email", person.Email);
                        }
                        doc.Save(filePath);
                    }
                    return updatedperson;
                }
            }
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
            if (person != null)
            {
                int count = 0;
                if (person.Id != null)
                {
                    XDocument doc = XDocument.Load(filePath);
                    XElement remove = doc.Descendants("Person").
                        Where(x => x.Element("Id").Value.Equals(person.Id.ToString())).
                        First();
                    remove.Remove();
                    doc.Save(filePath);
                    count = xmlDatabase.RemoveAll(x => x.Id == person.Id);

                }
                return count > 0;
            }
            else
            {
                return false;
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
            bool validname = !string.IsNullOrWhiteSpace(name);
            bool validEmail = !string.IsNullOrWhiteSpace(email);
            bool validDate = dateOfBirth > DateTime.MinValue;
            XDocument doc = XDocument.Load(filePath);
            IEnumerable<XElement> query = doc.Descendants("Person").ToList();

            if (validname)
            {
                query = query.Where(x => x.Element("Name").Value.ToLower().Contains(name.ToLower())).ToList();
            }
            if (validDate)
            {
                query = query.Where(x => Convert.ToDateTime(x.Element("DateOfBirth").Value).Year == dateOfBirth.Value.Year).ToList();
            }
            if (validEmail)
            {
                query = query.Where(x => x.Element("Email").Value.ToLower().Contains(email.ToLower()));
            }
            XmlSerializer serializer = new XmlSerializer(typeof(Person));
            List<Person> result = new List<Person>();
            foreach (XElement item in query)
            {
                result.Add((Person)serializer.Deserialize(item.CreateReader()));
            }

            return result;

        }
        #endregion
    }
}

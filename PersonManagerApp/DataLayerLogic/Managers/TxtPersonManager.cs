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
        public TxtPersonManager()
        {
            IEnumerable<string> temp = File.ReadLines("FakeDB.txt").Skip(1);
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
            addedPerson.Name = person.Name;
            addedPerson.DateOfBirth = person.DateOfBirth;
            addedPerson.Email = person.Email;
            TxtDBdatabase.Add(addedPerson);
            if(wasnull==true)
            {
                WriteDB();
            }

            return addedPerson;
        }

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

        public List<Person> GetPersons()
        {
            return new List<Person>(TxtDBdatabase);

        }

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
        public void WriteDB()
        {
            using (StreamWriter writer = new StreamWriter("FakeDB.txt"))
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
    }

}

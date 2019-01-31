using Faker;
using FizzWare.NBuilder;
using MongoDB.Driver;
using Newtonsoft.Json;
using PersonEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace DataLayerLogic.Managers
{
    internal class PersonManagerFakeDB : IPersonManager
    {
        private int _id = 1;
        private readonly List<Person> _personFakeDB = new List<Person>();
        public PersonManagerFakeDB()
        {
            #region  Create FakeDB in Memory using NBuilder and Faker.Net

            IList<Person> people = Builder<Person>.CreateListOfSize(100).All().
                With(x => x.Name = Name.FullName())
                .With(x => x.Email = Internet.Email())
                .With(x => x.DateOfBirth = DateTime.Today.AddDays(-RandomNumber.Next(5000, 30000)).Date)
                .Build();
            #endregion
            #region Create and write XML
            XDocument xmlDocument = new XDocument(
                 new XDeclaration("1.0", "utf-8", "yes"),
                 new XComment("People Fake DataBase"),
                 new XElement("People",

                people.Select(person => new XElement("Person", new XAttribute("Id", person.Id),
                new XElement("Name", person.Name),
                new XElement("DateOfBirth", person.DateOfBirth.ToShortDateString()),
                new XElement("Email", person.Email)
                 ))));
            // xmlDocument.Save("FakeDB.xml");

            #endregion
            #region Create and Write CSV
            //using (TextWriter writer = new StreamWriter(@"FakeDB.csv"))
            //using (var csv = new CsvWriter(writer))
            //{
            //    csv.Configuration.Delimiter = ";";
            //    csv.Configuration.HasHeaderRecord = true;
            //    csv.Configuration.AutoMap<Person>();
            //    csv.WriteHeader<Person>();
            //    csv.NextRecord();
            //    csv.WriteRecords(people);
            //    writer.Flush();

            //}
            #endregion
            #region Create and Write TXT
            //using (StreamWriter writer = new StreamWriter("FakeDB.txt"))
            //{
            //    string header = string.Empty;
            //        int i = 0;
            //    foreach (var item in typeof(Person).GetProperties())
            //    {
            //        if (i>0 && i< typeof(Person).GetProperties().Count())
            //        {
            //            header += ",";
            //        }
            //        header +=item.Name;
            //        i++;
            //    }
            //    writer.WriteLine(header);
            //    foreach (var item in people)
            //    {
            //        writer.WriteLine($"{item.Id},{item.Name},{item.DateOfBirth.ToShortDateString()},{item.Email}");
            //    }
            //}
            #endregion
            #region Create and Write Json
            string jsonresult1 = JsonConvert.SerializeObject(people);
            // File.WriteAllText("FakeDB.min.json", jsonresult1);
            string jsonresult = JsonConvert.SerializeObject(people, Formatting.Indented);
            // File.WriteAllText("FakeDB.json", jsonresult);
            #endregion
            #region Create and populate MongoDB
            //try
            //{
            //    string connectionstring = ConfigurationManager.AppSettings["AtlasMongo"];
            //    MongoClient client = new MongoClient(connectionstring);
            //    IMongoDatabase db = client.GetDatabase(ConfigurationManager.AppSettings["AtlasMongodatabase"]);
            //    IMongoCollection<Person> collection = db.GetCollection<Person>(ConfigurationManager.AppSettings["AtlasMongocollection"]);

            //    collection.InsertMany(people);

            //}
            //catch (Exception e)
            //{

            //    Console.WriteLine(e.Message);
            //}
            #endregion
            #region Connect to MS SQL and insert data
            //using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Somee"].ConnectionString))
            //{
            //    try
            //    {

            //        var result = connection.Execute(@"dbo.uspInsertPerson @Name, @DateOfBirth, @Email", people);
            //    }
            //    catch (Exception w)
            //    {

            //        Console.WriteLine(w.Message);
            //    }

            //}
            #endregion
            #region Connect to MYSQL and insert data
            //using (IDbConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["GearHostMySql"].ConnectionString))
            //{
            //    try
            //    {

            //        int result = connection.Execute($@"call {ConfigurationManager.AppSettings["GearhostMysqlDB"]}.`dbo.uspInsertPerson`( @Name, @DateOfBirth, @Email);", people);
            //    }
            //    catch (Exception w)
            //    {

            //        Console.WriteLine(w.Message);
            //    }

            //}
            #endregion
            #region Connect to PostgreSql and populate table
            //try
            //{

            //    using (IDbConnection connection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["ElephantPostgreSql"].ConnectionString))
            //    {

            //      int result = connection.Execute($"Select \"MySchema\".\"dbo_uspInsertPerson\"( @Name, @DateOfBirth, @Email)", people);
            //        if (result>0)
            //        {
            //            Console.WriteLine("Success!");
            //        }
            //        else
            //        {
            //            Console.WriteLine("Insertion Failed!");
            //        }
            //    }
            //}
            //catch (Exception e)
            //{

            //    Console.WriteLine(e.Message);
            //}
            #endregion
            #region Create and write TOML

            //var table = Toml.Create();
            //foreach (var item in people)
            //{

            //table.Add($"Person{item.Id}",item);
            //}
            //Toml.WriteFile(table, $"FakeDB{Toml.FileExtension}");

            #endregion
            foreach (Person item in people.ToArray())
            {
                AddPerson(item);

            }
        }
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

        public List<Person> GetPersons()
        {
            return new List<Person>(_personFakeDB);
        }

        public bool DeletePerson(Person p)
        {
            if (p == null)
            {
                throw new InvalidDataException("Person cannot be null");
            }
            return _personFakeDB.RemoveAll(x => x.Id == p.Id) > 0;

        }

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


    }
}

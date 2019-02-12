using CsvHelper;
using MongoDB.Driver;
using Nett;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
namespace DataLayerLogic
{
    public static class CreateFileTypes
    {

        #region Create and write XML
        /// <summary>
        /// This method creates an Xml file from the chosen type of collection
        /// </summary>
        /// <typeparam name="T">Sets the type of your list</typeparam>
        /// <param name="Collection">your list of objects</param>
        /// <param name="filePath"> file path, filename and extension</param>
        public static void CreatXML<T>(this ICollection<T> Collection, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ICollection<T>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, Collection);
            }
        }


        #endregion

        #region Create and Write CSV
        /// <summary>
        /// This method creates a CSV file from the chosen type of collection
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="Collection">your collection of objects</param>
        /// <param name="filePath">file path, name and extension</param>
        /// <param name="delimiter">delimitering character in string format</param>
        /// <param name="hasHeader">with header row or not</param>
        public static void CreateCSV<T>(this ICollection<T> Collection, string filePath, string delimiter = ";", bool hasHeader = true)
        {

            using (TextWriter writer = new StreamWriter($"{filePath}"))
            using (CsvWriter csv = new CsvWriter(writer))
            {
                csv.Configuration.Delimiter = delimiter;
                csv.Configuration.HasHeaderRecord = hasHeader;
                csv.Configuration.AutoMap<T>();
                if (hasHeader)
                {
                    csv.WriteHeader<T>();
                    csv.NextRecord();
                }
                csv.WriteRecords(Collection);
                writer.Flush();

            }
        }
        #endregion

        #region Create and Write TXT
        /// <summary>
        /// This method creates a Txt file from the chosen type of collection
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="Collection">your collection of objects</param>
        /// <param name="filePath">filepath with filename and extension</param>
        public static void CreateTxt<T>(this ICollection<T> Collection, string filePath, char delimiter = ',', bool headerrow = true)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                if (headerrow)
                {
                    string header = string.Empty;
                    int i = 0;
                    foreach (System.Reflection.PropertyInfo item in typeof(T).GetProperties())
                    {
                        if (i > 0 && i < typeof(T).GetProperties().Count())
                        {
                            header += delimiter;
                        }
                        header += item.Name;
                        i++;
                    }
                    writer.WriteLine(header);

                }
                foreach (T item in Collection)
                {
                    string text = string.Empty;
                    int j = 0;
                    foreach (System.Reflection.PropertyInfo attribute in item.GetType().GetProperties())
                    {

                        if (j > 0 && j < item.GetType().GetProperties().Count())
                        {
                            text += delimiter;
                        }
                        text += attribute.GetValue(item).ToString();
                        j++;
                    }

                    writer.WriteLine(text);
                }
            }
        }


        #endregion

        #region Create and Write Json
        /// <summary>
        /// This method creates a Json file from the collection and you can 
        /// choose from formated and minified version
        /// </summary>
        /// <typeparam name="T">Type of objects</typeparam>
        /// <param name="collection">Name of your collection</param>
        /// <param name="filePath">filepath with file name</param>
        /// <param name="minify">minify or not the result</param>
        public static void CreateJson<T>(this ICollection<T> collection, string filePath, bool minify = false)
        {
            if (minify)
            {
                string[] temp = filePath.Split('.');
                string jsonresult1 = JsonConvert.SerializeObject(collection);
                File.WriteAllText($"{temp[0]}.min.json", jsonresult1);
            }
            else
            {
                string jsonresult = JsonConvert.SerializeObject(collection, Formatting.Indented);
                File.WriteAllText(filePath, jsonresult);
            }

        }

        #endregion

        #region Create and write TOML
        /// <summary>
        /// This method creates a Toml file from the chosen Object
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="collection">object to printout</param>
        /// <param name="filePath">filepath with file name</param>
        public static void CreateToml<T>(this T collection, string filePath)
        {
            TomlTable table = Toml.Create(collection);
            Toml.WriteFile(table, $"{filePath}{Toml.FileExtension}");
        }

        #endregion

        #region Create and Write Binary file
        /// <summary>
        /// Serializies the object and write it to a binary file
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="objectToWrite">the object what will be serialized and written to the binary file</param>
        /// <param name="filePath">file path, name and extension</param>
        /// <param name="append">Create a new file or add content to an extisting one</param>
        public static void CreateBinarty<T>(this T objectToWrite, string filePath, bool append = false)
        {
            using (Stream stream= File.Open(filePath,append? FileMode.Append :FileMode.Create))
            {
                BinaryFormatter binaryformatter = new BinaryFormatter();
                binaryformatter.Serialize(stream, objectToWrite);
            }
        }
        #endregion

        #region Create and populate MongoDB
        /// <summary>
        /// connect to MongoDB database
        /// </summary>
        /// <typeparam name="T">Type of Object to work with</typeparam>
        /// <param name="classObject">collection to use for task</param>
        /// <param name="constringname">Connectionstring name from Appconfig</param>
        /// <param name="databasename">Database name stored in Appconfig</param>
        /// <param name="collectionname">Collection name stored in AppConfig</param>
        public static void ConnectToMongoDB<T>(this ICollection<T> classObject,
                                                         string constringname,
                                                         string databasename,
                                                         string collectionname)
        {
            MongoClient client = new MongoClient(ConfigurationManager.AppSettings[$"{constringname}"]);
            IMongoDatabase db = client.GetDatabase(ConfigurationManager.AppSettings[$"{databasename}"]);
            IMongoCollection<T> collection = db.GetCollection<T>(ConfigurationManager.AppSettings[$"{collectionname}"]);

            collection.InsertMany(classObject);
        }

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

    }
}

using CsvHelper;
using Nett;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
namespace DataLayerLogic
{
   public static class ReadFileTypes
    {
        #region Read txt file to a ICollection<T>
         /// <summary>
         /// Read and convert a Txt file to a List<T> 
         /// If txt file doesn't have header row then the order of the class properties will be followed
         /// so beaware of this.
         /// </summary>
         /// <typeparam name="T">Must be a class</typeparam>
         /// <param name="filePath">filepath, name and extension</param>
         /// <param name="delimiter">delimitering character</param>
         /// <param name="headerrow">does it have a header row</param>
         /// <returns>Returns a List<T> </returns>
        public static ICollection<T> ReadTxtFileToList<T>(string filePath,
                                                   bool headerrow=true,
                                                   char delimiter = ',') where T: class, new()
        {
            var temp = File.ReadLines(filePath).ToList();
            if (temp.Count < 1)
            {
               throw new FileNotFoundException("File not found or missing");
            }
            T entry = new T();
            ICollection<T> output= new Collection<T>();
            var columns = entry.GetType().GetProperties();
            string[] headers=new string[columns.Length];
            if(headerrow)
            {
            if (temp.Count<2)
            {
                throw new IndexOutOfRangeException("Empty file or just the header row");
            }
                 headers = temp[0].Split(delimiter);
                temp.RemoveAt(0);
            }
            else
            {
                 headers = columns.ToList().Select(x => x.Name).ToArray();
            }
            foreach (var lines in temp)
            {
                entry = new T();
                var value = lines.Split(delimiter);
                for (int i = 0; i < headers.Length; i++)
                {
                    foreach ( var column in columns)
                    {
                        if (column.Name== headers[i])
                        {
                            Type t = Nullable.GetUnderlyingType(column.PropertyType) ?? column.PropertyType;
                            object safeValue = (value[i] == null) ? null : Convert.ChangeType(value[i], t);
                            column.SetValue(entry,safeValue);
                        }
                    }
                }
                output.Add(entry);
            }
            return output.Count>0 ?output :throw new Exception
                ("File data structure not identical to class's.");
        }
        #endregion

        #region Read CSV to a ICollection<T>
        /// <summary>
        ///  Reads and convert you CSV file to the chosen class object
        /// if it's possible
        /// </summary>
        /// <typeparam name="T">class type</typeparam>
        /// <param name="filePath">filepath, name and extesnsion</param>
        /// <param name="delimiter"> delimiter character string format</param>
        /// <param name="hasHeader">CSV file does it have a header</param>
        /// <returns>Returns a ICollection<T></returns>
        public static ICollection<T> ReadCSVToList<T>(string filePath, string delimiter=";",bool hasHeader=true) where T : class
        {
            using (StreamReader reader = new StreamReader(filePath))
            using (CsvReader cs = new CsvReader(reader))
            {
                cs.Configuration.Delimiter = delimiter;
                cs.Configuration.HasHeaderRecord = hasHeader;
                ICollection<T> people = cs.GetRecords<T>().ToList();
                return people;
            }
        }
        #endregion

        #region Read Xml to a ICollection<T>
        /// <summary>
        /// eads and convert you Xml file to the chosen class object
        /// if it's possible
        /// </summary>
        /// <typeparam name="T">Class Type</typeparam>
        /// <param name="filePath">file path, name and extension</param>
        /// <returns>Returns an ICollection<T></returns>
        public static ICollection<T> ReadXmlToList<T>(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Collection<T>));

            using (StreamReader reader = new StreamReader(filePath))
            {
                ICollection<T> input = (ICollection<T>)serializer.Deserialize(reader);
                return input;
            }
        }
        #endregion

        #region Toml to T object
        /// <summary>
        /// Reads Toml file to an Class object
        /// </summary>
        /// <typeparam name="T">Class Type</typeparam>
        /// <param name="filePath">file path and name</param>
        /// <returns>Returns an object</returns>
        public static T ReadTomlToList<T>( string filePath) where T : class, new()
        {
            var result = Toml.ReadFile<T>($"{filePath}{Toml.FileExtension}");

            return result ?? throw new FileLoadException("File cannot be found or Empty");
        }
        #endregion

        #region Json to ICollection<T>
        public static ICollection<T> ReadJsonToList<T>(string filePath)
        {
            string jsonText = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<ICollection<T>>(jsonText) ?? throw new FileLoadException
                ("File cannot be found or empty, or structures is not identical to the refered class");
            
        }
        #endregion

        #region Binary file to ICollection<T>
        /// <summary>
        /// Deserializes a binary file to the T type of object
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="filePath">file path, name and extension</param>
        /// <returns>REturns the T type of result</returns>
        public static T ReadBinaryFile<T>(string filePath)
        {
            using (Stream stream= File.Open(filePath,FileMode.Open))
            {
                BinaryFormatter binaryFormatter =new BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }
        #endregion
    }
}

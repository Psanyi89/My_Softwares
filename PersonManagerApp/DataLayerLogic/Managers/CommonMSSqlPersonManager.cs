using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonEntities;
using static DataLayerLogic.Managers.MsSQLIPersonProcessor;
namespace DataLayerLogic.Managers
{
    internal class CommonMSSqlPersonManager 
    {

        #region Constant default parameter for connectionstring
        /// <summary>
        /// default connectionstring name for optional parameters
        /// </summary>
        private const string ConnName = "LocalDB";
        #endregion
        
        #region Add IPerson to collection and/or to database
        /// <summary>
        /// Adds IPerson to collection or if its a new person adds it to the database too
        /// </summary>
        /// <typeparam name="T">Type of IPerson</typeparam>
        /// <param name="person">IPerson object</param>
        /// <param name="collection">ICollection<IPerson></param>
        /// <returns>Returns an ICollection<IPerson></returns>
        public static T SqlCommonAddIPerson<T>(T person,string connectionString=ConnName) where T: IPerson, new()
        {
            bool wasnull = false;
            if (EqualityComparer<T>.Default.Equals(person,default(T))||!(person.DateOfBirth>DateTime.MinValue && person.DateOfBirth<DateTime.MaxValue))
            {
                throw new ArgumentNullException("All fields must be set.");
            }
            T addedPerson = new T();
            if (person.Id==0)
            {
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
            if (wasnull)
            {
                addedPerson.Id = InsertPerson(addedPerson,connectionString);
            }
         
            return addedPerson;
        }

        #endregion

        #region Populates ICollection<IPerson> from database
        /// <summary>
        /// Retrieves ICollection<IPerson> from database
        /// </summary>
        /// <typeparam name="T">Type of IPerson</typeparam>
        /// <returns>Returns an ICollection<IPerson></returns>
        public static ICollection<T> SqlCommonGetPersons<T>(string connectionString=ConnName)where T: IPerson
        {
            return LoadIPerson<T>(connectionString);
        }
        #endregion


        public static T SqlCommonUpdatePerson<T>(T person, string connectionString=ConnName)where T:IPerson
        {
          return UpdateIPerson(person, connectionString)>0 ?person :throw new NullReferenceException("Person not extists");
          
        }

        public static bool SqlCommonDeletePerson<T>(T person,string connectionString=ConnName) where T: IPerson, new()
        {
           return DeleteIPerson(person,connectionString)>0? true:false;
        }

        public static ICollection<T> SqlCommonSearchResult<T>(string name = null, DateTime? dateOfBirth = null, string email = null, string connectionString = ConnName) where T : IPerson
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                name = null;
            }
            if (!(dateOfBirth > DateTime.MinValue && dateOfBirth < DateTime.MaxValue))
            {
                dateOfBirth = null;
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                email = null;
            }
            return IPersonSearch<T>(connectionString, name, dateOfBirth, email);
        }

    }
}

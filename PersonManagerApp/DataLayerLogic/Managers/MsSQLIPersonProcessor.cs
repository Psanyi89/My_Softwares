using Dapper;
using PersonEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static DataLayerLogic.MSSqlAccess;

namespace DataLayerLogic.Managers
{
    internal class MsSQLIPersonProcessor
    {

        #region Inserts IPerson object to Sql database
        /// <summary>
        /// Inserts IPerson object to Sql database and returns its Id
        /// </summary>
        /// <typeparam name="T">IPerson type of object</typeparam>
        /// <param name="person">IPerson object</param>
        /// <param name="connectionString">name of connectionstring in AppConfig</param>
        /// <returns>Returns the Id of the newly added person</returns>
        public static int InsertPerson<T>(T person, string connectionString) where T : IPerson
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@PersonId", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Name", person.Name);
            parameters.Add("@DateOfBirth", person.DateOfBirth);
            parameters.Add("@Email", person.Email);
            string sql = @"dbo.uspInsertPerson";
            ExecuteSql(sql, parameters, connectionString);
            int addedPersonId = parameters.Get<int>("@PersonId");
            return addedPersonId;


        }
        #endregion

        #region Retrieves all IPerson from the Sql database
        /// <summary>
        /// Retrieves all IPerson from the database
        /// </summary>
        /// <typeparam name="T">IPerson type object</typeparam>
        /// <param name="connectionString">name of connectionstring in AppConfig</param>
        /// <returns>Returns an ICollection<IPerson></returns>
        public static ICollection<T> LoadIPerson<T>(string connectionString) where T : IPerson
        {
            string sql = "uspSearchPeopleDB";
            return LoadData<T>(sql, connectionString);
        }
        #endregion

        #region Update IPerson's data in database
        /// <summary>
        /// Updates Person data in database
        /// </summary>
        /// <typeparam name="T">Type of IPerson</typeparam>
        /// <param name="person">IPerson object</param>
        /// <param name="connectionString">connectionstring name in AppConfig</param>
        /// <returns>Returns affected rows number</returns>
        public static int UpdateIPerson<T>(T person, string connectionString) where T : IPerson
            {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", person.Id);
            parameters.Add("@Name", person.Name);
            parameters.Add("@DateOfBirth", person.DateOfBirth);
            parameters.Add("@Email", person.Email);
            string sql = "uspUpdatePerson";
           return ExecuteSql(sql, parameters,connectionString);
        }
        #endregion

        #region Delete IPerson from database
        /// <summary>
        /// Deletes the selected IPerson by Id from database
        /// </summary>
        /// <typeparam name="T">IPerson type of object</typeparam>
        /// <param name="person">IPerson object</param>
        /// <param name="connectionString">connectstring name in AppConfig</param>
        /// <returns>Returns the number or rows were affected</returns>
        public static int DeleteIPerson<T>(T person,string connectionString)where T:IPerson
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", person.Id);
            string sql = "uspDeletePerson";
            return ExecuteSql(sql, parameters, connectionString);
        }
        #endregion

        #region Search in database filtering by Name, date of birth, email
        /// <summary>
        /// Search query by name, date of birth and email
        /// </summary>
        /// <typeparam name="T">IPerson type object</typeparam>
        /// <param name="connectionString">connectionstring name in AppConfig</param>
        /// <param name="name">name or name fragment</param>
        /// <param name="dateOfBirth">year</param>
        /// <param name="email">email or email fragment</param>
        /// <returns>Returns a list of IPerson whos matching with the filter.</returns>
        public static ICollection<T> IPersonSearch<T>(string connectionString,string name=null, DateTime? dateOfBirth=null, string email=null)where T: IPerson
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Name", name);
            parameters.Add("@DateOfBirth", dateOfBirth);
            parameters.Add("@Email", email);
            string sql = "uspSearchPeopleDB";
            return LoadData<T>(sql,connectionString ,parameters);
        }
        #endregion

    }
}

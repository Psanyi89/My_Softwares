using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataLayerLogic
{
    public class MSSqlAccess
    {

        #region Get Specific ConnectionString from AppConfig
        /// <summary>
        /// Chooses the specified ConnectionString from AppConfig file
        /// </summary>
        /// <param name="conn">Name of connection in AppConfig</param>
        /// <returns>Returns the connectionstring</returns>
        public static string GetConnectionString(string conn = "LocalDB")
        {
            return ConfigurationManager.ConnectionStrings[conn].ConnectionString;
        }
        #endregion

        #region Load a list of data from database
        /// <summary>
        /// Loads a list of data from a database
        /// </summary>
        /// <typeparam name="T">Type of the data</typeparam>
        /// <param name="sql">sql command</param>
        /// <returns>Returns the result of the sql querry</returns>
        public IEnumerable<T> LoadData<T>(string sql)
        {

            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                return connection.Query<T>(sql);
            }

        }
        #endregion

        #region Insert to database
        /// <summary>
        /// Insert data to the database
        /// </summary>
        /// <typeparam name="T">the type of data</typeparam>
        /// <param name="sql">sql command</param>
        /// <param name="data">object properties</param>
        /// <returns>Returns the number of rows were affected.</returns>
        public static int InsertData<T>(string sql, T data)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                return connection.Execute(sql, data);
            }
        }
        #endregion

        #region Single Line Query
        /// <summary>
        /// Single object or value query
        /// </summary>
        /// <typeparam name="T">type of object, variable</typeparam>
        /// <param name="sql">sql query command</param>
        /// <param name="parameters">variable numbers of parameters for the query command</param>
        /// <returns>Returns a single object or variable</returns>
        public static T GetSingle<T>(string sql, DynamicParameters parameters)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString()))
            {
                return connection.QuerySingle<T>(sql, parameters);
            }
        }
        #endregion

    }
}

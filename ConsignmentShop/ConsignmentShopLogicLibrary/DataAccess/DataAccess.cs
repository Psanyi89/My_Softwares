using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using ConsignmentShopLogicLibrary.Helpers;

namespace ConsignmentShopLogicLibrary.DataAccess
{
    public class DataAccess
    {
        #region Get connectionstring from AppConfig by Name
        /// <summary>
        /// Get connectionstring from Appconfig by name
        /// </summary>
        /// <param name="ConnectionName">name of connectionstring in AppConfig</param>
        /// <returns>returns the chosen connectionstring</returns>
        public static string GetConnectionString(string ConnectionName)
        {
            return Encrypt.DecryptString($"{ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString}", "Szeress amíg lehet");
        }
        #endregion

        #region Get IEnumerable of T type
        /// <summary>
        /// Loads a list of data from a database
        /// </summary>
        /// <typeparam name="T">Type of the data</typeparam>
        /// <param name="sql">sql command</param>
        /// <param name="connectionName">Name of connectionstring in AppConfig</param>
        /// <param name="parameters">DynamicParameter for the query</param>
        /// <returns>Returns the result of the sql querry</returns>
        public static IEnumerable<T> GetList<T>(string sql, string connectionName, DynamicParameters parameters = null)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString(connectionName)))
            {
                return connection.QueryAsync<T>(sql, parameters, commandType: CommandType.StoredProcedure).Result;
            }
        }
        #endregion

        #region  Single Line Query
        /// <summary>
        ///  Single object or value query
        /// </summary>
        /// <typeparam name="T">type of object, variable</typeparam>
        /// <param name="sql">sql query command</param>
        /// <param name="parameters">DynamicParameter for the query</param>
        /// <param name="connectionName">Name of connectionstring in AppConfig</param>
        /// <returns>Returns a single object or variable</returns>
        public static T GetSingle<T>(string sql, DynamicParameters parameters, string connectionName = null)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString(connectionName)))
            {
                return connection.QuerySingleAsync<T>(sql, parameters, commandType: CommandType.StoredProcedure).Result;
            }
        }
        #endregion

        #region Executes non-query sql commands
        /// <summary>
        /// Executes non-query sql commands
        /// </summary>
        /// <param name="sql">sql command</param>
        /// <param name="parameters">DynamicParameter for the query</param>
        /// <param name="connectionName">Name of connectionstring in AppConfig</param>
        /// <returns>returns number of rows were affected</returns>
        public static int CmdExecute(string sql, DynamicParameters parameters, string connectionName)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString(connectionName)))
            {
                return connection.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure).Result;
            }
        }
        #endregion

    }
}

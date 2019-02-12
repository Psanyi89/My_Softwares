using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataLayerLogic
{
    internal static class MSSqlAccess
    {

        #region Get Specific ConnectionString from AppConfig
        /// <summary>
        /// Chooses the specified ConnectionString from AppConfig file
        /// </summary>
        /// <param name="conn">Name of connection in AppConfig</param>
        /// <returns>Returns the connectionstring</returns>
        public static string GetConnectionString(string conn)
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
        /// <param name="connectionString">Name of connectionstring in AppConfig</param>
        /// <returns>Returns the result of the sql querry</returns>
        public static ICollection<T> LoadData<T>(string sql,string connectionString,DynamicParameters param=null)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString(connectionString)))
            {
               var result =connection.Query<T>(sql,param, commandType: CommandType.StoredProcedure).ToCollection();
                return result;
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
        /// <param name="connectionString">Name of connectionstring in AppConfig</param>
        /// <returns>Returns a single object or variable</returns>
        public static T GetSingle<T>(string sql, DynamicParameters parameters, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString(connectionString)))
            {
                return connection.QuerySingle<T>(sql, parameters, commandType: CommandType.StoredProcedure);
            }
        }
        #endregion

        #region Execute sql command with output parameters
        /// <summary>
        /// Execute sql command with the attached type of objects
        /// </summary>
        /// <param name="sql">sql command</param>
        /// <param name="person">dynamicparameters object</param>
        /// <param name="connectionString">Name of connectionstring in AppConfig</param>
        /// <returns>Returns a dynamicparameter object</returns>
        public static int ExecuteSql(string sql, DynamicParameters person, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString(connectionString)))
            {
                return connection.Execute(sql, person, commandType: CommandType.StoredProcedure);
                
            }
        }
        #endregion

    }
}

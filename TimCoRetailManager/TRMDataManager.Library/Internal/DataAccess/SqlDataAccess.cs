using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRMDataManager.Library.Internal.DataAccess
{
   internal class SqlDataAccess : IDisposable
    {
        public string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public async Task<List<T>> LoadData<T,U>(string storedProcedure, U parameters, string connectionStringName)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString(connectionStringName)))
            {
               var rows =await connection.QueryAsync<T>(storedProcedure, parameters
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                return rows.ToList();
            }
        }
        public async Task SaveData<T>(string storedProcedure, T parameters, string connectionStringName)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString(connectionStringName)))
            {
             await connection.ExecuteAsync(storedProcedure, parameters
                    , commandType: CommandType.StoredProcedure);
              
            }
        }
        public void SaveDataInTransaction<T>(string storedProcedure, T parameters)
        {
           
                 _connection.Execute(storedProcedure, parameters
                       , commandType: CommandType.StoredProcedure, transaction: _transaction);

        }
        public List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters)
        {
      
                var rows =  _connection.Query<T>(storedProcedure, parameters

                     , commandType: CommandType.StoredProcedure,transaction: _transaction);
                return rows.ToList();

        }
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        public void StartTransaction(string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction?.Commit();
            _connection?.Close();
        }

        public void RollBackTransaction()
        {
            _transaction?.Rollback();
            _connection?.Close();
        }

        public void Dispose()
        {
            CommitTransaction();
        }
    }
}

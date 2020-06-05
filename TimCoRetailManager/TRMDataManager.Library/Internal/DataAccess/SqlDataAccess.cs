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
        public async Task SaveDataInTransaction<T>(string storedProcedure, T parameters)
        {
           
                await _connection.ExecuteAsync(storedProcedure, parameters
                       , commandType: CommandType.StoredProcedure, transaction: _transaction)
                .ConfigureAwait(false);

        }
        public async Task<List<T>> LoadDataInTransaction<T, U>(string storedProcedure, U parameters)
        {
      
                var rows = await _connection.QueryAsync<T>(storedProcedure, parameters

                     , commandType: CommandType.StoredProcedure,transaction: _transaction)
                .ConfigureAwait(false);
                return rows.ToList();

        }
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private bool isClosed = false;
        public void StartTransaction(string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            
            _transaction = _connection.BeginTransaction();
            isClosed = false;
        }

        public void CommitTransaction()
        {
            _transaction?.Commit();
            _connection?.Close();
            isClosed = true;
        }

        public void RollBackTransaction()
        {
            _transaction?.Rollback();
            _connection?.Close();
            isClosed = true;
        }

        public void Dispose()
        {
            if (!isClosed)
            {
                try
                {
                    CommitTransaction();
                }
                catch 
                {

                    // TODO Log this issue
                }
            }
            _transaction = null;
            _connection = null;
        }
    }
}

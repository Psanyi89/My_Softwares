using ConsignmentShopLibrary;
using Dapper;
using System.Collections.Generic;
using static ConsignmentShopLogicLibrary.DataAccess.DataAccess;
namespace ConsignmentShopLogicLibrary.TaskProcessor
{
    public class StoreProcessor
    {
        #region Default ConnectionString name
        /// <summary>
        /// default connectionstring name
        /// </summary>
        private const string connName = "LocalDB";
        #endregion

        #region Lists or searchIStores

        /// <summary>
        /// Lists IStores from Stores table
        /// </summary>
        /// <typeparam name="T">IStores type object </typeparam>
        /// <param name="items">IStores object</param>
        /// <param name="connectionString">Name of connectionstring in Appconfig</param>
        /// <returns>returns an IEnumerables of IStores</returns>
        public static IEnumerable<T> GetStores<T>(string storeName = null, string connectionString = connName)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Name", storeName);
            return GetList<T>("uspGetStores", connectionString, param);
        }
        #endregion

        #region Inserting new IStore
        /// <summary>
        /// Inserts store to Stores table
        /// </summary>
        /// <typeparam name="T">T type of IStores</typeparam>
        /// <param name="item">IStore object</param>
        /// <param name="connectionString">name of connectionstring in AppConfig</param>
        /// <returns>returns the number of rows were affected</returns>
        public static int InsertStore(string storeName, int? itemId=null, string connectionString = connName)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@StoreName", storeName);
            param.Add("@ItemId", itemId);
            return CmdExecute("uspInsertToStore", param, connectionString);

        }
        #endregion

        #region Update item details
        /// <summary>
        /// Updates details of selected store in Stores table
        /// </summary>
        /// <typeparam name="T">T type of IStores</typeparam>
        /// <param name="item">IStores object</param>
        /// <param name="connectionString">name of the connectionstring in AppConfig</param>
        /// <returns>returns number of rows were affected</returns>
        public static int UpdateStore<T>(T item, string connectionString = connName) where T : IStore
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@StoreId", item.StoreId);
            param.Add("@StoreName", item.Name);
            return CmdExecute("uspUpdateStore", param, connectionString);
        }
        #endregion

        #region Deletes item 
        /// <summary>
        /// deletes selected store from IStores table
        /// </summary>
        /// <typeparam name="T">T type of IStores</typeparam>
        /// <param name="item">IStores object</param>
        /// <param name="connectionString">name of connectionstring in AppConfig</param>
        /// <returns>returns number of rows were affected</returns>
        public static int DeleteStore<T>(T item, string connectionString = connName) where T : IStore
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@StoreId", item.StoreId);

            return CmdExecute("uspDeleteStore", param, connectionString);
        }
        #endregion

        #region Deletes item from store
        /// <summary>
        /// 
        /// </summary>
        /// <param name="storeId">selected store id</param>
        /// <param name="itemId">selected item id</param>
        /// <param name="connectionString">name of the connectionstring in AppConfig</param>
        /// <returns>returns the number of rows were affected</returns>
        public static int DeleteItemFromStore(int storeId, int itemId, string connectionString = connName)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@StoreId", storeId);
            param.Add("@ItemId", itemId);

            return CmdExecute("uspDeleteItemFromStore", param, connectionString);
        }
        #endregion

        #region list out items in store

        /// <summary>
        /// Lists IStores from Stores table
        /// </summary>
        /// <typeparam name="T">IStores type object </typeparam>
        /// <param name="items">IStores object</param>
        /// <param name="connectionString">Name of connectionstring in Appconfig</param>
        /// <returns>returns an IEnumerables of IStores</returns>
        public static IEnumerable<T> GetItemsInStores<T>(int storeId, string connectionString = connName) where T : IItemsInStore
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@StoreId", storeId);
            return GetList<T>("uspGetItemsInStore", connectionString, param);
        }
        #endregion

        #region Generates DynamicParameters for IItem
        /// <summary>
        /// Generates DynamicParameters for IStore
        /// </summary>
        /// <typeparam name="T">T type of IsTore</typeparam>
        /// <param name="item">IStore object</param>
        /// <returns>returns a DynamicParameter array</returns>
        private static DynamicParameters CreateParam<T>(T item) where T : IStore
        {
            DynamicParameters param = new DynamicParameters();
            if (item?.Name != null)
            {
                param.Add("@Name", item.Name);
            }
            return param;
        }
        #endregion

    }
}

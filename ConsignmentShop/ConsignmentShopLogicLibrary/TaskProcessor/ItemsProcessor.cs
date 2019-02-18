using ConsignmentShopLibrary;
using static ConsignmentShopLogicLibrary.DataAccess.DataAccess;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsignmentShopLogicLibrary.TaskProcessor
{
    public class ItemsProcessor
    {
      
        #region Default ConnectionString name
        /// <summary>
        /// default connectionstring name
        /// </summary>
       private const string connName = "LocalDB";
        #endregion
    
        #region Lists or searchIItems

        /// <summary>
        /// Lists IItems from Items table
        /// </summary>
        /// <typeparam name="T">IItem type object </typeparam>
        /// <param name="items">IItem object</param>
        /// <param name="connectionString">Name of connectionstring in Appconfig</param>
        /// <returns>returns an IEnumerables of IItems</returns>
        public static IEnumerable<T> GetItems<T>(IItem items = null, string connectionString = connName) where T : IItem
        {
            DynamicParameters param = CreateParam(items);
            return GetList<T>("uspGetItems", connectionString, param);
        }
        #endregion

        #region Inserting new IItem
        /// <summary>
        /// Inserts item to Items table
        /// </summary>
        /// <typeparam name="T">T type of IItem</typeparam>
        /// <param name="item">IItem object</param>
        /// <param name="connectionString">name of connectionstring in AppConfig</param>
        /// <returns>returns the number of rows were affected</returns>
        public static int InsertItem<T>(T item, string connectionString=connName)where T:IItem
        {
            DynamicParameters param = CreateParam(item);
            return CmdExecute("uspInsertItem", param, connectionString);
            
        }
        #endregion

        #region Update item details
        /// <summary>
        /// Updates details of selected item in Items table
        /// </summary>
        /// <typeparam name="T">T type of IItem</typeparam>
        /// <param name="item">IItem object</param>
        /// <param name="connectionString">name of the connectionstring in AppConfig</param>
        /// <returns>returns number of rows were affected</returns>
        public int UpdateItem<T>(T item,string connectionString=connName)where T : IItem
        {
            DynamicParameters param = CreateParam(item);
            return CmdExecute("uspUpdateItem", param, connectionString);
        }
        #endregion

        #region Deletes item 
        /// <summary>
        /// deletes selected item from Items table
        /// </summary>
        /// <typeparam name="T">T type of IItem</typeparam>
        /// <param name="item">IItem object</param>
        /// <param name="connectionString">name of connectionstring in AppConfig</param>
        /// <returns>returns number of rows were affected</returns>
        public int DeleteItem<T>(T item,string connectionString=connName)where T : IItem
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@ItemId", item.ItemId);
            return CmdExecute("uspDeleteItem", param, connectionString);
        }
        #endregion
  
        #region Generates DynamicParameters for IItem
        /// <summary>
        /// Generates DynamicParameters for IItem
        /// </summary>
        /// <typeparam name="T">T type of IItem</typeparam>
        /// <param name="item">IItem object</param>
        /// <returns>returns a DynamicParameter array</returns>
        private static DynamicParameters CreateParam<T>(T item) where T : IItem
        {
            DynamicParameters param = new DynamicParameters();
            if (!string.IsNullOrWhiteSpace(item?.Title))
            {
                param.Add("@Title", item.Title);
            }
            if (!string.IsNullOrWhiteSpace(item?.Description))
            {
                param.Add("@Desription", item.Description);
            }
            if (item?.Price != null && (item?.Price > 0))
            {
                param.Add("@Price", item.Price);
            }
            if (item?.Sold != null)
            {
                param.Add("@Sold", item.Sold);
            }
            if (item?.PaymentDistributed != null)
            {
                param.Add("@PaymentDistributed", item.PaymentDistributed);
            }
            if (item?.Owner != null)
            {
                param.Add("@OwnerId", item.Owner);
            }
            return param;
        }
        #endregion

    }
}

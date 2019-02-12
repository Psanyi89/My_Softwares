using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayerLogic.Managers;
using PersonEntities;
namespace DataLayerLogic
{
    public class DLLFacade
    {
        #region In memory database manager interface
        /// <summary>
        /// Call in memory database manager interface
        /// </summary>
        /// <returns>CRUD and search methods</returns>
        public IPersonManager GetPersonManagerMemory()
        {
            return new PersonManagerFakeDB();
            //return new ProxyPersonManager();
        }
        #endregion

        #region Txt file database manager interface
        /// <summary>
        /// Call  Txt database manager interface
        /// </summary>
        /// <returns>CRUD and search methods</returns>
        public IPersonManager GetPersonManagerTxt()
        {
            return new TxtPersonManager();
        }
        #endregion

        #region CSV file database manager interface
        /// <summary>
        /// Call in CSV database manager interface
        /// </summary>
        /// <returns>CRUD and search methods</returns>
        public IPersonManager GetPersonManagerCSV()
        {
            return new PersonManagerCSV();
        }
        #endregion

        #region Xml file database manager interface
        /// <summary>
        /// Call in Xml database manager interface
        /// </summary>
        /// <returns>CRUD and search methods</returns>
        public IPersonManager GetPersonManagerXml()
        {
            return new PersonManagerXml();
        }
        #endregion

        #region Toml file database manager interface
        /// <summary>
        /// Call in Toml database manager interface
        /// </summary>
        /// <returns>CRUD and search methods</returns>
        public IPersonManager GetPersonManagerToml()
        {
            return new PersonManagerToml();
        }
        #endregion
        
        #region Json file database manager interface
        /// <summary>
        /// Call in Json database manager interface
        /// </summary>
        /// <returns>CRUD and search methods</returns>
        public IPersonManager GetPersonManagerJson()
        {
            return new PersonManagerJson();
        }
        #endregion

        #region MS Sql LocalDB manager interface
        /// <summary>
        /// Call in LocalDB database manager interface
        /// </summary>
        /// <returns>CRUD and search methods</returns>
        public IPersonManager GetPersonManagerLocalDB()
        {
            return new PersonManagerLocalDB();
        }
        #endregion

        #region SqLite database manager interface
        /// <summary>
        /// Call in SqLite database manager interface
        /// </summary>
        /// <returns>CRUD and search methods</returns>
        public IPersonManager GetPersonManagerSqLiteFakeDB()
        {
            return new PersonManagerSqLiteFakeDB();
        }
        #endregion

        #region Binary database manager interface
        /// <summary>
        /// Call in Binary database manager interface
        /// </summary>
        /// <returns>CRUD and search methods</returns>
        public IPersonManager GetPersonManagerBinary()
        {
            return new PersonManagerBinary();
        }
        #endregion
    }
}

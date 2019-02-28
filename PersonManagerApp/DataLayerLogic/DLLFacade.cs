using System;
using DataLayerLogic.Managers;
namespace DataLayerLogic
{
    public class DLLFacade
    {
        public static IPersonManager CreateManager(AccessType accessType)
        {
            switch (accessType)
            {
                case AccessType.Memory:
                    #region In memory database manager interface
                    /// <summary>
                    /// Call in memory database manager interface
                    /// </summary>
                    /// <returns>CRUD and search methods</returns>
                    return new PersonManagerFakeDB();
                #endregion
                case AccessType.TXT:
                    #region Txt file database manager interface
                    /// <summary>
                    /// Call  Txt database manager interface
                    /// </summary>
                    /// <returns>CRUD and search methods</returns>
                    return new TxtPersonManager();
                #endregion
                case AccessType.CSV:
                    #region CSV file database manager interface
                    /// <summary>
                    /// Call in CSV database manager interface
                    /// </summary>
                    /// <returns>CRUD and search methods</returns>
                    return new PersonManagerCSV();
                #endregion
                case AccessType.XML:
                    #region Xml file database manager interface
                    /// <summary>
                    /// Call in Xml database manager interface
                    /// </summary>
                    /// <returns>CRUD and search methods</returns>
                    return new PersonManagerXml();
                #endregion
                case AccessType.TOML:
                    #region Toml file database manager interface
                    /// <summary>
                    /// Call in Toml database manager interface
                    /// </summary>
                    /// <returns>CRUD and search methods</returns>
                    return new PersonManagerToml();
                #endregion
                case AccessType.JSON:
                    #region Json file database manager interface
                    /// <summary>
                    /// Call in Json database manager interface
                    /// </summary>
                    /// <returns>CRUD and search methods</returns>
                    return new PersonManagerJson();
                #endregion
                case AccessType.LocalDB:
                    #region MS Sql LocalDB manager interface
                    /// <summary>
                    /// Call in LocalDB database manager interface
                    /// </summary>
                    /// <returns>CRUD and search methods</returns>
                    return new PersonManagerLocalDB();
                #endregion
                case AccessType.SQLite:
                    #region SqLite database manager interface
                    /// <summary>
                    /// Call in SqLite database manager interface
                    /// </summary>
                    /// <returns>CRUD and search methods</returns>
                    return new PersonManagerSqLiteFakeDB();
                #endregion
                case AccessType.Binary:
                    #region Binary database manager interface
                    /// <summary>
                    /// Call in Binary database manager interface
                    /// </summary>
                    /// <returns>CRUD and search methods</returns>
                    return new PersonManagerBinary();
                #endregion
                default:
                    throw new InvalidOperationException($"Unknown access type {accessType}");
            }
        }

    }
}

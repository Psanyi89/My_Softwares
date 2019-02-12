using System.Collections.Generic;

namespace DataLayerLogic
{
    public class DataAccess
    {
        #region DataAccess Options

        /// <summary>
        /// Options for DataAccess
        /// </summary>
        public List<string> AccesPoint = new List<string>
        {

            "Database in Memory",

            "TXT Database",

            "CSV Database",

            "Xml Database",

            "Toml Database",

            "Json Database",

            "LocalDB Database",

            "SqLite Database",

            "Binary Database"

        };
        #endregion
    }
}

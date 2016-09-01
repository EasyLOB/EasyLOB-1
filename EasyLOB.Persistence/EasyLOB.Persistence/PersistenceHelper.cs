using EasyLOB.Library;

namespace EasyLOB.Persistence
{
    #region Types DBMS

    /// <summary>
    /// Database Logger
    /// </summary>
    public enum ZDatabaseLogger
    {
        None,
        File,
        NLog
    };

    /// <summary>
    /// Database Management Systems
    /// </summary>
    public enum ZDBMS
    {
        Unknown,
        Firebird,
        MongoDB, // NoSQL
        MySQL,
        OData,
        Oracle,
        PostgreSQL,
        RavenDB, // NoSQL
        Redis, // NoSQL
        SQLite,
        SQLServer
    };

    #endregion Types DBMS

    /// <summary>
    /// Persistence Helper
    /// </summary>
    public static partial class PersistenceHelper
    {
        #region Properties

        public static bool IsTransaction
        {
            get
            {
                return LibraryHelper.AppSettings<bool>("EasyLOB.Transaction");
            }
        }

        #endregion 

        #region Methods DBMS

        /// <summary>
        /// Does DBMS generate Identity Ids ?
        /// </summary>
        /// <param name="database">Database</param>
        /// <returns>Generates ?</returns>
        public static bool GeneratesIdentity(ZDBMS database)
        {
            bool result;

            switch (database)
            {
                case ZDBMS.MySQL:
                case ZDBMS.SQLite:
                case ZDBMS.SQLServer:
                    result = true;
                    break;

                default:
                    result = false;
                    break;
            }

            return result;
        }

        /// <summary>
        /// Does DBMS have Server-Side Joins ?
        /// </summary>
        /// <param name="database">Database</param>
        /// <returns>Has ?</returns>
        public static bool HasServerSideJoins(ZDBMS database)
        {
            bool result;

            switch (database)
            {
                case ZDBMS.Firebird:
                case ZDBMS.MySQL:
                case ZDBMS.Oracle:
                case ZDBMS.PostgreSQL:
                case ZDBMS.RavenDB:
                case ZDBMS.SQLite:
                case ZDBMS.SQLServer:
                    result = false;
                    break;

                default:
                    result = false;
                    break;
            }

            return result;
        }

        #endregion Methods DBMS
    }
}
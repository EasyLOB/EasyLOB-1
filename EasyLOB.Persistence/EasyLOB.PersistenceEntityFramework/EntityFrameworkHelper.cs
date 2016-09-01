using EasyLOB.Log;
using System.IO;

namespace EasyLOB.Persistence
{
    public static partial class EntityFrameworkHelper
    {
        #region Methods

        public static void Log(string log, ZDatabaseLogger databaseLogger)
        {
            if (log != "\r\n")
            {
                switch (databaseLogger)
                {
                    case ZDatabaseLogger.File:
                        {
                            string filePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/EntityFramework.log";
                            File.AppendAllText(filePath, log);
                        }
                        break;

                    case ZDatabaseLogger.NLog:
                        ILogManager logManager = new LogManager();
                        logManager.Trace(log);
                        break;
                }
            }
        }

        #endregion Methods
    }
}
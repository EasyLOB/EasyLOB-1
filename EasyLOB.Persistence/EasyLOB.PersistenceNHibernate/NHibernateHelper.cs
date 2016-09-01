using EasyLOB.Log;
using Newtonsoft.Json.Serialization;
using NHibernate.Proxy;
using System;
using System.IO;

namespace EasyLOB.Persistence
{
    public static partial class NHibernateHelper
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
                            string filePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/NHibernate.log";
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

    /// <summary>
    /// "Error getting value from 'DefaultValue' on 'NHibernate.Type.DateTimeOffsetType'."
    /// JsonSerializationException on lazy loaded nHibernate object in WebApi when called from Angularjs service
    /// http://stackoverflow.com/questions/25011406/jsonserializationexception-on-lazy-loaded-nhibernate-object-in-webapi-when-calle
    /// Global.asax.cs
    /// GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new NHibernateContractResolver();
    /// </summary>
    public class NHibernateContractResolver : DefaultContractResolver // ???
    {
        protected override JsonContract CreateContract(Type objectType)
        {
            if (typeof(INHibernateProxy).IsAssignableFrom(objectType))
            {
                return base.CreateContract(objectType.BaseType);
            }

            return base.CreateContract(objectType);
        }
    }
}
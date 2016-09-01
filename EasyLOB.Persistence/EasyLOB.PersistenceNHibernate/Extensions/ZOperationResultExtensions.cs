using EasyLOB.Library;
using NHibernate;
using System;
using System.Collections.Generic;

namespace EasyLOB.Persistence
{
    public static partial class ZOperationResultExtensions
    {
        public static void ParseExceptionNHibernate(this ZOperationResult operationResult, Exception exception)
        {
            //NHibernate.ADOException
            //NHibernate.CallbackException
            //NHibernate.HibernateException
            //NHibernate.InstantiationException
            //NHibernate.LazyInitializationException
            //NHibernate.MappingException
            //NHibernate.NonUniqueObjectException
            //NHibernate.NonUniqueResultException
            //NHibernate.ObjectDeletedException
            //NHibernate.ObjectNotFoundException
            //NHibernate.PersistentObjectException
            //NHibernate.PropertyAccessException
            //NHibernate.PropertyNotFoundException
            //NHibernate.PropertyValueException
            //NHibernate.QueryParameterException
            //NHibernate.SessionException
            //NHibernate.StaleObjectStateException
            //NHibernate.StaleStateException
            //NHibernate.TransactionException
            //NHibernate.TransientObjectException
            //NHibernate.TypeMismatchException
            //NHibernate.UnresolvableObjectException
            //NHibernate.WrongClassException

            if (exception is MappingException)
            {
                operationResult.ErrorMessage = exception.Message;

                foreach (KeyValuePair<string, object> pair in (exception as MappingException).Data)
                {
                    operationResult.OperationErrors.Add(new ZOperationError("", "Mapping Exception: " + pair.Value.ToString(), new List<string> { pair.Key }));
                }
            }
            else
            {
                operationResult.ParseException(exception);
            }
        }
    }
}
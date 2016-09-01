using EasyLOB.Data;
using EasyLOB.Library;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

// Install-Package ServiceStack.Redis

// localhost
// 127.0.0.1:6379
// redis://localhost:6379
// password @localhost:6379
// clientid: password @localhost:6379
// redis://clientid:password@localhost:6380?ssl=true&db=1

// Redis Transactions works ONLY with Transaction.QueueCommand()

// How to create custom atomic operations in Redis
// https://github.com/ServiceStack/ServiceStack.Redis/wiki/RedisTransactions

// IRedisTransaction
// https://github.com/ServiceStack/ServiceStack.Redis/wiki/IRedisTransaction

//int callbackResult;
//using (var transaction = redis.CreateTransaction())
//{
//  transaction.QueueCommand(r => r.Increment("key"));  
//  transaction.QueueCommand(r => r.Increment("key"), i => callbackResult = i);  
//  transaction.Commit();
//}

namespace EasyLOB.Persistence
{
    public abstract class UnitOfWorkRedis : IUnitOfWork
    {
        #region Properties

        private ZDatabaseLogger _databaseLogger;

        public ZDatabaseLogger DatabaseLogger
        {
            get
            {
                return _databaseLogger;
            }
            set
            {
                _databaseLogger = value;
            }
        }

        public ZDBMS DBMS
        {
            get { return ZDBMS.Redis; }
        }

        public string Domain { get; protected set; }

        public IDictionary<Type, object> Repositories { get; }

        #endregion Properties

        #region Properties Redis

        public IRedisClient Client { get; protected set; }

        //public IRedisTransaction Transaction { get; }

        #endregion Properties Redis

        #region Methods

        public UnitOfWorkRedis(string host)
        {
            Client = new RedisClient(host);

            DatabaseLogger = ZDatabaseLogger.None;
            Domain = "";
            Repositories = new Dictionary<Type, object>();
        }

        public virtual void Dispose()
        {
        }

        public virtual bool BeginTransaction(ZOperationResult operationResult, bool isTransaction = true, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            //try
            //{
            //    if (isTransaction && PersistenceHelper.IsTransaction)
            //    {
            //        if (Transaction == null || ?)
            //        {
            //            Transaction = Client.CreateTransaction();
            //        }
            //    }
            //}
            //catch (Exception exception)
            //{
            //    operationResult.ParseExceptionRedis(exception);
            //}

            return operationResult.Ok;
        }

        public virtual bool CommitTransaction(ZOperationResult operationResult, bool isTransaction = true)
        {
            //try
            //{
            //    if (isTransaction && PersistenceHelper.IsTransaction)
            //    {
            //        if (Transaction != null)
            //        {
            //            Transaction.Commit();
            //        }
            //    }
            //}
            //catch (Exception exception)
            //{
            //    operationResult.ParseExceptionRedis(exception);
            //}

            return operationResult.Ok;
        }

        public virtual int ExecuteSQL(string sql)
        {
            throw new NotSupportedException();
        }

        public ZDataDictionaryAttribute GetDataDictionary<TEntity>()
            where TEntity : class, IZDataBase
        {
            return GetRepository<TEntity>().DataDictionary;
        }

        public virtual IQueryable<TEntity> GetQuery<TEntity>()
            where TEntity : class, IZDataBase
        {
            return GetRepository<TEntity>().Query;
        }

        public virtual IGenericRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class, IZDataBase
        {
            throw new NotImplementedException("abstract class Redis UnitOfWork.GetRepository()");

            //if (!Repositories.Keys.Contains(typeof(TEntity)))
            //{
            //    var repository = new GenericRepository<TEntity>(Context);
            //    Repositories.Add(typeof(TEntity), repository);
            //}

            //return Repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
        }

        public virtual bool RollbackTransaction(ZOperationResult operationResult, bool isTransaction = true)
        {
            //try
            //{
            //    if (isTransaction && PersistenceHelper.IsTransaction)
            //    {
            //        if (Transaction != null)
            //        {
            //            Transaction.Rollback();
            //        }
            //    }
            //}
            //catch (Exception exception)
            //{
            //    operationResult.ParseExceptionRedis(exception);
            //}

            return operationResult.Ok;
        }

        public virtual bool Save(ZOperationResult operationResult)
        {
            //try
            //{
            //    Client.Save();
            //}
            //catch (Exception exception)
            //{
            //    operationResult.ParseExceptionRedis(exception);
            //}

            return operationResult.Ok;
        }

        #endregion Methods

        #region Triggers

        public virtual bool BeforeCreate(ZOperationResult operationResult, object entity)
        {
            return operationResult.Ok;
        }

        public virtual bool AfterCreate(ZOperationResult operationResult, object entity)
        {
            return operationResult.Ok;
        }

        public virtual bool BeforeDelete(ZOperationResult operationResult, object entity)
        {
            return operationResult.Ok;
        }

        public virtual bool AfterDelete(ZOperationResult operationResult, object entity)
        {
            return operationResult.Ok;
        }

        public virtual bool BeforeUpdate(ZOperationResult operationResult, object entity)
        {
            return operationResult.Ok;
        }

        public virtual bool AfterUpdate(ZOperationResult operationResult, object entity)
        {
            return operationResult.Ok;
        }

        #endregion Triggers
    }
}
using EasyLOB.Data;
using EasyLOB.Library;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

// Install-Package MongoDB.Driver

namespace EasyLOB.Persistence
{
    public abstract class UnitOfWorkMongoDB : IUnitOfWork
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
            get { return ZDBMS.MongoDB; }
        }

        public string Domain { get; protected set; }

        public IDictionary<Type, object> Repositories { get; }

        #endregion Properties

        #region Properties MongoDB

        public IMongoDatabase Database { get; }

        #endregion Properties MongoDB

        #region Methods

        public UnitOfWorkMongoDB(string url, string databaseName)
        {
            Database = (new MongoClient(url)).GetDatabase(databaseName);

            DatabaseLogger = ZDatabaseLogger.None;
            Domain = "";
            Repositories = new Dictionary<Type, object>();
        }

        public virtual void Dispose()
        {
        }

        public virtual bool BeginTransaction(ZOperationResult operationResult, bool isTransaction = true, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return operationResult.Ok;
        }

        public virtual bool CommitTransaction(ZOperationResult operationResult, bool isTransaction = true)
        {
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
            throw new NotImplementedException("abstract class MongoDB UnitOfWork.GetRepository()");

            //if (!Repositories.Keys.Contains(typeof(TEntity)))
            //{
            //    var repository = new GenericRepository<TEntity>(Context);
            //    Repositories.Add(typeof(TEntity), repository);
            //}

            //return Repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
        }

        public virtual bool RollbackTransaction(ZOperationResult operationResult, bool isTransaction = true)
        {
            return operationResult.Ok;
        }

        public virtual bool Save(ZOperationResult operationResult)
        {
            return operationResult.Ok;
        }

        #endregion Methods

        #region Methods MongoDB

        protected IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            return Database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        #endregion Methods MongoDB

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
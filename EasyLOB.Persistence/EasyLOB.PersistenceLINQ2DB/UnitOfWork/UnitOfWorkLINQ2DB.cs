using EasyLOB.Data;
using EasyLOB.Library;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

// Install-Package linq2db

namespace EasyLOB.Persistence
{
    public abstract class UnitOfWorkLINQ2DB : IUnitOfWork
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
            get
            {
                return AdoNetHelper.GetDBMS(Connection.DataProvider.ConnectionNamespace);
            }
        }

        public string Domain { get; protected set; }

        public IDictionary<Type, object> Repositories { get; }

        #endregion Properties

        #region Properties LINQ to DB

        public DataConnection Connection { get; }

        public DataConnectionTransaction Transaction { get; protected set; }

        #endregion Properties LINQ to DB

        #region Methods

        public UnitOfWorkLINQ2DB(DataConnection connection)
        {
            Connection = connection;

            DatabaseLogger = ZDatabaseLogger.None;
            Domain = "";
            Repositories = new Dictionary<Type, object>();
        }

        public virtual void Dispose()
        {
            if (Transaction != null)
            {
                Transaction.Dispose();
                Transaction = null;
            }

            Connection.Dispose();
        }

        public virtual bool BeginTransaction(ZOperationResult operationResult, bool isTransaction = true, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            try
            {
                if (isTransaction && PersistenceHelper.IsTransaction)
                {
                    if (Transaction == null || Transaction.DataConnection == null)
                    {
                        Transaction = Connection.BeginTransaction(isolationLevel);
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseExceptionLINQ2DB(exception);
            }

            return operationResult.Ok;
        }

        public virtual bool CommitTransaction(ZOperationResult operationResult, bool isTransaction = true)
        {
            try
            {
                if (isTransaction && PersistenceHelper.IsTransaction)
                {
                    if (Transaction != null)
                    {
                        Transaction.Commit();
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseExceptionLINQ2DB(exception);
            }

            return operationResult.Ok;
        }

        public virtual int ExecuteSQL(string sql)
        {
            return Connection.Execute(sql);
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
            throw new NotImplementedException("abstract class LINQ to DB UnitOfWork.GetRepository()");

            //if (!Repositories.Keys.Contains(typeof(TEntity)))
            //{
            //    var repository = new GenericRepository<TEntity>(Context);
            //    Repositories.Add(typeof(TEntity), repository);
            //}

            //return Repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
        }

        public virtual bool RollbackTransaction(ZOperationResult operationResult, bool isTransaction = true)
        {
            try
            {
                if (isTransaction && PersistenceHelper.IsTransaction)
                {
                    if (Transaction != null)
                    {
                        Transaction.Rollback();
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseExceptionLINQ2DB(exception);
            }

            return operationResult.Ok;
        }

        public virtual bool Save(ZOperationResult operationResult)
        {
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
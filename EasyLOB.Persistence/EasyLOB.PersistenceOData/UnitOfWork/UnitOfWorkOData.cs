using EasyLOB.Data;
using EasyLOB.Library;
using Microsoft.OData.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

// Install-Package Microsoft.OData.Client -Version 6.15.0

namespace EasyLOB.Persistence
{
    public abstract class UnitOfWorkOData : IUnitOfWorkDTO
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
                return ZDBMS.OData;
            }
        }

        public string Domain { get; protected set; }

        public IDictionary<Type, object> Repositories { get; }

        #endregion Properties

        #region Properties OData

        public DataServiceContext Container { get; }

        #endregion Properties OData

        #region Methods

        public UnitOfWorkOData(DataServiceContext container)
        {
            Container = container;

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

        public ZDataDictionaryAttribute GetDataDictionary<TEntityDTO, TEntity>()
            where TEntityDTO : class, IZDTOBase<TEntityDTO, TEntity>
            where TEntity : class, IZDataBase
        {
            return GetRepository<TEntityDTO, TEntity>().DataDictionary;
        }

        public virtual IQueryable<TEntityDTO> GetQuery<TEntityDTO, TEntity>()
            where TEntityDTO : class, IZDTOBase<TEntityDTO, TEntity>
            where TEntity : class, IZDataBase
        {
            return GetRepository<TEntityDTO, TEntity>().Query;
        }

        public virtual IGenericRepositoryDTO<TEntityDTO, TEntity> GetRepository<TEntityDTO, TEntity>()
            where TEntityDTO : class, IZDTOBase<TEntityDTO, TEntity>
            where TEntity : class, IZDataBase
        {
            throw new NotImplementedException("abstract class OData UnitOfWork.GetRepository()");
        }

        public virtual bool RollbackTransaction(ZOperationResult operationResult, bool isTransaction = true)
        {
            return operationResult.Ok;
        }

        public virtual bool Save(ZOperationResult operationResult)
        {
            try
            {
                Container.SaveChanges();
            }
            catch (Exception exception)
            {
                operationResult.ParseExceptionOData(exception);
            }

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
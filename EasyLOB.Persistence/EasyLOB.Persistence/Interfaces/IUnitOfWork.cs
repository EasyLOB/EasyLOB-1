using EasyLOB.Data;
using EasyLOB.Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace EasyLOB.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        #region Properties

        /// <summary>
        /// Logger.
        /// </summary>
        ZDatabaseLogger DatabaseLogger { get; set; }

        /// <summary>
        /// DBMS.
        /// </summary>
        ZDBMS DBMS { get; }

        /// <summary>
        /// Domain.
        /// </summary>
        string Domain { get; }

        /// <summary>
        /// Repositories.
        /// </summary>
        IDictionary<Type, object> Repositories { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Begin Transaction.
        /// </summary>
        /// <param name="operationResult">Operation Result</param>
        /// <param name="isTransaction">Do Transaction ?</param>
        /// <param name="isolationLevel">Isolation Level</param>
        /// <returns>Ok ?</returns>
        bool BeginTransaction(ZOperationResult operationResult, bool isTransaction = true, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

        /// <summary>
        /// Commit Transaction.
        /// </summary>
        /// <param name="operationResult">Operation Result</param>
        /// <param name="isTransaction">Do Transaction ?</param>
        /// <returns>Ok ?</returns>
        bool CommitTransaction(ZOperationResult operationResult, bool isTransaction = true);

        /// <summary>
        /// Execute SQL.
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <returns>Scalar</returns>
        int ExecuteSQL(string sql);

        /// <summary>
        /// Get Data Dictionary.
        /// </summary>
        /// <typeparam name="TEntity">Entity</typeparam>
        /// <returns>Data Dictionary</returns>
        ZDataDictionaryAttribute GetDataDictionary<TEntity>()
            where TEntity : class, IZDataBase;

        /// <summary>
        /// Get IQueryable.
        /// </summary>
        /// <typeparam name="TEntity">Entity</typeparam>
        /// <returns>Ok ?</returns>
        IQueryable<TEntity> GetQuery<TEntity>()
            where TEntity : class, IZDataBase;

        /// <summary>
        /// Get Repository.
        /// </summary>
        /// <typeparam name="TEntity">Entity</typeparam>
        /// <returns>Ok ?</returns>
        IGenericRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class, IZDataBase;

        /// <summary>
        /// Rollback Transaction.
        /// </summary>
        /// <param name="operationResult">Operation Result</param>
        /// <param name="isTransaction">Do Transaction ?</param>
        /// <returns>Ok ?</returns>
        bool RollbackTransaction(ZOperationResult operationResult, bool isTransaction = true);

        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="operationResult">Operation Result</param>
        /// <returns>Ok ?</returns>
        bool Save(ZOperationResult operationResult);

        #endregion Methods

        #region Triggers

        /// <summary>
        /// Before create Trigger.
        /// </summary>
        /// <param name="operationResult">Operation Result</param>
        /// <param name="entity">Entity</param>
        /// <returns>Ok ?</returns>
        bool BeforeCreate(ZOperationResult operationResult, object entity);

        /// <summary>
        /// After create Trigger.
        /// </summary>
        /// <param name="operationResult">Operation Result</param>
        /// <param name="entity">Entity</param>
        /// <returns>Ok ?</returns>
        bool AfterCreate(ZOperationResult operationResult, object entity);

        /// <summary>
        /// Before delete Trigger.
        /// </summary>
        /// <param name="operationResult">Operation Result</param>
        /// <param name="entity">Entity</param>
        /// <returns>Ok ?</returns>
        bool BeforeDelete(ZOperationResult operationResult, object entity);

        /// <summary>
        /// After delete Trigger.
        /// </summary>
        /// <param name="operationResult">Operation Result</param>
        /// <param name="entity">Entity</param>
        /// <returns>Ok ?</returns>
        bool AfterDelete(ZOperationResult operationResult, object entity);

        /// <summary>
        /// Before udpate Trigger.
        /// </summary>
        /// <param name="operationResult">Operation Result</param>
        /// <param name="entity">Entity</param>
        /// <returns>Ok ?</returns>
        bool BeforeUpdate(ZOperationResult operationResult, object entity);

        /// <summary>
        /// After update Trigger.
        /// </summary>
        /// <param name="operationResult">Operation Result</param>
        /// <param name="entity">Entity</param>
        /// <returns>Ok ?</returns>
        bool AfterUpdate(ZOperationResult operationResult, object entity);

        #endregion Triggers
    }
}
using EasyLOB.AuditTrail;
using EasyLOB.Data;
using EasyLOB.Library;
using EasyLOB.Log;
using EasyLOB.Persistence;
using EasyLOB.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EasyLOB.Application
{
    public interface IGenericApplication<TEntity>
        where TEntity : class, IZDataBase
    {
        #region Properties

        IQueryable<TEntity> Query { get; }

        IGenericRepository<TEntity> Repository { get; }

        IUnitOfWork UnitOfWork { get; }

        IAuditTrailManager AuditTrailManager { get; }

        ILogManager LogManager { get; }

        ISecurityManager SecurityManager { get; }

        ZIsSecurityOperations IsSecurityOperations { get; }

        #endregion Properties

        #region Methods

        int Count(Expression<Func<TEntity, bool>> where);

        int Count(string where, object[] args);

        int CountAll();

        bool Create(ZOperationResult operationResult, TEntity entity, bool isTransaction = true);

        bool Delete(ZOperationResult operationResult, TEntity entity, bool isTransaction = true);

        TEntity Get(ZOperationResult operationResult, Expression<Func<TEntity, bool>> where);

        TEntity Get(ZOperationResult operationResult, string where);

        TEntity GetById(ZOperationResult operationResult, object id);

        TEntity GetById(ZOperationResult operationResult, object[] ids);

        IEnumerable<TEntity> Select(ZOperationResult operationResult,
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            List<Expression<Func<TEntity, object>>> associations = null);

        IEnumerable<TEntity> Select(ZOperationResult operationResult,
            string where = null,
            object[] arguments = null,
            string orderBy = null,
            int? skip = null,
            int? take = null,
            string[] associations = null);

        IEnumerable<TEntity> SelectAll(ZOperationResult operationResult);

        bool Update(ZOperationResult operationResult, TEntity entity, bool isTransaction = true);

        #endregion Methods
    }
}
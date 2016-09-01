using EasyLOB.Activity;
using EasyLOB.AuditTrail;
using EasyLOB.Data;
using EasyLOB.Library;
using EasyLOB.Library.Resources;
using EasyLOB.Log;
using EasyLOB.Persistence;
using EasyLOB.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EasyLOB.Application
{
    public abstract class GenericApplication<TEntity> : IGenericApplication<TEntity>
        where TEntity : class, IZDataBase
    {
        #region Properties

        public virtual IQueryable<TEntity> Query
        {
            get { return Repository.Query; }
        }

        public IGenericRepository<TEntity> Repository
        {
            get { return UnitOfWork.GetRepository<TEntity>(); }
        }

        public IUnitOfWork UnitOfWork { get; }

        public IAuditTrailManager AuditTrailManager { get; }

        public ILogManager LogManager { get; }

        public ISecurityManager SecurityManager { get; }

        public ZIsSecurityOperations IsSecurityOperations { get; }

        #endregion Properties

        #region Methods

        public GenericApplication(IUnitOfWork unitOfWork, IAuditTrailManager auditTrailManager, ILogManager logManager, ISecurityManager securityManager)
        {
            UnitOfWork = unitOfWork;
            AuditTrailManager = auditTrailManager;
            LogManager = logManager;
            SecurityManager = securityManager;

            IsSecurityOperations = SecurityManager.GetOperations(ActivityHelper.EntityActivity(unitOfWork.Domain, Repository.Entity));
        }

        public int Count(Expression<Func<TEntity, bool>> where)
        {
            return Repository.Count(where);
        }

        public int Count(string where, object[] args)
        {
            return Repository.Count(where, args);
        }

        public int CountAll()
        {
            return Repository.CountAll();
        }

        public virtual bool Create(ZOperationResult operationResult, TEntity entity, bool isTransaction = true)
        {
            bool inTransaction = false;

            try
            {
                if (IsCreate(operationResult))
                {
                    inTransaction = UnitOfWork.BeginTransaction(operationResult, isTransaction);
                    if (inTransaction)
                    {
                        try
                        {
                            if (Repository.Create(operationResult, entity))
                            {
                                if (UnitOfWork.Save(operationResult))
                                {
                                    if (UnitOfWork.CommitTransaction(operationResult, isTransaction))
                                    {
                                        string logOperation = "C";
                                        AuditTrailManager.AuditTrail(operationResult, UnitOfWork.Domain, Repository.Entity, logOperation, null, entity);
                                    }
                                }
                            }
                        }
                        finally
                        {
                            if (!operationResult.Ok)
                            {

                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }
            finally
            {
                if (inTransaction && !operationResult.Ok)
                {
                    UnitOfWork.RollbackTransaction(operationResult, isTransaction);
                }
            }

            return operationResult.Ok;
        }

        public virtual bool Delete(ZOperationResult operationResult, TEntity entity, bool isTransaction = true)
        {
            bool inTransaction = false;

            try
            {
                if (IsDelete(operationResult))
                {
                    inTransaction = UnitOfWork.BeginTransaction(operationResult, isTransaction);
                    if (inTransaction)
                    {
                        if (Repository.Delete(operationResult, entity))
                        {
                            if (UnitOfWork.Save(operationResult))
                            {
                                if (UnitOfWork.CommitTransaction(operationResult, isTransaction))
                                {
                                    string logOperation = "D";
                                    AuditTrailManager.AuditTrail(operationResult, UnitOfWork.Domain, Repository.Entity, logOperation, entity, null);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }
            finally
            {
                if (inTransaction && !operationResult.Ok)
                {
                    UnitOfWork.RollbackTransaction(operationResult, isTransaction);
                }
            }

            return operationResult.Ok;
        }

        public virtual TEntity Get(ZOperationResult operationResult, Expression<Func<TEntity, bool>> where)
        {
            TEntity result = null;

            try
            {
                if (IsRead(operationResult) || IsUpdate(operationResult) || IsDelete(operationResult))
                {
                    result = Repository.Get(where);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return result;
        }

        public virtual TEntity Get(ZOperationResult operationResult, string where)
        {
            TEntity result = null;

            try
            {
                if (IsRead(operationResult) || IsUpdate(operationResult) || IsDelete(operationResult))
                {
                    result = Repository.Get(where);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return result;
        }

        public virtual TEntity GetById(ZOperationResult operationResult, object id)
        {            
            return GetById(operationResult, new object[] { id });
        }

        public virtual TEntity GetById(ZOperationResult operationResult, object[] ids)
        {
            TEntity result = null;

            try
            {
                if (IsRead(operationResult) || IsUpdate(operationResult) || IsDelete(operationResult))
                {
                    result = Repository.GetById(ids);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return result;
        }

        public virtual IEnumerable<TEntity> Select(ZOperationResult operationResult, Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            List<Expression<Func<TEntity, object>>> associations = null)
        {
            IEnumerable<TEntity> result = new List<TEntity>();

            try
            {
                if (IsSearch(operationResult))
                {
                    result = Repository.Select(where, orderBy, skip, take, associations);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return result;
        }

        public virtual IEnumerable<TEntity> Select(ZOperationResult operationResult, string where = null,
            object[] arguments = null,
            string orderBy = null,
            int? skip = null,
            int? take = null,
            string[] associations = null)
        {
            IEnumerable<TEntity> result = new List<TEntity>();

            try
            {
                if (IsSearch(operationResult))
                {
                    result = Repository.Select(where, arguments, orderBy, skip, take, associations);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return result;
        }

        public IEnumerable<TEntity> SelectAll(ZOperationResult operationResult)
        {
            IEnumerable<TEntity> result = new List<TEntity>();

            try
            {
                if (IsSearch(operationResult))
                {
                    result = Repository.SelectAll();
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return result;
        }

        public bool Update(ZOperationResult operationResult, TEntity entity, bool isTransaction = true)
        {
            bool inTransaction = false;

            try
            {
                if (IsUpdate(operationResult))
                {
                    inTransaction = UnitOfWork.BeginTransaction(operationResult, isTransaction);
                    if (inTransaction)
                    {
                        string logOperation = "U";
                        bool isAuditTrail = AuditTrailManager.IsAuditTrail(UnitOfWork.Domain, Repository.Entity, logOperation);
                        TEntity entityBefore = null;
                        if (isAuditTrail)
                        {
                            entityBefore = Repository.GetById(entity.GetId());
                        }

                        if (Repository.Update(operationResult, entity))
                        {
                            if (UnitOfWork.Save(operationResult))
                            {
                                if (UnitOfWork.CommitTransaction(operationResult, isTransaction))
                                {
                                    if (isAuditTrail)
                                    {
                                        AuditTrailManager.AuditTrail(operationResult, UnitOfWork.Domain, Repository.Entity, logOperation, entityBefore, entity);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }
            finally
            {
                if (inTransaction && !operationResult.Ok)
                {
                    UnitOfWork.RollbackTransaction(operationResult, isTransaction);
                }
            }

            return operationResult.Ok;
        }

        #endregion Methods

        #region Methods IsActivity

        protected bool IsSearch(ZOperationResult operationResult)
        {
            return SecurityHelper.IsSearch(IsSecurityOperations, operationResult);
        }

        protected bool IsCreate(ZOperationResult operationResult)
        {
            return SecurityHelper.IsCreate(IsSecurityOperations, operationResult);
        }

        protected bool IsRead(ZOperationResult operationResult)
        {
            return SecurityHelper.IsRead(IsSecurityOperations, operationResult);
        }

        protected bool IsUpdate(ZOperationResult operationResult)
        {
            return SecurityHelper.IsUpdate(IsSecurityOperations, operationResult);
        }

        protected bool IsDelete(ZOperationResult operationResult)
        {
            return SecurityHelper.IsDelete(IsSecurityOperations, operationResult);
        }

        protected bool IsExecute(ZOperationResult operationResult)
        {
            return SecurityHelper.IsExecute(IsSecurityOperations, operationResult);
        }

        #endregion Methods Activity
    }
}
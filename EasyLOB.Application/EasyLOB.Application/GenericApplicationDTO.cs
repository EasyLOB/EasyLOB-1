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
    public abstract class GenericApplicationDTO<TEntityDTO, TEntity> : GenericApplication<TEntity>, IGenericApplicationDTO<TEntityDTO, TEntity>
        where TEntityDTO : class, IZDTOBase<TEntityDTO, TEntity>
        where TEntity : class, IZDataBase
    {
        #region Properties

        public IQueryable<TEntityDTO> QueryDTO
        {
            get
            {
                TEntityDTO dto = (TEntityDTO)Activator.CreateInstance(typeof(TEntityDTO));

                return Query.Select(dto.GetDTOSelector()).AsQueryable();
            }
        }

        #endregion Properties

        #region Methods

        public GenericApplicationDTO(IUnitOfWork unitOfWork, IAuditTrailManager auditTrailManager, ILogManager logManager, ISecurityManager securityManager)
            : base(unitOfWork, auditTrailManager, logManager, securityManager)
        {
        }

        public virtual bool Create(ZOperationResult operationResult, TEntityDTO entityDTO, bool isTransaction = true)
        {
            bool inTransaction = false;

            try
            {
                if (IsCreate(operationResult))
                {
                    TEntity entity = (TEntity)entityDTO.ToData();

                    inTransaction = UnitOfWork.BeginTransaction(operationResult, isTransaction);
                    if (inTransaction)
                    {
                        if (Repository.Create(operationResult, entity))
                        {
                            if (UnitOfWork.Save(operationResult))
                            {
                                if (UnitOfWork.CommitTransaction(operationResult, isTransaction))
                                {
                                    //???
                                    //LibraryHelper.Clone((TEntityDTO)Activator.CreateInstance(typeof(TEntityDTO), entity), entityDTO);
                                    entityDTO.FromData(entity);

                                    string logOperation = "C";
                                    AuditTrailManager.AuditTrail(operationResult, UnitOfWork.Domain, Repository.Entity, logOperation, null, entity);
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

        public virtual bool Delete(ZOperationResult operationResult, TEntityDTO entityDTO, bool isTransaction = true)
        {
            bool inTransaction = false;

            try
            {
                if (IsDelete(operationResult))
                {
                    TEntity entity = (TEntity)entityDTO.ToData();

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

        public new TEntityDTO Get(ZOperationResult operationResult, Expression<Func<TEntity, bool>> where)
        {
            TEntityDTO result = null;

            try
            {
                if (IsRead(operationResult) || IsUpdate(operationResult) || IsDelete(operationResult))
                {
                    result = (TEntityDTO)Activator.CreateInstance(typeof(TEntityDTO), Repository.Get(where));
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return result;
        }

        public new TEntityDTO Get(ZOperationResult operationResult, string where)
        {
            TEntityDTO result = null;

            try
            {
                if (IsRead(operationResult) || IsUpdate(operationResult) || IsDelete(operationResult))
                {
                    result = (TEntityDTO)Activator.CreateInstance(typeof(TEntityDTO), Repository.Get(where));
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return result;
        }

        public new TEntityDTO GetById(ZOperationResult operationResult, object id)
        {
            return GetById(operationResult, new object[] { id });
        }

        public new TEntityDTO GetById(ZOperationResult operationResult, object[] ids)
        {
            TEntityDTO result = null;

            try
            {
                if (IsRead(operationResult) || IsUpdate(operationResult) || IsDelete(operationResult))
                {
                    result = (TEntityDTO)Activator.CreateInstance(typeof(TEntityDTO), Repository.GetById(ids));
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return result;
        }

        public new IEnumerable<TEntityDTO> Select(ZOperationResult operationResult,
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            List<Expression<Func<TEntity, object>>> associations = null)
        {
            IEnumerable<TEntityDTO> result = new List<TEntityDTO>();

            try
            {
                if (IsSearch(operationResult))
                {
                    return ZDTOHelper<TEntityDTO, TEntity>.ToDTOList(Repository.Select(where, orderBy, skip, take, associations));
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return result;
        }

        public new IEnumerable<TEntityDTO> Select(ZOperationResult operationResult,
            string where = null,
            object[] arguments = null,
            string orderBy = null,
            int? skip = null,
            int? take = null,
            string[] associations = null)
        {
            IEnumerable<TEntityDTO> result = new List<TEntityDTO>();

            try
            {
                if (IsSearch(operationResult))
                {
                    return ZDTOHelper<TEntityDTO, TEntity>.ToDTOList(Repository.Select(where, arguments, orderBy, skip, take, associations));
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return result;
        }

        public new IEnumerable<TEntityDTO> SelectAll(ZOperationResult operationResult)
        {
            IEnumerable<TEntityDTO> result = new List<TEntityDTO>();

            try
            {
                if (IsSearch(operationResult))
                {
                    result = ZDTOHelper<TEntityDTO, TEntity>.ToDTOList(Repository.SelectAll());
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return result;
        }

        public virtual bool Update(ZOperationResult operationResult, TEntityDTO entityDTO, bool isTransaction = true)
        {
            bool inTransaction = false;

            try
            {
                if (IsUpdate(operationResult))
                {
                    TEntity entity = (TEntity)entityDTO.ToData();

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
                                    // ???
                                    //LibraryHelper.Clone((TEntityDTO)Activator.CreateInstance(typeof(TEntityDTO), entity), entityDTO);
                                    entityDTO.FromData(entity);
                    
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
    }
}
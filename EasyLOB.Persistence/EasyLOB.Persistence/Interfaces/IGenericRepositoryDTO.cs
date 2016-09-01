using EasyLOB.Data;
using EasyLOB.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EasyLOB.Persistence
{
    public interface IGenericRepositoryDTO<TEntityDTO, TEntity>
        where TEntityDTO : class, IZDTOBase<TEntityDTO, TEntity>
    {
        #region Properties

        /// <summary>
        /// Z Data Dictionary.
        /// </summary>
        ZDataDictionaryAttribute DataDictionary { get; }

        /// <summary>
        /// Entity.
        /// </summary>
        string Entity { get; }

        /// <summary>
        /// Joins.
        /// </summary>
        int Joins { get; }

        /// <summary>
        /// IQueryable.
        /// </summary>
        IQueryable<TEntityDTO> Query { get; }

        /// <summary>
        /// Unit of Work.
        /// </summary>
        IUnitOfWorkDTO UnitOfWork { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Count.
        /// </summary>
        /// <param name="where">Where LINQ expression</param>
        /// <returns>Count</returns>
        int Count(Expression<Func<TEntityDTO, bool>> where);

        /// <summary>
        /// Count.
        /// </summary>
        /// <param name="where">Where Dynamic LINQ expression</param>
        /// <param name="args">Arguments</param>
        /// <returns>Ok ?</returns>
        int Count(string where, object[] args);

        /// <summary>
        /// Count ALL.
        /// </summary>
        /// <returns></returns>
        int CountAll();

        /// <summary>
        /// Create.
        /// </summary>
        /// <param name="operationResult">Operation Result</param>
        /// <param name="entity">Entity DTO</param>
        /// <returns>Ok ?</returns>
        bool Create(ZOperationResult operationResult, TEntityDTO entity);

        /// <summary>
        /// Delete.
        /// </summary>
        /// <param name="operationResult">Operation Result</param>
        /// <param name="entity">Entity DTO</param>
        /// <returns>Ok ?</returns>
        bool Delete(ZOperationResult operationResult, TEntityDTO entity);
        /*
        /// <summary>
        /// Delete by Id.
        /// </summary>
        /// <param name="operationResult">Operation Result</param>
        /// <param name="ids">Ids</param>
        /// <returns>Ok ?</returns>
        bool DeleteById(ZOperationResult operationResult, object[] ids);
         */
        /// <summary>
        /// Get.
        /// </summary>
        /// <param name="where">Where LINQ expression</param>
        /// <returns>Entity DTO</returns>
        TEntityDTO Get(Expression<Func<TEntityDTO, bool>> where);

        /// <summary>
        /// Get.
        /// </summary>
        /// <param name="where">Where Dynamic LINQ expression</param>
        /// <returns>Entity DTO</returns>
        TEntityDTO Get(string where);

        /// <summary>
        /// Get by Id.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Entity DTO</returns>
        TEntityDTO GetById(object id);

        /// <summary>
        /// Get by Id.
        /// </summary>
        /// <param name="ids">Ids</param>
        /// <returns>Entity DTO</returns>
        TEntityDTO GetById(object[] ids);

        /// <summary>
        /// Get Next DBMS Sequence.
        /// </summary>
        /// <returns>DBMS Sequence</returns>
        object GetNextSequence();

        /// <summary>
        /// Join.
        /// </summary>
        /// <param name="entity">Entity DTO</param>
        void Join(TEntityDTO entity);

        /// <summary>
        /// Join.
        /// </summary>
        /// <param name="enumerable">IEnumerable</param>
        void Join(IEnumerable<TEntityDTO> enumerable);

        /// <summary>
        /// Join.
        /// </summary>
        /// <param name="query">IQueryable</param>
        /// <returns>IQueryable</returns>
        IQueryable<TEntityDTO> Join(IQueryable<TEntityDTO> query);

        /// <summary>
        /// Join.
        /// </summary>
        /// <param name="query">IQueryable</param>
        /// <param name="associations">Associations LINQ expression</param>
        /// <returns></returns>
        IQueryable<TEntityDTO> Join(IQueryable<TEntityDTO> query, List<Expression<Func<TEntityDTO, object>>> associations = null);

        /// <summary>
        /// Join.
        /// </summary>
        /// <param name="query">IQueryable</param>
        /// <param name="associations">Associations</param>
        /// <returns>IQueryable</returns>
        IQueryable<TEntityDTO> Join(IQueryable<TEntityDTO> query, string[] associations = null);

        /// <summary>
        /// Select
        /// </summary>
        /// <param name="where">Where LINQ expression</param>
        /// <param name="orderBy">Order By LINQ expression</param>
        /// <param name="skip">Records to skip</param>
        /// <param name="take">Records to take</param>
        /// <param name="associations">Associations LINQ expression</param>
        /// <returns>IEnumerable</returns>
        IEnumerable<TEntityDTO> Select(Expression<Func<TEntityDTO, bool>> where = null,
            Func<IQueryable<TEntityDTO>, IOrderedQueryable<TEntityDTO>> orderBy = null,
            int? skip = null,
            int? take = null,
            List<Expression<Func<TEntityDTO, object>>> associations = null);

        /// <summary>
        /// Select.
        /// </summary>
        /// <param name="where">Where Dynamic LINQ expression</param>
        /// <param name="arguments">Arguments</param>
        /// <param name="orderBy">Order By Dynamic LINQ expression</param>
        /// <param name="skip">Records to skip</param>
        /// <param name="take">Records to take</param>
        /// <param name="associations">Associations</param>
        /// <returns>IEnumerable</returns>
        IEnumerable<TEntityDTO> Select(string where = null,
            object[] arguments = null,
            string orderBy = null,
            int? skip = null,
            int? take = null,
            string[] associations = null);

        /// <summary>
        /// Select ALL.
        /// </summary>
        /// <returns>IEnumerable</returns>
        IEnumerable<TEntityDTO> SelectAll();

        /// <summary>
        /// Set DBMS Sequence.
        /// </summary>
        /// <param name="value">Sequence = Id + 1</param>
        /// <returns></returns>
        void SetSequence(int value);

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="operationResult">Operation Result</param>
        /// <param name="entity">Entity DTO</param>
        /// <returns>Ok ?</returns>
        bool Update(ZOperationResult operationResult, TEntityDTO entity);

        #endregion Methods

        #region Triggers

        /// <summary>
        /// Before create Trigger.
        /// </summary>
        /// <param name="operationResult">Operation Result</param>
        /// <param name="entity">Entity DTO</param>
        /// <returns>Ok ?</returns>
        bool BeforeCreate(ZOperationResult operationResult, TEntityDTO entity);

        /// <summary>
        /// After create Trigger.
        /// </summary>
        /// <param name="operationResult">Operation Result</param>
        /// <param name="entity">Entity DTO</param>
        /// <returns>Ok ?</returns>
        bool AfterCreate(ZOperationResult operationResult, TEntityDTO entity);

        /// <summary>
        /// Before delete Trigger.
        /// </summary>
        /// <param name="operationResult">Operation Result</param>
        /// <param name="entity">Entity DTO</param>
        /// <returns>Ok ?</returns>
        bool BeforeDelete(ZOperationResult operationResult, TEntityDTO entity);

        /// <summary>
        /// After delete Trigger.
        /// </summary>
        /// <param name="operationResult">Operation Result</param>
        /// <param name="entity">Entity DTO</param>
        /// <returns>Ok ?</returns>
        bool AfterDelete(ZOperationResult operationResult, TEntityDTO entity);

        /// <summary>
        /// Before update Trigger.
        /// </summary>
        /// <param name="operationResult">Operation Result</param>
        /// <param name="entity">Entity DTO</param>
        /// <returns>Ok ?</returns>
        bool BeforeUpdate(ZOperationResult operationResult, TEntityDTO entity);

        /// <summary>
        /// After update Trigger.
        /// </summary>
        /// <param name="operationResult">Operation Result</param>
        /// <param name="entity">Entity DTO</param>
        /// <returns>Ok ?</returns>
        bool AfterUpdate(ZOperationResult operationResult, TEntityDTO entity);

        #endregion Triggers
    }
}
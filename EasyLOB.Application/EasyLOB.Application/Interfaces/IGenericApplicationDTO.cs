using EasyLOB.Data;
using EasyLOB.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EasyLOB.Application
{
    public interface IGenericApplicationDTO<TEntityDTO, TEntity> : IGenericApplication<TEntity>
        where TEntityDTO : class, IZDTOBase<TEntityDTO, TEntity>
        where TEntity : class, IZDataBase
    {
        #region Properties

        IQueryable<TEntityDTO> QueryDTO { get; }

        #endregion Properties

        #region Methods

        bool Create(ZOperationResult operationResult, TEntityDTO entityDTO, bool isTransaction = true);

        bool Delete(ZOperationResult operationResult, TEntityDTO entityDTO, bool isTransaction = true);

        new TEntityDTO Get(ZOperationResult operationResult, Expression<Func<TEntity, bool>> where);

        new TEntityDTO Get(ZOperationResult operationResult, string where);

        new TEntityDTO GetById(ZOperationResult operationResult, object id);

        new TEntityDTO GetById(ZOperationResult operationResult, object[] ids);

        new IEnumerable<TEntityDTO> Select(ZOperationResult operationResult,
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            List<Expression<Func<TEntity, object>>> associations = null);

        new IEnumerable<TEntityDTO> Select(ZOperationResult operationResult,
            string where = null,
            object[] arguments = null,
            string orderBy = null,
            int? skip = null,
            int? take = null,
            string[] associations = null);

        new IEnumerable<TEntityDTO> SelectAll(ZOperationResult operationResult);

        bool Update(ZOperationResult operationResult, TEntityDTO entityDTO, bool isTransaction = true);

        #endregion Methods
    }
}
﻿using EasyLOB.Data;
using EasyLOB.Library;
using Microsoft.OData.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace EasyLOB.Persistence
{
    public abstract class GenericRepositoryOData<TEntityDTO, TEntity> : IGenericRepositoryDTO<TEntityDTO, TEntity>
        where TEntityDTO : class, IZDTOBase<TEntityDTO, TEntity>
        where TEntity : class, IZDataBase
    {
        #region Properties

        public virtual ZDataDictionaryAttribute DataDictionary
        {
            get
            {
                Type type = this.GetRepositoryType<TEntityDTO, TEntity>();

                return DataHelper.GetDataDictionaryAttribute(type);
            }
        }

        public virtual string Entity
        {
            get { return typeof(TEntity).Name; }
        }

        public virtual int Joins
        {
            get { return 10; }
        }

        public virtual IQueryable<TEntityDTO> Query
        {
            get
            {
                IQueryable<TEntityDTO> query = Container.CreateQuery<TEntityDTO>(EntitySetName).AsQueryable<TEntityDTO>();
                query = Join(query);

                return query;
            }
        }

        public IUnitOfWorkDTO UnitOfWork { get; }

        #endregion Properties

        #region Properties OData

        public DataServiceContext Container { get; protected set; }

        public DataServiceQuery DataServiceQuery
        {
            get { return Container.CreateQuery<TEntityDTO>(EntitySetName); }
        }

        public string EntitySetName
        {
            // EntityDTO => Entity
            get { return Regex.Replace(typeof(TEntityDTO).Name, @"DTO$", String.Empty); }
            //get { return typeof(TEntityDTO).Name.Replace("DTO", ""); }
        }

        #endregion Properties OData

        #region Methods

        public GenericRepositoryOData(IUnitOfWorkDTO unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public virtual int Count(Expression<Func<TEntityDTO, bool>> where)
        {
            int result;

            if (where != null)
            {
                result = Query.Count(where);
            }
            else
            {
                result = DataServiceQuery.Count();
            }

            return result;
        }

        public virtual int Count(string where, object[] args = null)
        {
            int result;

            if (!String.IsNullOrEmpty(where))
            {
                if (args != null)
                {
                    result = Query.Where(where, args).Count();
                }
                else
                {
                    result = Query.Where(where).Count();
                }
            }
            else
            {
                result = DataServiceQuery.Count();
            }

            return result;
        }

        public virtual int CountAll()
        {
            return Query.Count();
        }

        public virtual bool Create(ZOperationResult operationResult, TEntityDTO entity)
        {
            try
            {
                if (UnitOfWork.BeforeCreate(operationResult, entity))
                {
                    if (BeforeCreate(operationResult, entity))
                    {
                        Container.AddObject(EntitySetName, entity);

                        if (AfterCreate(operationResult, entity))
                        {
                            UnitOfWork.AfterCreate(operationResult, entity);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseExceptionOData(exception);
            }

            return operationResult.Ok;
        }

        public virtual bool Delete(ZOperationResult operationResult, TEntityDTO entity)
        {
            try
            {
                if (UnitOfWork.BeforeDelete(operationResult, entity))
                {
                    if (BeforeDelete(operationResult, entity))
                    {
                        Container.DeleteObject(entity);

                        if (AfterDelete(operationResult, entity))
                        {
                            UnitOfWork.AfterDelete(operationResult, entity);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseExceptionOData(exception);
            }

            return operationResult.Ok;
        }

        public virtual TEntityDTO Get(Expression<Func<TEntityDTO, bool>> where)
        {
            return Query.Where(where).FirstOrDefault();
        }

        public virtual TEntityDTO Get(string where)
        {
            return Query.Where(where).FirstOrDefault();
        }

        public virtual TEntityDTO GetById(object id)
        {
            return GetById(new object[] { id });
        }

        public virtual TEntityDTO GetById(object[] ids)
        {
            string predicate = DataDictionary.LINQWhere;
            Expression<Func<TEntityDTO, bool>> where =
                System.Linq.Dynamic.DynamicExpression.ParseLambda<TEntityDTO, bool>(predicate, ids);

            return Get(where);
        }

        public virtual object GetNextSequence()
        {
            return null;
        }

        public virtual void Join(TEntityDTO entity)
        {
        }

        public virtual void Join(IEnumerable<TEntityDTO> enumerable)
        {
            if (enumerable != null)
            {
                int items = 0;
                foreach (TEntityDTO entity in enumerable)
                {
                    Join(entity);

                    if (++items >= Joins)
                    {
                        break;
                    }
                }
            }
        }

        public virtual IQueryable<TEntityDTO> Join(IQueryable<TEntityDTO> query)
        {
            return query;
        }

        public virtual IQueryable<TEntityDTO> Join(IQueryable<TEntityDTO> query, List<Expression<Func<TEntityDTO, object>>> associations)
        {
            return Join(query);
        }

        public virtual IQueryable<TEntityDTO> Join(IQueryable<TEntityDTO> query, string[] associations)
        {
            return Join(query);
        }

        public virtual IEnumerable<TEntityDTO> Select(Expression<Func<TEntityDTO, bool>> where = null,
            Func<IQueryable<TEntityDTO>, IOrderedQueryable<TEntityDTO>> orderBy = null,
            int? skip = null,
            int? take = null,
            List<Expression<Func<TEntityDTO, object>>> associations = null)
        {
            IQueryable<TEntityDTO> query = Query;

            if (where != null)
            {
                query = query.Where(where);
            }

            if (skip != null && orderBy == null)
            {
                query = query.OrderBy(DataDictionary.LINQOrderBy);
            }
            else if (orderBy != null)
            {
                query = orderBy(query);
            }

            // The method 'Skip' is only supported for sorted input in LINQ to Entities.
            // The method 'OrderBy' must be called before the method 'Skip'.
            if (skip != null && skip >= 0)
            {
                query = query.Skip((int)skip);
            }

            if (take != null && take > 0)
            {
                query = query.Take((int)take);
            }

            query = Join(query, associations);

            return query.ToList<TEntityDTO>();
        }

        public virtual IEnumerable<TEntityDTO> Select(string where = null,
            object[] args = null,
            string orderBy = null,
            int? skip = null,
            int? take = null,
            string[] associations = null)
        {
            IQueryable<TEntityDTO> query = Query;

            if (!String.IsNullOrEmpty(where))
            {
                if (args != null)
                {
                    query = query.Where(where, args);
                }
                else
                {
                    query = query.Where(where);
                }
            }

            //if (orderBy != null && orderBy.Contains("LookupText"))
            //{
            //    orderBy = null;
            //}

            if (skip != null && String.IsNullOrEmpty(orderBy))
            {
                query = query.OrderBy(DataDictionary.LINQOrderBy);
            }
            else if (!String.IsNullOrEmpty(orderBy))
            {
                query = query.OrderBy(orderBy);
            }

            // The method 'Skip' is only supported for sorted input in LINQ to Entities.
            // The method 'OrderBy' must be called before the method 'Skip'.
            if (skip != null && skip >= 0)
            {
                query = query.Skip((int)skip);
            }

            if (take != null && take > 0)
            {
                query = query.Take((int)take);
            }

            query = Join(query, associations);

            return query.ToList<TEntityDTO>();
        }

        public virtual IEnumerable<TEntityDTO> SelectAll()
        {
            return Query.ToList();
        }

        public virtual void SetSequence(int value)
        {
        }

        public virtual bool Update(ZOperationResult operationResult, TEntityDTO entity)
        {
            try
            {
                if (UnitOfWork.BeforeUpdate(operationResult, entity))
                {
                    if (BeforeUpdate(operationResult, entity))
                    {
                        Container.UpdateObject(entity);

                        if (AfterUpdate(operationResult, entity))
                        {
                            UnitOfWork.AfterUpdate(operationResult, entity);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseExceptionOData(exception);
            }

            return operationResult.Ok;
        }

        #endregion Methods

        #region Triggers

        public virtual bool BeforeCreate(ZOperationResult operationResult, TEntityDTO entity)
        {
            return operationResult.Ok;
        }

        public virtual bool AfterCreate(ZOperationResult operationResult, TEntityDTO entity)
        {
            return operationResult.Ok;
        }

        public virtual bool BeforeDelete(ZOperationResult operationResult, TEntityDTO entity)
        {
            return operationResult.Ok;
        }

        public virtual bool AfterDelete(ZOperationResult operationResult, TEntityDTO entity)
        {
            return operationResult.Ok;
        }

        public virtual bool BeforeUpdate(ZOperationResult operationResult, TEntityDTO entity)
        {
            return operationResult.Ok;
        }

        public virtual bool AfterUpdate(ZOperationResult operationResult, TEntityDTO entity)
        {
            return operationResult.Ok;
        }

        #endregion Triggers
    }
}
using EasyLOB.Data;
using EasyLOB.Library;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;

namespace EasyLOB.Persistence
{
    public abstract class GenericRepositoryRedis<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IZDataBase
    {
        #region Properties

        public virtual ZDataDictionaryAttribute DataDictionary
        {
            get
            {
                Type type = this.GetRepositoryType<TEntity>();

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

        public virtual IQueryable<TEntity> Query
        {
            get
            {
                IQueryable<TEntity> query = Client.As<TEntity>().GetAll().AsQueryable<TEntity>();
                query = Join(query);

                return query;
            }
        }

        public IUnitOfWork UnitOfWork { get; }

        #endregion Properties

        #region Properties Redis

        public IRedisClient Client { get; protected set; }

        public IRedisTypedClient<TEntity> TypedClient { get; }

        #endregion Properties Redis

        #region Methods

        public GenericRepositoryRedis(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public virtual int Count(Expression<Func<TEntity, bool>> where)
        {
            int result;

            if (where != null)
            {
                result = TypedClient.GetAll().AsQueryable<TEntity>().Where(where).Count();
            }
            else
            {
                result = TypedClient.GetAll().AsQueryable<TEntity>().Count();
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
                    result = TypedClient.GetAll().AsQueryable<TEntity>().Where(where, args).Count();
                }
                else
                {
                    result = TypedClient.GetAll().AsQueryable<TEntity>().Where(where).Count();
                }
            }
            else
            {
                result = TypedClient.GetAll().AsQueryable<TEntity>().Count();
            }

            return result;
        }

        public virtual int CountAll()
        {
            return TypedClient.GetAll().Count();
        }

        public virtual bool Create(ZOperationResult operationResult, TEntity entity)
        {
            try
            {
                if (UnitOfWork.BeforeCreate(operationResult, entity))
                {
                    if (BeforeCreate(operationResult, entity))
                    {
                        object id = GetNextSequence();
                        if (id != null)
                        {
                            (entity as ZDataBase).SetId(new object[] { id });
                        }

                        TypedClient.Store(entity);

                        if (AfterCreate(operationResult, entity))
                        {
                            UnitOfWork.AfterCreate(operationResult, entity);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseExceptionRedis(exception);
            }

            return operationResult.Ok;
        }

        public virtual bool Delete(ZOperationResult operationResult, TEntity entity)
        {
            try
            {
                if (UnitOfWork.BeforeDelete(operationResult, entity))
                {
                    if (BeforeDelete(operationResult, entity))
                    {
                        TypedClient.Delete(entity);

                        if (AfterDelete(operationResult, entity))
                        {
                            UnitOfWork.AfterDelete(operationResult, entity);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseExceptionRedis(exception);
            }

            return operationResult.Ok;
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> where)
        {
            TEntity entity = null;

            entity = Query.Where(where).FirstOrDefault();
            Join(entity);

            return entity;
        }

        public virtual TEntity Get(string where)
        {
            TEntity entity = null;

            entity = Query.Where(where).FirstOrDefault();
            Join(entity);

            return entity;
        }

        public virtual TEntity GetById(object id)
        {
            return GetById(new object[] { id });
        }

        public virtual TEntity GetById(object[] ids)
        {
            string predicate = DataDictionary.LINQWhere;
            Expression<Func<TEntity, bool>> where =
                System.Linq.Dynamic.DynamicExpression.ParseLambda<TEntity, bool>(predicate, ids);

            return Get(where);
        }

        public virtual object GetNextSequence()
        {
            object id = null;

            if (DataDictionary.IsIdentity)
            {
                id = (int)TypedClient.GetNextSequence();
            }

            return id;
        }

        public virtual void Join(TEntity entity)
        {
            // Redis Joins are client-side
            // Refer to \Application.PersistenceRedis\Repositories\DatabaseEntityRepositoryRedis.cs for Join() implementation
        }

        public virtual void Join(IEnumerable<TEntity> enumerable)
        {
            // Redis Joins are client-side

            if (enumerable != null)
            {
                int items = 0;
                foreach (TEntity entity in enumerable)
                {
                    Join(entity);

                    if (++items >= Joins)
                    {
                        break;
                    }
                }
            }
        }

        public virtual IQueryable<TEntity> Join(IQueryable<TEntity> query)
        {
            return query;
        }

        public virtual IQueryable<TEntity> Join(IQueryable<TEntity> query, List<Expression<Func<TEntity, object>>> associations)
        {
            return Join(query);
        }

        public virtual IQueryable<TEntity> Join(IQueryable<TEntity> query, string[] associations)
        {
            return Join(query);
        }

        public virtual IEnumerable<TEntity> Select(Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            List<Expression<Func<TEntity, object>>> associations = null)
        {
            IQueryable<TEntity> query = TypedClient.GetAll().AsQueryable<TEntity>();

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

            //query = Join(query, associations);

            IEnumerable<TEntity> enumerable = query.ToList<TEntity>();
            Join(enumerable);

            return enumerable;
        }

        public virtual IEnumerable<TEntity> Select(string where = null,
            object[] args = null,
            string orderBy = null,
            int? skip = null,
            int? take = null,
            string[] associations = null)
        {
            IQueryable<TEntity> query = TypedClient.GetAll().AsQueryable<TEntity>();

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

            //query = Join(query, associations);

            IEnumerable<TEntity> enumerable = query.ToList<TEntity>();
            Join(enumerable);

            return enumerable;
        }

        public virtual IEnumerable<TEntity> SelectAll()
        {
            IEnumerable<TEntity> enumerable = Query.ToList<TEntity>();
            Join(enumerable);

            return enumerable;
        }

        public void SetSequence(int value)
        {
            if (DataDictionary.IsIdentity)
            {
                TypedClient.SetSequence(value);
            }
        }

        public virtual bool Update(ZOperationResult operationResult, TEntity entity)
        {
            try
            {
                if (UnitOfWork.BeforeUpdate(operationResult, entity))
                {
                    if (BeforeUpdate(operationResult, entity))
                    {
                        TypedClient.Store(entity);

                        if (AfterUpdate(operationResult, entity))
                        {
                            UnitOfWork.AfterUpdate(operationResult, entity);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseExceptionRedis(exception);
            }

            return operationResult.Ok;
        }

        #endregion Methods

        #region Triggers

        public virtual bool BeforeCreate(ZOperationResult operationResult, TEntity entity)
        {
            return operationResult.Ok;
        }

        public virtual bool AfterCreate(ZOperationResult operationResult, TEntity entity)
        {
            return operationResult.Ok;
        }

        public virtual bool BeforeDelete(ZOperationResult operationResult, TEntity entity)
        {
            return operationResult.Ok;
        }

        public virtual bool AfterDelete(ZOperationResult operationResult, TEntity entity)
        {
            return operationResult.Ok;
        }

        public virtual bool BeforeUpdate(ZOperationResult operationResult, TEntity entity)
        {
            return operationResult.Ok;
        }

        public virtual bool AfterUpdate(ZOperationResult operationResult, TEntity entity)
        {
            return operationResult.Ok;
        }

        #endregion Triggers
    }
}
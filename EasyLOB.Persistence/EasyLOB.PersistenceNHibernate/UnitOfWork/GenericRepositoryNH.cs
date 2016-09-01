using EasyLOB.Data;
using EasyLOB.Library;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;

namespace EasyLOB.Persistence
{
    public abstract class GenericRepositoryNH<TEntity> : IGenericRepository<TEntity>
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
                IQueryable<TEntity> query = Session.Query<TEntity>();
                query = Join(query);

                return query;
            }
        }

        public IUnitOfWork UnitOfWork { get; }

        #endregion Properties

        #region Properties NHibernate

        public ISession Session { get; protected set; }

        #endregion Properties NHibernate

        #region Methods

        public GenericRepositoryNH(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public virtual int Count(Expression<Func<TEntity, bool>> where)
        {
            return Session.Query<TEntity>().Where(where).Count();
        }

        public virtual int Count(string where, object[] args = null)
        {
            int result = 0;

            if (!String.IsNullOrEmpty(where))
            {
                if (args != null)
                {
                    result = Session.Query<TEntity>().Where(where, args).Count();
                }
                else
                {
                    result = Session.Query<TEntity>().Where(where).Count();
                }
            }
            else
            {
                result = Session.Query<TEntity>().Count();
            }

            return result;
        }

        public virtual int CountAll()
        {
            return Session.Query<TEntity>().Count();
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

                        Session.Save(entity);

                        if (AfterCreate(operationResult, entity))
                        {
                            UnitOfWork.AfterCreate(operationResult, entity);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseExceptionNHibernate(exception);
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
                        Session.Delete(entity);

                        if (AfterDelete(operationResult, entity))
                        {
                            UnitOfWork.AfterDelete(operationResult, entity);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseExceptionNHibernate(exception);
            }

            return operationResult.Ok;
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> where)
        {
            return Session.Query<TEntity>().Where(where).FirstOrDefault();
        }

        public virtual TEntity Get(string where)
        {
            return Session.Query<TEntity>().Where(where).FirstOrDefault();
        }

        public virtual TEntity GetById(object id)
        {
            return GetById(new object[] { id });
        }

        public virtual TEntity GetById(object[] ids)
        {
            //if (ids.Length == 1)
            //{
            //    entity = Session.Load<TEntity>(ids[0]);
            //}
            //else
            //{
            //    throw new NotImplementedException("NHibernate GetById(objects[] ids)");
            //}

            string predicate = DataDictionary.LINQWhere;
            Expression<Func<TEntity, bool>> where =
                System.Linq.Dynamic.DynamicExpression.ParseLambda<TEntity, bool>(predicate, ids);

            return Get(where); // !!! Not.LazyLoad() is mandatory in "*Map.cs" to convert "EntityProxy" in "Entity"
        }

        public virtual object GetNextSequence()
        {
            object id = null;

            if (DataDictionary.IsIdentity && !PersistenceHelper.GeneratesIdentity(UnitOfWork.DBMS))
            {
                string sql = AdoNetHelper.GetSequenceSql(UnitOfWork.DBMS, this.GetType().Name);
                id = Session.CreateSQLQuery(sql).UniqueResult<object>();
            }

            return id;
        }

        public virtual void Join(TEntity entity)
        {
            // NHibernate Joins are server-side
        }

        public virtual void Join(IEnumerable<TEntity> enumerable)
        {
            /*
            // NHibernate Joins are server-side

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
             */
        }

        public virtual IQueryable<TEntity> Join(IQueryable<TEntity> query)
        {
            return Join(query, DataDictionary.Associations);
        }

        public virtual IQueryable<TEntity> Join(IQueryable<TEntity> query, List<Expression<Func<TEntity, object>>> associations)
        {
            associations = associations != null && associations.Count() > 0 ?
                associations : LambdaHelper<TEntity>.ToFuncProperty(DataDictionary.Associations);

            if (query != null && associations != null)
            {
                associations.ForEach(x => { query = query.Fetch(x); });

                //foreach (Expression<Func<TEntity, object>> association in associations)
                //{
                //    query = query.Fetch(association);
                //}
            }

            return query;
        }

        public virtual IQueryable<TEntity> Join(IQueryable<TEntity> query, string[] associations)
        {
            associations = associations != null && associations.Count() > 0 ?
                associations : DataDictionary.Associations;

            if (query != null && associations != null)
            {
                List<Expression<Func<TEntity, object>>> associationsExpression = associations != null && associations.Count() > 0 ?
                        LambdaHelper<TEntity>.ToFuncProperty(associations) :
                        LambdaHelper<TEntity>.ToFuncProperty(DataDictionary.Associations);
                query = Join(query, associationsExpression);
            }

            return query;
        }

        public virtual IEnumerable<TEntity> Select(Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            List<Expression<Func<TEntity, object>>> associations = null)
        {
            IQueryable<TEntity> query = Session.Query<TEntity>();

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

            return query.ToList<TEntity>();
        }

        public virtual IEnumerable<TEntity> Select(string where = null,
            object[] args = null,
            string orderBy = null,
            int? skip = null,
            int? take = null,
            string[] associations = null)
        {
            IQueryable<TEntity> query = Session.Query<TEntity>();

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

            return query.ToList<TEntity>();
        }

        public virtual IEnumerable<TEntity> SelectAll()
        {
            return Query.ToList<TEntity>();
        }

        public void SetSequence(int value)
        {
        }

        public virtual bool Update(ZOperationResult operationResult, TEntity entity)
        {
            try
            {
                if (UnitOfWork.BeforeUpdate(operationResult, entity))
                {
                    if (BeforeUpdate(operationResult, entity))
                    {
                        Session.Merge(entity);

                        if (AfterUpdate(operationResult, entity))
                        {
                            UnitOfWork.AfterUpdate(operationResult, entity);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseExceptionNHibernate(exception);
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
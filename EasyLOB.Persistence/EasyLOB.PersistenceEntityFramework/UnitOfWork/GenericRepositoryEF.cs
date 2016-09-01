using EasyLOB.Data;
using EasyLOB.Library;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;

namespace EasyLOB.Persistence
{
    public abstract class GenericRepositoryEF<TEntity> : IGenericRepository<TEntity>
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
                IQueryable<TEntity> query = Context.Set<TEntity>();
                query = Join(query);

                return query;
            }
        }

        public IUnitOfWork UnitOfWork { get; }

        #endregion Properties

        #region Properties Entity Framework

        public DbContext Context { get; protected set; }

        public IDbSet<TEntity> Set
        {
            get { return Context.Set<TEntity>(); }
        }

        #endregion Properties Entity Framework

        #region Methods

        public GenericRepositoryEF(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public virtual int Count(Expression<Func<TEntity, bool>> where)
        {
            int result;

            if (where != null)
            {
                result = Set.Where(where).Count();
            }
            else
            {
                result = Set.Count();
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
                    result = Set.Where(where, args).Count();
                }
                else
                {
                    result = Set.Where(where).Count();
                }
            }
            else
            {
                result = Set.Count();
            }

            return result;
        }

        public virtual int CountAll()
        {
            return Set.Count();
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

                        Set.Add(entity);

                        if (AfterCreate(operationResult, entity))
                        {
                            UnitOfWork.AfterCreate(operationResult, entity);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseExceptionEntityFramework(exception);
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
                        Context.Entry(entity).State = EntityState.Deleted;

                        if (AfterDelete(operationResult, entity))
                        {
                            UnitOfWork.AfterDelete(operationResult, entity);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseExceptionEntityFramework(exception);
            }

            return operationResult.Ok;
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> where)
        {
            return Query.Where(where).FirstOrDefault();
        }

        public virtual TEntity Get(string where)
        {
            return Query.Where(where).FirstOrDefault();
        }

        public virtual TEntity GetById(object id)
        {
            return GetById(new object[] { id });
        }

        public virtual TEntity GetById(object[] ids)
        {
            //TEntity entity = Set.Find(ids);

            string predicate = DataDictionary.LINQWhere;
            Expression<Func<TEntity, bool>> where =
                System.Linq.Dynamic.DynamicExpression.ParseLambda<TEntity, bool>(predicate, ids);

            return Get(where);
        }

        public virtual object GetNextSequence()
        {
            object id = null;

            if (DataDictionary.IsIdentity && !PersistenceHelper.GeneratesIdentity(UnitOfWork.DBMS))
            {
                string sql = AdoNetHelper.GetSequenceSql(UnitOfWork.DBMS, this.GetType().Name);
                if (!String.IsNullOrEmpty(sql))
                {
                    id = Context.Database.SqlQuery<object>(sql);
                }
            }

            return id;
        }

        public virtual void Join(TEntity entity)
        {
            // Entity Framework Joins are server-side
        }

        public virtual void Join(IEnumerable<TEntity> enumerable)
        {
            /*
            // Entity Framework Joins are server-side

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
                associations.ForEach(x => { query = query.Include(x); });

                //foreach (Expression<Func<TEntity, object>> association in associations)
                //{
                //    query = query.Include(association);
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
                associations.ToList<string>().ForEach(x => { query = query.Include(x); });

                //foreach (string association in associations)
                //{
                //    query = query.Include(association);
                //}
            }

            return query;
        }

        public virtual IEnumerable<TEntity> Select(Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            List<Expression<Func<TEntity, object>>> associations = null)
        {
            IQueryable<TEntity> query = Set;

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

            //return query.ToList<TEntity>();
            return query.AsNoTracking().ToList<TEntity>();
        }

        public virtual IEnumerable<TEntity> Select(string where = null,
            object[] args = null,
            string orderBy = null,
            int? skip = null,
            int? take = null,
            string[] associations = null)
        {
            IQueryable<TEntity> query = Set;

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

            //return query.ToList<TEntity>();
            return query.AsNoTracking().ToList<TEntity>();
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
                        //Set.Attach(entity);
                        //Context.Entry(entity).State = EntityState.Modified;

                        Context.Set<TEntity>().AddOrUpdate(entity); // System.Data.Entity.Migrations

                        //AddOrUpdate(entity);

                        if (AfterUpdate(operationResult, entity))
                        {
                            UnitOfWork.AfterUpdate(operationResult, entity);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseExceptionEntityFramework(exception);
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

        #region Methods Entity Framework
        /*
        public TEntity AddOrUpdate(TEntity entity)
        {
            // The key to AddOrUpdate
            // https://blog.oneunicorn.com/2012/05/03/the-key-to-addorupdate    

            var tracked = Context.Set<TEntity>().Find(entity.GetId());
            if (tracked != null)
            {
                Context.Entry(tracked).CurrentValues.SetValues(entity);
                return tracked;
            }
            else
            {
                Context.Set<TEntity>().Add(entity);

                return entity;
            }
        }
         */
        #endregion Methods Entity Framework
    }
}
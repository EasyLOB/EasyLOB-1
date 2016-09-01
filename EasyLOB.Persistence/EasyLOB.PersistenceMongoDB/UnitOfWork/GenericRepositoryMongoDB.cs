﻿using EasyLOB.Data;
using EasyLOB.Library;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;

namespace EasyLOB.Persistence
{
    public abstract class GenericRepositoryMongoDB<TEntity> : IGenericRepository<TEntity>
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
                IQueryable<TEntity> query = Collection.AsQueryable<TEntity>();
                query = Join(query);

                return query;
            }
        }

        public IUnitOfWork UnitOfWork { get; }

        #endregion Properties

        #region Properties MongoDB

        public IMongoDatabase Database { get; protected set; }

        public IMongoCollection<TEntity> Collection 
        {
            get { return Database.GetCollection<TEntity>(typeof(TEntity).Name); }
        }

        #endregion Properties MongoDB

        #region Methods

        public GenericRepositoryMongoDB(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public virtual int Count(Expression<Func<TEntity, bool>> where)
        {
            int result;

            if (where != null)
            {
                result = (int)Collection.Count(where);
            }
            else
            {
                result = Collection.AsQueryable<TEntity>().Count();
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
                    result = Collection.AsQueryable<TEntity>().Where(where, args).Count();
                }
                else
                {
                    result = (int)Collection.Count(where);
                }
            }
            else
            {
                result = Collection.AsQueryable<TEntity>().Count();
            }

            return result;
        }

        public virtual int CountAll()
        {
            return Collection.AsQueryable<TEntity>().Count();
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

                        Collection.InsertOne(entity);

                        if (AfterCreate(operationResult, entity))
                        {
                            UnitOfWork.AfterCreate(operationResult, entity);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseExceptionMongoDB(exception);
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
                        string predicate = DataDictionary.LINQWhere;
                        object[] ids = entity.GetId();
                        Expression<Func<TEntity, bool>> filter = System.Linq.Dynamic.DynamicExpression.ParseLambda<TEntity, bool>(predicate, ids);

                        Collection.DeleteOne<TEntity>(filter);

                        if (AfterDelete(operationResult, entity))
                        {
                            UnitOfWork.AfterDelete(operationResult, entity);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseExceptionMongoDB(exception);
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
                string name = typeof(TEntity).Name;

                var collection = Database.GetCollection<BsonDocument>(typeof(MongoDBSequence).Name);
                var filter = Builders<BsonDocument>.Filter.Eq("Name", name);
                var update = new BsonDocument("$inc", new BsonDocument { { "Value", 1 } });
                var document = collection.FindOneAndUpdateAsync(filter, update).Result;
                if (document != null)
                {
                    MongoDBSequence mongoDBSequence = BsonSerializer.Deserialize<MongoDBSequence>(document);
                    id = mongoDBSequence.Value + 1;
                }
                else
                {
                    id = 1;
                    collection.InsertOne(new BsonDocument { { "Name", name }, { "Value", 2 } });
                }
            }

            return id;
        }

        public virtual void Join(TEntity entity)
        {
            // MongoDB Joins are client-side
            // Refer to \Application.PersistenceMongoDB\Repositories\DatabaseEntityRepositoryMongoDB.cs for Join() implementation
        }

        public virtual void Join(IEnumerable<TEntity> enumerable)
        {
            // MongoDB Joins are client-side

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
            IQueryable<TEntity> query = Collection.AsQueryable<TEntity>();

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
            IQueryable<TEntity> query = Collection.AsQueryable<TEntity>();

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
                string name = typeof(TEntity).Name;

                IMongoCollection<MongoDBSequence> collection = Database.GetCollection<MongoDBSequence>(typeof(MongoDBSequence).Name);
                FilterDefinition<MongoDBSequence> filter = Builders<MongoDBSequence>.Filter.Eq("Name", name);

                // UPDATE OR INSERT
                collection.UpdateOneAsync(filter,
                    Builders<MongoDBSequence>.Update.Set(x => x.Value, value),
                    new UpdateOptions { IsUpsert = true });

                // REPLACE OR INSERT
                //collection.ReplaceOneAsync(filter,
                //    new MongoDBSequence() { Name = name, Value = value },
                //    new UpdateOptions { IsUpsert = true });
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
                        string predicate = DataDictionary.LINQWhere;
                        object[] ids = entity.GetId();
                        Expression<Func<TEntity, bool>> filter = System.Linq.Dynamic.DynamicExpression.ParseLambda<TEntity, bool>(predicate, ids);

                        Collection.ReplaceOne<TEntity>(filter, entity);

                        if (AfterUpdate(operationResult, entity))
                        {
                            UnitOfWork.AfterUpdate(operationResult, entity);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseExceptionMongoDB(exception);
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

    public class MongoDBSequence
    {
        [BsonId]
        public ObjectId DocumentId { get; set; }

        public string Name { get; set; }

        public int Value { get; set; }
    }
}
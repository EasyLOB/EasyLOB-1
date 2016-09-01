using Raven.Client;
using Raven.Client.Document;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;
using EasyLOB.Persistence;

// Install-Package RavenDB.Client

// Working with document identifiers
// https://ravendb.net/docs/article-page/3.0/csharp/client-api/document-identifiers/working-with-document-ids

// Global identifier generation conventions
// https://ravendb.net/docs/article-page/3.0/csharp/client-api/configuration/conventions/identifier-generation/global

// Working with document identifiers
// https://ravendb.net/docs/article-page/3.0/csharp/client-api/document-identifiers/working-with-document-ids#identity-ids

namespace EasyLOB.Persistence
{
    public abstract class UnitOfWorkRavenDB : IUnitOfWork
    {
        #region Properties

        private ZDatabaseLogger _databaseLogger;

        public ZDatabaseLogger DatabaseLogger
        {
            get
            {
                return _databaseLogger;
            }
            set
            {
                _databaseLogger = value;
            }
        }

        public ZDBMS DBMS
        {
            get { return ZDBMS.RavenDB; }
        }

        public string Domain { get; protected set; }

        public IDictionary<Type, object> Repositories { get; }

        #endregion Properties

        #region Properties RavenDB

        public IDocumentStore DocumentStore { get; }

        public IDocumentSession DocumentSession { get; }

        #endregion Properties RavenDB

        #region Methods

        public UnitOfWorkRavenDB(string url, string databaseName)
        {
            DocumentStore = new DocumentStore
            {
                Url = url,
                DefaultDatabase = databaseName
            };

            // Key = entity/1
            //DocumentStore.Conventions
            //    .DocumentKeyGenerator = (dbname, commands, entity) => _documentStore.Conventions.GetTypeTagName(entity.GetType()) + "/";

            // Identity = Id
            //DocumentStore.Conventions
            //    .FindIdentityProperty = property => property.Name == "Id";

            // entity/1 instead of entities/1
            DocumentStore.Conventions
                .FindTypeTagName = type => type.Name;

            DocumentStore.Initialize();

            DocumentSession = DocumentStore.OpenSession();

            DatabaseLogger = ZDatabaseLogger.None;
            Domain = "";
            Repositories = new Dictionary<Type, object>();
        }

        public virtual void Dispose()
        {
        }

        public virtual bool BeginTransaction(ZOperationResult operationResult, bool isTransaction = true, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return operationResult.Ok;
        }

        public virtual bool CommitTransaction(ZOperationResult operationResult, bool isTransaction = true)
        {
            return operationResult.Ok;
        }

        public virtual int ExecuteSQL(string sql)
        {
            return 0;
        }

        public ZDataDictionaryAttribute GetDataDictionary<TEntity>()
            where TEntity : class, IZDataBase
        {
            return GetRepository<TEntity>().DataDictionary;
        }

        public virtual IQueryable<TEntity> GetQuery<TEntity>()
            where TEntity : class, IZDataBase
        {
            return DocumentSession.Query<TEntity>();
        }

        public virtual IGenericRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class, IZDataBase
        {
            throw new NotImplementedException("abstract class Entity Framework UnitOfWork.GetRepository()");

            //if (!Repositories.Keys.Contains(typeof(TEntity)))
            //{
            //    var repository = new GenericRepository<TEntity>(Context);
            //    Repositories.Add(typeof(TEntity), repository);
            //}

            //return Repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
        }

        public virtual bool RollbackTransaction(ZOperationResult operationResult, bool isTransaction = true)
        {
            return operationResult.Ok;
        }

        public virtual bool Save(ZOperationResult operationResult)
        {
            try
            {
                DocumentSession.SaveChanges();
            }
            catch (Exception exception)
            {
                operationResult.ParseExceptionRavenDB(exception);
            }

            return operationResult.Ok;
        }

        #endregion Methods

        #region Triggers

        public virtual bool BeforeCreate(ZOperationResult operationResult, object entity)
        {
            return operationResult.Ok;
        }

        public virtual bool AfterCreate(ZOperationResult operationResult, object entity)
        {
            return operationResult.Ok;
        }

        public virtual bool BeforeDelete(ZOperationResult operationResult, object entity)
        {
            return operationResult.Ok;
        }

        public virtual bool AfterDelete(ZOperationResult operationResult, object entity)
        {
            return operationResult.Ok;
        }

        public virtual bool BeforeUpdate(ZOperationResult operationResult, object entity)
        {
            return operationResult.Ok;
        }

        public virtual bool AfterUpdate(ZOperationResult operationResult, object entity)
        {
            return operationResult.Ok;
        }

        #endregion Triggers
    }
}
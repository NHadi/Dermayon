using Dermayon.Infrastructure.Data.MongoRepositories.Contracts;
using Dermayon.Infrastructure.Data.MongoRepositories.UoW;
using MongoDB.Driver;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dermayon.Infrastructure.Data.MongoRepositories
{
    public class MongoDbRepository<TContext, TEntity> : IMongoDbRepository<TContext, TEntity> 
        where TContext : MongoContext
        where TEntity : class
    {
        protected readonly IUnitOfWorkMongo<TContext> UoW;
        protected IMongoCollection<TEntity> DbSet;
        public MongoDbRepository(TContext context)
        {
            UoW = new UnitOfWorkMongo<TContext>(context);
            DbSet = context.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public virtual void Delete(TEntity entity)
        => UoW.Context.AddCommand(() => DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", entity.GetId())));

        public virtual async Task DeleteEvent(TEntity entity, CancellationToken cancellationToken)
        => await DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", entity.GetId()), cancellationToken);

        public virtual void DeleteRange(List<TEntity> entities)
        => UoW.Context.AddCommand(() => DbSet.DeleteManyAsync(Builders<TEntity>.Filter.In("_id", entities.Select(x => x.GetId()))));

        public virtual async Task DeleteRangeEvent(List<TEntity> entities, CancellationToken cancellationToken)
        => await DbSet.DeleteManyAsync(Builders<TEntity>.Filter.In("_id", entities.Select(x => x.GetId())), cancellationToken);

        public virtual IEnumerable<TEntity> Get()
        => DbSet.Find(Builders<TEntity>.Filter.Empty).ToList();

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool withTracking = true)
        => DbSet.Find(predicate).ToList();

        public virtual async Task<IEnumerable<TEntity>> GetAsync()
        => (await DbSet.FindAsync(Builders<TEntity>.Filter.Empty)).ToList();

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, bool withTracking = true)
        => (await DbSet.FindAsync(Builders<TEntity>.Filter.Empty)).ToList();

        public virtual TEntity GetById(object id)
        => DbSet.Find(Builders<TEntity>.Filter.Eq("_id", id)).SingleOrDefault();

        public virtual async Task<TEntity> GetByIdAsync(object id)
        => (await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("_id", id))).SingleOrDefault();


        public virtual void Insert(TEntity entitiy)
        => UoW.Context.AddCommand(() => DbSet.InsertOneAsync(entitiy));

        public virtual async Task InsertEvent(TEntity entitiy, CancellationToken cancellationToken)
        => await DbSet.InsertOneAsync(entitiy, new InsertOneOptions { BypassDocumentValidation = false }, cancellationToken);

        public virtual void InsertRange(List<TEntity> entities)
        => UoW.Context.AddCommand(() => DbSet.InsertManyAsync(entities));

        public virtual async Task InsertRangeEvent(List<TEntity> entities, CancellationToken cancellationToken)
        => await DbSet.InsertManyAsync(entities, new InsertManyOptions { BypassDocumentValidation = false }, cancellationToken);

        public virtual void Update(TEntity entitiy)
         => UoW.Context.AddCommand(() => DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", entitiy.GetId()), entitiy));

        public async Task UpdateEvent(TEntity entitiy, CancellationToken cancellationToken)
        => await DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", entitiy.GetId()), entitiy, new UpdateOptions { IsUpsert = false }, cancellationToken);

        public virtual void UpdateRange(List<TEntity> entities)
        => entities.ForEach(item => {
            UoW.Context.AddCommand(() => DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", item.GetId()), item));
        });

        public async Task UpdateRangeEvent(List<TEntity> entities, CancellationToken cancellationToken)
        {
            List<Func<Task>> updatedTasks = new List<Func<Task>>();

            entities.ForEach(item =>
            {
                updatedTasks.Add(() => DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", item.GetId()), item, new UpdateOptions { IsUpsert = false }, cancellationToken));
            });

            var commandTasks = updatedTasks.Select(c => c());

            await Task.WhenAll(commandTasks);
        }
    }
}

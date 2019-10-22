using Dermayon.Common.Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dermayon.Common.Infrastructure.Data.Repositories.MongoDb
{
    public class MongoDbRepository<TEntity> : IMongoDbRepository<TEntity> where TEntity : EntityBase
    {
        protected readonly IMongoContext Context;
        protected IMongoCollection<TEntity> DbSet;
        public MongoDbRepository(IMongoContext context)
        {
            Context = context;
            DbSet = context.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public virtual void Delete(TEntity entity)
        => Context.AddCommand(async () => await DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", entity.Id)));

        public virtual void DeleteRange(List<TEntity> entities)
        => Context.AddCommand(async () => await DbSet.DeleteManyAsync(Builders<TEntity>.Filter.In("_id", entities.Select(x => x.Id))));

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
        => Context.AddCommand(async () => await DbSet.InsertOneAsync(entitiy));

        public virtual void InsertRange(List<TEntity> entities)
        => Context.AddCommand(async () => await DbSet.InsertManyAsync(entities));

        public virtual void Update(TEntity entitiy)
         => Context.AddCommand(async () => await DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", entitiy.Id), entitiy));

        public virtual void UpdateRange(List<TEntity> entities) 
        => entities.ForEach(item =>{
            Context.AddCommand(async () => await DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", item.Id), item));
        });
    }
}

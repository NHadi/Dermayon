using Dermayon.Common.Events;
using Dermayon.Common.Infrastructure.Data.Contracts;
using Dermayon.Infrastructure.Data.MongoRepositories.UoW;
using Dermayon.Infrastructure.Data.MongoRepositories.Contracts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dermayon.Infrastructure.Data.MongoRepositories;
using ServiceStack;
using System.Linq;

namespace Dermayon.Infrastructure.Data.EventSources
{
    public class EventSourceRepository<TContext, TEvent> : IEventRepository<TContext, TEvent>
       where TContext : MongoContext
       where TEvent : IEvent
    {
        protected readonly IDermayonUnitOfWork<TContext> UoW;
        protected IMongoCollection<TEvent> DbSet;

        public EventSourceRepository(TContext context)
        {
            UoW = new UnitOfWorkMongo<TContext>(context);
            DbSet = context.GetCollection<TEvent>(typeof(TEvent).Name);
        }

        public virtual async Task DeleteEvent(TEvent @event, CancellationToken cancellationToken)
        => await DbSet.DeleteOneAsync(Builders<TEvent>.Filter.Eq("_id", @event.GetId()), cancellationToken);

        public virtual async Task DeleteRangeEvent(List<TEvent> events, CancellationToken cancellationToken)
        => await DbSet.DeleteManyAsync(Builders<TEvent>.Filter.In("_id", events.Select(x => x.GetId())), cancellationToken);

        public virtual async Task InserEvent(TEvent @event, CancellationToken cancellationToken)
        => await DbSet.InsertOneAsync(@event, new InsertOneOptions { BypassDocumentValidation = false }, cancellationToken);

        public virtual async Task InsertRangeEvent(List<TEvent> events, CancellationToken cancellationToken)
        => await DbSet.InsertManyAsync(events, new InsertManyOptions { BypassDocumentValidation = false }, cancellationToken);

        public virtual async Task UpdateEvent(TEvent @event, CancellationToken cancellationToken)
        => await DbSet.ReplaceOneAsync(Builders<TEvent>.Filter.Eq("_id", @event.GetId()), @event, new UpdateOptions { IsUpsert = false }, cancellationToken);

        public virtual async Task UpdateRangeEvent(List<TEvent> events, CancellationToken cancellationToken)
        {
            List<Func<Task>> updatedTasks = new List<Func<Task>>();

            events.ForEach(item =>
            {
                updatedTasks.Add(() => DbSet.ReplaceOneAsync(Builders<TEvent>.Filter.Eq("_id", item.GetId()), item, new UpdateOptions { IsUpsert = false }, cancellationToken));
            });

            var commandTasks = updatedTasks.Select(c => c());

            await Task.WhenAll(commandTasks);
        }
    }
}

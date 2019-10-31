using Dermayon.Common.CrossCutting;
using Dermayon.Infrastructure.Data.MongoRepositories.Contracts;
using EnsureThat;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dermayon.Infrastructure.Data.MongoRepositories
{
    public class MongoContext
    {
        protected readonly IMongoDatabase Database;
        protected readonly List<Func<Task>> _commands;

        public IClientSessionHandle Session { get; set; }
        public MongoClient MongoClient { get; set; }

        public MongoContext(MongoDbSettings settings)
        {
            Ensure.That(settings).IsNotNull();

            MongoClient = new MongoClient(settings.ServerConnection);

            Database = MongoClient.GetDatabase(settings.Database,
                new MongoDatabaseSettings
                {
                    GuidRepresentation = GuidRepresentation.Standard
                });

            // Every command will be stored and it'll be processed at SaveChanges
            _commands = new List<Func<Task>>();
        }

        public MongoContext(MongoCredentialDbSettings settings)
        {
            Ensure.That(settings).IsNotNull();

            MongoClient = new MongoClient(settings.MongoClientSettings);


            Database = MongoClient.GetDatabase(settings.Database,
                new MongoDatabaseSettings
                {
                    GuidRepresentation = GuidRepresentation.Standard
                });

            // Every command will be stored and it'll be processed at SaveChanges
            _commands = new List<Func<Task>>();
        }

        public virtual async Task<int> SaveChanges()
        {
            var result = _commands.Count;

            using (Session = await MongoClient.StartSessionAsync())
            {
                Session.StartTransaction();

                try
                {
                    var commandTasks = _commands.Select(c => c());

                    await Task.WhenAll(commandTasks);

                    await Session.CommitTransactionAsync();
                }
                catch (Exception ex)
                {
                    await Session.AbortTransactionAsync();
                    _commands.Clear();
                    throw ex;
                }
            }

            _commands.Clear();
            return result;
        }

        public virtual IMongoCollection<T> GetCollection<T>(string name)
        {
            return Database.GetCollection<T>(name);
        }

        public virtual void Dispose()
        {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }

        public virtual void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }
    }
}

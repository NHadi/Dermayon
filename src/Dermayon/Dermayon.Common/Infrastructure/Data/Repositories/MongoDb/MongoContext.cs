using Dermayon.Common.CrossCutting;
using EnsureThat;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dermayon.Common.Infrastructure.Data.Repositories.MongoDb
{
    public class MongoContext : IMongoContext
    {        
        protected readonly IMongoDatabase Database;
        protected readonly List<Func<Task>> _commands;        
        protected readonly ILog _log;

        public IClientSessionHandle Session { get; set; }
        public MongoClient MongoClient { get; set; }

        public MongoContext(MongoDbSettings settings, ILog log)
        {
            _log = log;

            Ensure.That(settings).IsNotNull();
            Ensure.String.IsNotNullOrEmpty(settings.ServerConnection);
            Ensure.String.IsNotNullOrEmpty(settings.Database);

            var client = new MongoClient(settings.ServerConnection);

            Database = client.GetDatabase(settings.Database,
                new MongoDatabaseSettings
                {
                    GuidRepresentation = GuidRepresentation.Standard
                });

            // Every command will be stored and it'll be processed at SaveChanges
            _commands = new List<Func<Task>>();
        }


        public async Task<int> SaveChanges()
        {
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
                    _log.Error("Error writing to MongoDB:", ex);
                    await Session.AbortTransactionAsync();
                    return 0;                    
                }
                
            }
            return _commands.Count;
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return Database.GetCollection<T>(name);
        }

        public void Dispose()
        {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }

        public void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }
    }
}

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

            if (settings.Credential == null)
            {
                MongoClient = new MongoClient(settings.ServerConnection);
            }
            else
            {
                MongoClient = new MongoClient(new MongoClientSettings
                {
                    Server = MongoServerAddress.Parse(settings.ServerConnection),
                    Credential = MongoCredential.CreateCredential(settings.Credential.Db, settings.Credential.User, settings.Credential.Password),
                    AllowInsecureTls = true,
                    SslSettings = new SslSettings
                    {
                        CheckCertificateRevocation = false
                    },
                    UseTls = false
                });
            }

           

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

                var commandTasks = _commands.Select(c => c());

                await Task.WhenAll(commandTasks);

                await Session.CommitTransactionAsync();
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

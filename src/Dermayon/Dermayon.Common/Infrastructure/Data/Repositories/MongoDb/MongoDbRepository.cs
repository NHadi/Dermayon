using EnsureThat;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dermayon.Common.Infrastructure.Data.Repositories.MongoDb
{
    public class MongoDbRepository
    {
        protected readonly IMongoDatabase MongoDatabase;
        public MongoDbRepository(MongoDbSettings settings)
        {
            Ensure.That(settings).IsNotNull();
            Ensure.String.IsNotNullOrEmpty(settings.ServerConnection);
            Ensure.String.IsNotNullOrEmpty(settings.Database);

            var client = new MongoClient(settings.ServerConnection);

            MongoDatabase = client.GetDatabase(settings.Database,
                new MongoDatabaseSettings
                {
                    GuidRepresentation = GuidRepresentation.Standard
                });
        }
    }
}

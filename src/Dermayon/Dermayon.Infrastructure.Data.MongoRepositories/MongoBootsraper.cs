using Dermayon.Infrastructure.Data.MongoRepositories.Contracts;
using Dermayon.Infrastructure.Data.MongoRepositories.UoW;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dermayon.Infrastructure.Data.MongoRepositories
{
    public class MongoBootsraper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IMongoDbRepository<,>), typeof(MongoDbRepository<,>));
            services.AddTransient(typeof(IUnitOfWorkMongo<>), typeof(UnitOfWorkMongo<>));
        }
    }
}

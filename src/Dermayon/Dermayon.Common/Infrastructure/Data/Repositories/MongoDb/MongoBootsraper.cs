using Dermayon.Common.Infrastructure.Data.Repositories.MongoDb.UoW;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dermayon.Common.Infrastructure.Data.Repositories.MongoDb
{
    public class MongoBootsraper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IMongoContext, MongoContext>();
            services.AddScoped(typeof(IMongoDbRepository<>), typeof(MongoDbRepository<>));
            services.AddScoped(typeof(IUnitOfWorkMongo<>), typeof(UnitOfWorkMongo<>));
        }
    }
}

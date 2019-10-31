using Dermayon.Infrastructure.Data.MongoRepositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MongoDbBuilder
    {
        public static IServiceCollection RegisterMongoContext<TContext, TSetting>(this IServiceCollection services, TSetting setting)
          where TContext : MongoContext
          where TSetting : class
        {
            services.AddSingleton(typeof(TContext));
            services.AddSingleton(setting);
            return services;
        }

    }
}

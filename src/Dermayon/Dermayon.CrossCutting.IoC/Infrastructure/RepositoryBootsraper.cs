using Dermayon.Infrastructure.Data.DapperRepositories;
using Dermayon.Infrastructure.Data.EFRepositories;
using Dermayon.Infrastructure.Data.MongoRepositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RepositoryBootsraper
    {
        public static IServiceCollection RegisterMongo(this IServiceCollection Services)
        {
            MongoBootsraper.RegisterServices(Services);
            return Services;
        }


        public static IServiceCollection RegisterEf(this IServiceCollection Services)
        {
            EfBootsraper.RegisterServices(Services);
            return Services;
        }

        public static IServiceCollection RegisterDapper(this IServiceCollection Services)
        {
            DapperBootsraper.RegisterServices(Services);
            return Services;
        }

    }
}

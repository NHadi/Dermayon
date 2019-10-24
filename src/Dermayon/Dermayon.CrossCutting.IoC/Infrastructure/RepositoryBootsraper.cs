using Dermayon.Infrastructure.Data.DapperRepositories;
using Dermayon.Infrastructure.Data.EFRepositories;
using Dermayon.Infrastructure.Data.MongoRepositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dermayon.CrossCutting.IoC.Infrastructure
{
    public class RepositoryBootsraper
    {
        public RepositoryBootsraper RegisterMongo(IServiceCollection Services, Action<MongoBootsraper> action = null)
        {
            Services.PostConfigure(action);
            MongoBootsraper.RegisterServices(Services);
            return this;
        }

        public RepositoryBootsraper RegisterEf(IServiceCollection Services, Action<EfBootsraper> action = null)
        {
            EfBootsraper.RegisterServices(Services);
            Services.PostConfigure(action);
            return this;
        }

        public RepositoryBootsraper RegisterDapper(IServiceCollection Services, Action<DapperBootsraper> action = null)
        {
            Services.PostConfigure(action);
            DapperBootsraper.RegisterServices(Services);
            return this;
        }

    }
}

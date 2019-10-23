using Dermayon.Common.Infrastructure.Data.Repositories.Dapper;
using Dermayon.Common.Infrastructure.Data.Repositories.EntityFramework;
using Dermayon.Common.Infrastructure.Data.Repositories.MongoDb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Dermayon.Common.Infrastructure.Data
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

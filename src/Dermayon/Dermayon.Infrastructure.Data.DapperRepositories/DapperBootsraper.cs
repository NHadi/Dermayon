using Dermayon.Infrastructure.Data.DapperRepositories.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dermayon.Infrastructure.Data.DapperRepositories
{
    public class DapperBootsraper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IDapperRepository<>), typeof(DapperRepository<>));
        }
    }
}

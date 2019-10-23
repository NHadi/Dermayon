using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dermayon.Common.Infrastructure.Data.Repositories.Dapper
{
    public class DapperBootsraper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IDapperRepository<>), typeof(DapperRepository<>));
        }
    }
}

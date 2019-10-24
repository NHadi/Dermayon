using Dermayon.Infrastructure.Data.EFRepositories.Contracts;
using Dermayon.Infrastructure.Data.EFRepositories.UoW;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dermayon.Infrastructure.Data.EFRepositories
{
    public class EfBootsraper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IEfRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }
    }
}

using Dermayon.Common.Domain;
using Dermayon.Common.Infrastructure.Data.Repositories.EntityFramework.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Text;

namespace Dermayon.Common.Infrastructure.Data.Repositories.EntityFramework
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

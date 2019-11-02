using Dermayon.Common.Infrastructure.Data.Contracts;
using Dermayon.Infrastructure.Data.MongoRepositories.UoW;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dermayon.Infrastructure.Data.EventSources
{
    public class EventSourceRepositoryBootsraper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IEventRepository<,>), typeof(EventSourceRepository<,>));
            services.AddTransient(typeof(IDermayonUnitOfWork<>), typeof(UnitOfWorkMongo<>));
        }
    }
}

using Dermayon.Common.CrossCutting;
using Dermayon.Common.Infrastructure.Data;
using Dermayon.Common.Infrastructure.Data.Contracts;
using Dermayon.CrossCutting.IoC.Infrastructure;
using Dermayon.Infrastructure.EvenMessaging.Kafka;
using Dermayon.Infrastructure.EvenMessaging.Kafka.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace Dermayon.CrossCutting.IoC
{
    public class DermayonBootsraper
    {
        public readonly IServiceCollection Services;
        protected readonly IConfiguration Configuration;
        public DermayonBootsraper(IServiceCollection services, IConfiguration configuration)
        {
            Services = services;
            Configuration = configuration;
        }
        public DermayonBootsraper InitBootsraper()
        {
            var log = new Log();
            Services.AddSingleton<ILog>(log);
            return this;
        }
        public DermayonBootsraper InitRepositoryBootsraper(Action<RepositoryBootsraper> Repository = null)
        {
            Services.AddTransient<IDbConectionFactory, DbConectionFactory>();
            Services.PostConfigure(Repository);
            return this;
        }

        public DermayonBootsraper InitKafka(Action<KafkaEventConsumerConfiguration> Consumer = null)
        {
            Services.PostConfigure(Consumer);

            Services.AddSingleton<IHostedService, KafkaConsumer>();
            
            Services.PostConfigure<KafkaEventProducerConfiguration>(options =>
            {
                options.SerializerSettings =
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            });

            Services.AddTransient<IKakfaProducer, KafkaProducer>();

            return this;
        }
    }
}

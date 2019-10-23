using Dermayon.Common.CrossCutting;
using Dermayon.Common.Infrastructure.Data;
using Dermayon.Common.Infrastructure.Data.Repositories.Dapper;
using Dermayon.Common.Infrastructure.Data.Repositories.EntityFramework;
using Dermayon.Common.Infrastructure.Data.Repositories.MongoDb;
using Dermayon.Common.Infrastructure.EventMessaging.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;

namespace Dermayon.Common
{
    public class DermayonBootsraper
    {
        public readonly IServiceCollection Services;
        protected readonly IConfiguration Configuration;
        public DermayonBootsraper(IServiceCollection services, string path, string environtment)
        {
            Services = services;

            
            var configurationBuilder = new ConfigurationBuilder()            
                .SetBasePath(path)
                .AddJsonFile("dermayonAppConfig.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"dermayonAppConfig.{environtment}.json", reloadOnChange: true, optional: true);

            Configuration = configurationBuilder.Build();
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
            Services.Configure<KafkaEventConsumerConfiguration>(Configuration.GetSection("KafkaConsumer"));
            Services.PostConfigure(Consumer);

            Services.AddSingleton<IHostedService, KafkaConsumer>();

            Services.Configure<KafkaEventProducerConfiguration>(Configuration.GetSection("KafkaProducer"));
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

using Dermayon.Common.CrossCutting;
using Dermayon.Common.Infrastructure.Data;
using Dermayon.Common.Infrastructure.Data.Contracts;
using Dermayon.Infrastructure.EvenMessaging.Kafka;
using Dermayon.Infrastructure.EvenMessaging.Kafka.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DermayonBootsraper
    {
        public static IServiceCollection InitDermayonBootsraper(this IServiceCollection services)
        {
            var log = new Log();
            services.AddSingleton<ILog>(log);

            services.AddTransient<IDbConectionFactory, DbConectionFactory>();
            return services;
        }

        public static IServiceCollection InitKafka(this IServiceCollection services)
        {
            services.AddSingleton<IHostedService, KafkaConsumer>();

            services.PostConfigure<KafkaEventProducerConfiguration>(options =>
            {
                options.SerializerSettings =
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            });

            services.AddTransient<IKakfaProducer, KafkaProducer>();

            return services;
        }        


    }
}

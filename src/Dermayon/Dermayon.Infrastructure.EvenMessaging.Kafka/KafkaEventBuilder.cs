using Dermayon.Common.Domain;
using Dermayon.Common.Events;
using Dermayon.Common.Infrastructure.EventMessaging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dermayon.Infrastructure.EvenMessaging.Kafka
{
    public static class KafkaEventBuilder
    {
        public static IServiceCollection RegisterKafkaConsumer<TEvent, TEventHandler>(this IServiceCollection services)
            where TEvent : IEvent
            where TEventHandler : IServiceEventHandler
        {
            services.PostConfigure<KafkaEventConsumerConfiguration>(options =>
            {
                options.RegisterConsumer<TEvent, TEventHandler>();
            });
            return services;
        }        
        //public static IServiceCollection RegisterKafkaPublisher<TCommand, TCommandHandler>(this IServiceCollection services)
        //   where TCommand : ICommand
        //   where TCommandHandler : ICommandHandler<ICommand>
        //{
        //    services.AddTransient<TCommand, TCommandHandler>();
        //    return services;
        //}
    }
}

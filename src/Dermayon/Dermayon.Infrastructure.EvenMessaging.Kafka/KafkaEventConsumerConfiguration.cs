using Dermayon.Common.Events;
using Dermayon.Common.Infrastructure.EventMessaging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Dermayon.Infrastructure.EvenMessaging.Kafka
{
    public class KafkaEventConsumerConfiguration
    {
        public IDictionary<string, Type> Handlers { get; set; } = new Dictionary<string, Type>();
        public List<string> Topics { get; set; }
        public string GroupId { get; set; }
        public string Server { get; set; }
        public TimeSpan Timeout { get; set; }

        public KafkaEventConsumerConfiguration RegisterConsumer<TEvent, TEventHandler>()
            where TEvent : IEvent
            where TEventHandler : IServiceEventHandler
        {
            var eventName = typeof(TEvent).GetCustomAttribute<EventAttribute>()?.Name;
            if (string.IsNullOrEmpty(eventName))
            {
                throw new InvalidOperationException($"{nameof(EventAttribute)} missing on {typeof(TEvent).Name}");
            }
            Handlers[eventName] = typeof(TEventHandler);
            return this;
        }
    }
}

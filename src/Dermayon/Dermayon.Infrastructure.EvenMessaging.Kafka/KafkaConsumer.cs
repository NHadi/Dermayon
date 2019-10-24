using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Dermayon.Common.CrossCutting;
using Dermayon.Common.Events;
using Dermayon.Common.Infrastructure.EventMessaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dermayon.Infrastructure.EvenMessaging.Kafka
{
    public class KafkaConsumer : HostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILog _log;
        private readonly KafkaEventConsumerConfiguration _options;

        public KafkaConsumer(IOptions<KafkaEventConsumerConfiguration> options, IServiceProvider serviceProvider, ILog log)
        {
            _options = options.Value;
            _serviceProvider = serviceProvider;
            _log = log;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var config = new Dictionary<string, object>
            {
                {"group.id", _options.GroupId},
                {"bootstrap.servers", _options.Server},

            };

            foreach (var topic in _options.Topics)
            {
                var consumer = new Consumer<Ignore, string>(config, new IgnoreDeserializer(), new StringDeserializer(Encoding.UTF8));
                consumer.OnConsumeError += ConsumerOnOnConsumeError;
                consumer.OnError += ConsumerOnOnError;
                consumer.Subscribe(topic);

                Task.Run(async () => await Consume(consumer, cancellationToken), cancellationToken);
            }

            return Task.CompletedTask;
        }

        private void ConsumerOnOnError(object sender, Error error)
        {
            _log.Info($" error consuming message <{error.Reason}> with reason <{error.Reason}", error);
        }

        private void ConsumerOnOnConsumeError(object sender, Message message) => _log.Info($" error onconsume consuming message on topic <{message.Topic}> with reason <{message.Error}", message.Value);

        private async Task Consume(Consumer<Ignore, string> consumer, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var eventMessage = default(EventMessage);
                try
                {
                    if (!consumer.Consume(out var message, _options.Timeout))
                    {
                        continue;
                    }
                    _log.Info($" received message on topic <{message.Topic}>", message.Value);
                    // 1: The received message is deserialized to an IntegrationEvent which contains the meta-data for handling the event.
                    eventMessage = ParseAsEventMessage(message);

                    // 2: The meta-data is used to retrieve the registered message handler for this kind of message.
                    var handlerType = GetHandlerType(eventMessage);
                    if (handlerType == null)
                    {
                        continue;
                    }

                    using (var scope = _serviceProvider.CreateScope())
                    {
                        // 3: The registered message handler information is used to resolve an instance of this message handler.
                        var handler = GetHandler(scope, handlerType);
                        _log.Info($"{nameof(KafkaConsumer)}: received message on topic <{message.Topic}>", eventMessage);

                        // 4: The message handler is called to process the actual event
                        await HandleMessage(handler, eventMessage, _log, cancellationToken);

                        // 5: The message is committed, if no exception occurred during handling of the message
                        await consumer.CommitAsync(message);
                    }
                }
                catch (Exception ex)
                {
                    if (eventMessage != null)
                    {
                        ex.Data.Add("IntegrationMessage", eventMessage);
                    }

                    _log.Error(ex);
                }
            }
        }

        private object EventConsumer()
        {
            throw new NotImplementedException();
        }

        private async Task HandleMessage(IServiceEventHandler handler, EventMessage integrationMessage, ILog log,
            CancellationToken cancellationToken)
        {
            await handler.Handle(JObject.Parse(integrationMessage.EventData), log, cancellationToken);
        }

        protected virtual EventMessage ParseAsEventMessage(Message<Ignore, string> message) =>
            JsonConvert.DeserializeObject<EventMessage>(message.Value);

        internal Type GetHandlerType(EventMessage message)
        {
            if (message.Name == null)
            {
                throw new ArgumentNullException($"{nameof(KafkaConsumer)} exception: event Name is missing");
            }

            return _options.Handlers.TryGetValue(message.Name, out var handlerType) ? handlerType : null;
        }

        internal IServiceEventHandler GetHandler(IServiceScope scope, Type handlerType)
        {
            var handler = scope.ServiceProvider.GetService(handlerType);

            if (handler == null)
            {
                var nullRefEx = new NullReferenceException($"{nameof(KafkaConsumer)} exception: no handler found for type <{handlerType}>");
                throw nullRefEx;
            }

            if (handler is IServiceEventHandler eventHandler)
            {
                return eventHandler;
            }

            var castEx = new InvalidCastException($"{nameof(KafkaConsumer)} exception: handler <{handlerType}> not of type <{typeof(IServiceEventHandler)}>");
            throw castEx;
        }
    }
}

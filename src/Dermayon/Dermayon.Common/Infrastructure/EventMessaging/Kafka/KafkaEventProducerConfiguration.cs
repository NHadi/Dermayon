using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dermayon.Common.Infrastructure.EventMessaging.Kafka
{
    public class KafkaEventProducerConfiguration
    {
        public string Server { get; set; }
        public JsonSerializerSettings SerializerSettings { get; set; }
        public int MaxRetries { get; set; }
        public TimeSpan MessageTimeout { get; set; }
    }
}

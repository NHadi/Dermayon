using Dermayon.Common.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dermayon.Common.Infrastructure.EventMessaging.Kafka
{
    public interface IKakfaProducer
    {
        Task Send(IEvent @event, string topic);
    }
}
